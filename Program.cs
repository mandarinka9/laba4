using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace laba4
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            // Задание 1: Удаление элементов из списка
            Console.WriteLine("Введите элементы списка");
            var list = InputList<dynamic>();
            Console.WriteLine("Введите элементы для удаления");
            var elementsToRemove = InputList<dynamic>();
            RemoveElements(list, elementsToRemove);
            Console.WriteLine("Список после удаления элементов:");
            PrintList(list);

            // Задание 2: Вывод списка в обратном порядке
            Console.WriteLine("Введите элементы списка для обратного вывода:");
            var reverseList = InputLi<dynamic>();
            PrintReverse(reverseList);

            // Задание 3: Обработка учебных заведений
            Institutionsss();

            // Задание 4: Вывод звонких согласных
            PrintVoicedCons();

        }

        // Задание 1: Удаление нескольких элементов из списка
        public static void RemoveElements<T>(List<T> list, List<T> elementsToRemove)
        {
            for (int i = list.Count - 1; i >= 0; i--) // с конца для избежания смещения индексов
            {
                foreach (var element in elementsToRemove)
                {
                    if (EqualityComparer<T>.Default.Equals(list[i], element)) //удаляем элемент если равны
                    {
                        list.RemoveAt(i); // Удаляем элемент по индексу
                        break;
                    }
                }
            }
        }

        // Задание 2: Вывод списка в обратном порядке
        public static void PrintReverse<T>(LinkedList<T> linkedList)
        {
            var current = linkedList.Last;
            while (current != null)
            {
                Console.WriteLine(current.Value); 
                current = current.Previous; // предыдущ элемент
            }
        }

        public static LinkedList<T> InputLi<T>()
        {
            var linkedList = new LinkedList<T>();
            Console.WriteLine("Введите элементы списка (стоп для завершения):");
            string input;
            while ((input = Console.ReadLine()) != "стоп")
            {                                                             
                var ttext = Convert.ChangeType(input, typeof(T));
                linkedList.AddLast((T)ttext);
            }
            return linkedList;
        }

        // Задание 3: Учебные заведения и фирмы
        public static void Institutionsss()
        {
            Console.WriteLine("Введите список фирм (через запятую):");
            var allPossibleCompanies = new HashSet<string>( //без повторов
                Console.ReadLine().Split(',').Select(f => f.Trim()) //ввод разделяется , и для каждого эл удаляются пробелы
            );

            Console.WriteLine("Введите количество учебных заведений:");
            int n = int.Parse(Console.ReadLine());
            var institutions = new List<string>(); //для названий
            var companyPurchases = new Dictionary<string, HashSet<string>>(); //ключ - название уч, знач - названия фирм

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"Введите название учебного заведения {i + 1}:");
                string institution = Console.ReadLine();
                institutions.Add(institution);

                Console.WriteLine("Введите фирмы, в которых закупалась техника (через запятую):");
                string[] firms = Console.ReadLine().Split(',');
                var firmSet = new HashSet<string>(firms.Select(f => f.Trim())); //удаляет пробелы и созд список уникальных названий фирм

                companyPurchases[institution] = firmSet; //добавляет записи в словарь comPur присваивая множ фирм по ключу instit
            }

            // 1) В каких фирмах закупка производилась каждым из заведений?
            var commonCompanies = new HashSet<string>(companyPurchases[institutions[0]]); //для хранен фирм, куплен 1 учрежд
            foreach (var institution in institutions) 
            {
                commonCompanies.IntersectWith(companyPurchases[institution]); //оставл только повторяющ фирмы
            }
            Console.WriteLine("Фирмы, где закупка производилась каждым из заведений:");
            Console.WriteLine(string.Join(", ", commonCompanies));

            // 2) В каких фирмах закупка производилась хотя бы одним из заведений?
            var allCompanies = new HashSet<string>(); //пустое мн
            foreach (var firms in companyPurchases.Values) //проходим по всем фирмам без ключей
            {
                allCompanies.UnionWith(firms);
            }
            Console.WriteLine("Фирмы, где закупка производилась хотя бы одним из заведений:");
            Console.WriteLine(string.Join(", ", allCompanies));

            // 3) В каких фирмах ни одно из заведений не закупало компьютеры?
            var companiesNotUsed = new HashSet<string>(allPossibleCompanies); //мн всех фирм
            companiesNotUsed.ExceptWith(allCompanies); //удаляет из всех фирм те, которые есть в allCompanies
            Console.WriteLine("Фирмы, где ни одно из заведений не закупало компьютеры:");
            Console.WriteLine(string.Join(", ", companiesNotUsed));
        }

        // Задание 4: Вывод звонких согласных
        public static void PrintVoicedCons()
        {
            Console.WriteLine("Введите текст:");
            string text = Console.ReadLine();
            char[] voicedConsonants = { 'б', 'в', 'г', 'д', 'ж', 'з', 'й', 'л', 'м', 'н', 'р' };
            var foundConsonants = new HashSet<char>();

            foreach (char c in text)
            {
                if (voicedConsonants.Contains(c))
                {
                    foundConsonants.Add(c);
                }
            }

            Console.WriteLine("Звонкие согласные в алфавитном порядке:");
            foreach (var consonant in foundConsonants.OrderBy(c => c))
            {
                Console.WriteLine(consonant);
            }
        }


        //метод для ввода и вывода
        private static List<dynamic> InputList<T>()
        {
            var list = new List<dynamic>();
            Console.WriteLine("Введите элементы списка. Для завершения введите пустую строку: ");

            while (true)
            {
                string input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    break; // Завершение ввода при пустой строке
                }

                string[] inputs = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);//разбивает на отдельные пробелы. удаляет пустые элементы из результата
                foreach (var item in inputs) //по каждому элементу строки
                {
                    try
                    {
                        dynamic value = Convert.ChangeType(item, typeof(T));
                        list.Add(value);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine($"'{item}' не может быть преобразовано в {typeof(T).Name}.");
                    }
                }
            }

            return list;
        }

        private static void PrintList<T>(List<T> list) //вывод рез через ,
        {
            Console.WriteLine(string.Join(", ", list));
        }
    }
}