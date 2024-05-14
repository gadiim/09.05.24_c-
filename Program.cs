using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

//Тема: Серіалізація об'єктів. Логування
//Модуль 14. Частина 1

namespace _09._05._24_c_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Завдання 1:
            //Створіть програму для роботи з масивом дробів(чисельник і
            //знаменник) з такою функціональністю:

            //1.Введення масиву дробів з клавіатури.
            //2.Серіалізація масиву дробів.
            //3.Збереження серіалізованого масиву у файл.
            //4.Завантаження серіалізованого масиву з файлу.

            //Після завантаження потрібно виконати десеріалізацію.
            //Вибір певного формату серіалізації потрібно зробити вам.
            //Звертаємо вашу увагу, що вибір має бути обґрунтованим.

            Console.WriteLine($"Task 1\n");

            int fraction_counter = 1;
            string path_1 = "test";
            string json_1;
            string loaded_file_1;

            List<string> list_1 = new List<string>();

            Console.WriteLine("fill the fraction array\n(press 'q' to quit)\n");
            while (true)
            {
                Console.Write($"numerator {fraction_counter}:\t\t");
                string numerator = Console.ReadLine();

                if (numerator.ToLower() == "q") break;
                Console.Write($"denominator {fraction_counter}:\t\t");

                string denominator = Console.ReadLine();

                string input = $"{numerator}/{denominator}";
                string[] numbers = input.Split(' ');

                foreach (string number in numbers)
                {
                    list_1.Add(number);
                }

                fraction_counter++;
                Console.WriteLine();
            }

            Console.WriteLine();

            json_1 = ToSerialize(list_1);
            DisplayJson(json_1);

            WriteToFile(json_1, path_1);

            loaded_file_1 = ReadFromFile(path_1);

            list_1 = ToDeserialize<List<string>>(loaded_file_1);
            DisplayConsole(list_1);

            Console.WriteLine("\nPress any key to continue . . .");
            Console.ReadKey();
            Console.Clear();

            //Завдання 2:
            //Створіть програму для роботи з інформацією про журнал, в якій
            //зберігатиметься таку інформація:

            //1.Назва журналу.
            //2.Назва видавництва.
            //3.Дата видання.
            //4.Кількість сторінок.

            //Програма має бути з такою функціональністю:

            //1.Введення інформації про журнал.
            //2.Виведення інформації про журнал.
            //3.Серіалізація журналу.
            //4.Збереження серіалізованого журналу у файл.
            //5.Завантаження серіалізованого журналу з файлу.
            //
            //Після завантаження потрібно виконати десеріалізацію журналу.
            //Вибір певного формату серіалізації потрібно зробити вам.
            //Звертаємо вашу увагу, що вибір має бути обґрунтованим.

            Console.WriteLine($"Task 2\n");

            string path_2 = "";
            string json_2, loaded_2, choice_2;
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("create journal (press)\t1\nshow journal (press)\t2\nexit (press)\t\t3");
                choice_2 = Console.ReadLine();
                switch (choice_2)
                {
                    case "1":
                        Journal add_journal_2 = CreateJournal();
                        path_2 = add_journal_2.Title;
                        json_2 = ToSerialize(add_journal_2);
                        DisplayJson(json_2);
                        WriteToFile(json_2, path_2);
                        break;
                    case "2":
                        loaded_2 = ReadFromFile(path_2);
                        if (System.IO.File.Exists(MakeName(path_2)))
                        {
                            Journal loaded_magazine_2 = ToDeserialize<Journal>(loaded_2);
                            loaded_magazine_2.ToShow();
                        }
                        break;
                    case "3":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("default");
                        break;
                }
                Console.WriteLine();
            }

            //Console.WriteLine("\nPress any key to continue . . .");
            //Console.ReadKey();
            //Console.Clear();

            //Завдання 3:
            //Додайте до попереднього завдання список статей з журналу.
            //Потрібно зберігати наступну інформацію про кожну статтю:

            //1.Назва статті.
            //2.Кількість символів.
            //3.Анонс статті.

            //Змініть функціональність з попереднього завдання таким чином,
            //щоб вона враховувала список статей.
            //Вибір певного формату серіалізації потрібно зробити вам.
            //Звертаємо вашу увагу, що вибір має бути обґрунтованим.

            Console.WriteLine($"Task 3\n");

            Magazine magazine_3 = new Magazine();
            magazine_3.InputMagazineInfo();
            magazine_3.WriteToFile(magazine_3.ToSerialize(), magazine_3.MakePath());
            string json_3 = magazine_3.ReadFromFile();
            Magazine load_magazine_3 = Magazine.ToDeserialize(json_3);
            load_magazine_3.ToShow();

            Console.WriteLine("\nPress any key to continue . . .");
            Console.ReadKey();
            Console.Clear();

            //Завдання 4:
            //Додайте до попереднього завдання можливість створення
            //масиву журналів.
            //Змініть функціональність з другого завдання таким чином, щоб
            //вона враховувала масив журналів.
            //Вибір певного формату серіалізації потрібно зробити вам.
            //Звертаємо вашу увагу, що вибір має бути обґрунтованим.

            Console.WriteLine($"Task 4\n");

            string path_4;
            string json_4;
            bool exit_4 = false;

            while (!exit_4)
            {
                Console.WriteLine("create magazine (press)\t1\nshow magazine (press)\t2\nexit (press)\t\t3");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write($"enter path:\t\t");
                        path_4 = Console.ReadLine();
                        Console.WriteLine();

                        List<Magazine> list_new_magazines_4 = new List<Magazine>();
                        while (true)
                        {
                            Magazine new_magazine_4 = new Magazine();
                            new_magazine_4.InputMagazineInfo();

                            list_new_magazines_4.Add(new_magazine_4);
                            Console.WriteLine("\nto add magazine? (Y/N)");
                            string input_magazine_4 = Console.ReadLine().ToLower();

                            if (input_magazine_4 != "y")
                            {
                                break;
                            }
                        }
                        SaveListToFile(list_new_magazines_4, path_4);
                        break;
                    case "2":
                        Console.Write($"enter path (no file format):\t");
                        path_4 = Console.ReadLine();
                        Console.WriteLine();
                        json_4 = ReadFromFile(path_4);
                        try
                        {
                            List<Magazine> list_loaded_magazines_4 = JsonConvert.DeserializeObject<List<Magazine>>(json_4);
                            foreach (var magazine in list_loaded_magazines_4)
                            {
                                Console.WriteLine($"-= magazine informatoin =-\n\n{magazine}");
                            }
                        }
                        catch (JsonException ex)
                        {
                            Console.WriteLine($"* error: {ex.Message}");
                        }                      
                        break;
                    case "3":
                        exit_4 = true;
                        break;
                    default:
                        Console.WriteLine("default");
                        break;
                }
                Console.WriteLine();
            }
            Console.WriteLine();

        }


        static Journal CreateJournal()
        {
            Console.WriteLine("enter journal information:\n");
            Console.Write("title:\t\t\t");
            string title = Console.ReadLine();
            Console.Write("publisher:\t\t");
            string publisher = Console.ReadLine();
            Console.Write("data(year):\t\t");
            int year = Convert.ToInt32(Console.ReadLine());
            Console.Write("pages:\t\t\t");
            int pagesCount = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            return new Journal(title, publisher, year, pagesCount);    
        }
        static string ToSerialize(Object obj)
        {
            Console.WriteLine($"* file converted to json format\n");
            return JsonConvert.SerializeObject(obj);
        }
        static T ToDeserialize<T>(string json)
        {
            Console.WriteLine($"* file converted from json format\n");
            return JsonConvert.DeserializeObject<T>(json);
        }
        static string MakeName(string file)
        {
            return $"{file.Replace(" ", "_")}.txt";
        }
        static void WriteToFile(string file, string path)
        {
            try
            {
                using (StreamWriter stream_writer = new StreamWriter(MakeName(path), false))
                {
                    stream_writer.Write($"{file}");
                    Console.WriteLine($"* file \"{MakeName(path)}\" created/updated");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"error: {ex.Message}");
            };
        }
        static string ReadFromFile(string path)
        {
            try
            {
                using (StreamReader stream_reader = new StreamReader(MakeName(path)))
                {
                    return stream_reader.ReadToEnd();
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("* file not found");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"* error: {ex.Message}");
                return null;
            }
        }
        static void DisplayJson (string json)
        {
            Console.WriteLine($"json format:\t\t{json}\n");
        }
        static void DisplayConsole<T>(List<T> list)
        {
            Console.Write("console format:\t\t");
            foreach (T item in list)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine();
        }
        static void SaveListToFile(List<Magazine> magazines, string path)
        {
            string json = JsonConvert.SerializeObject(magazines, Formatting.Indented);
            System.IO.File.WriteAllText(path, json);
        }

    }
    //Task_2

    struct Journal
    {
        public string Title { get; set; }
        public string Publisher { get; set; }
        public int PublicationDate { get; set; }
        public int PageCount { get; set; }
        public Journal(string title, string publisher, int publicationDate, int pageCount)
        {
            Title = title;
            Publisher = publisher;
            PublicationDate = publicationDate;
            PageCount = pageCount;
        }
        public override string ToString()
        {
            return $"title:\t\t\t\"{Title}\";\n" +
                    $"publisher:\t\t\"{Publisher}\";\n" +
                    $"publication:\t\t{PublicationDate} year;\n" +
                    $"page count:\t\t{PageCount} pages;\n";
        }
        public void ToShow()
        {
            Console.WriteLine($"-= journal informatoin =-\n\n{this}");
        }
    }

    //Task_3

    public class Article
    {
        private string articleTitle { get; set; }
        private int characterCount { get; set; }
        private string announcement { get; set; }
        public Article() { }
        public Article(string articleTitle, int characterCount, string announcement)
        {
            this.articleTitle = articleTitle;
            this.characterCount = characterCount;
            this.announcement = announcement;
        }
        public string ArticleTitle { get { return articleTitle; } set { articleTitle = value; } }
        public int CharacterCount { get { return characterCount; } set { characterCount = value; } }
        public string Announcement { get { return announcement; } set { announcement = value; } }
        public override string ToString()
        {
            return $"article title:\t\t\"{ArticleTitle}\";\n" +
                    $"character сount:\t{CharacterCount} symbols;\n" +
                    $"announcement:\t\t{Announcement};\n\n";
        }
        public void ToShow()
        {
            Console.WriteLine($"-= article information =-\n\n{this}");
        }
        public void InputArticleInfo()
        {
            Console.WriteLine("enter article information:\n");
            Console.Write("title:\t\t\t\t");
            ArticleTitle = Console.ReadLine();
            Console.Write("data(year):\t\t\t");
            CharacterCount = Convert.ToInt32(Console.ReadLine());
            Console.Write("pages:\t\t\t\t");
            Announcement = Console.ReadLine();
        }
    }
    class Magazine
    {
        private string title { get; set; }
        private string publisher { get; set; }
        private int publicationDate { get; set; }
        private int pageCount { get; set; }
        private List<Article> articles { get; set; }
        public Magazine() { }
        public Magazine(string title, string publisher, int publicationDate, int pageCount, List<Article> articles)
        {
            this.title = title;
            this.publisher = publisher;
            this.publicationDate = publicationDate;
            this.pageCount = pageCount;
            this.articles = articles;
        }
        public string Title { get { return title; } set { title = value; } }
        public string Publisher { get { return publisher; } set { publisher = value; } }
        public int PublicationDate { get { return publicationDate; } set { publicationDate = value; } }
        public int PageCount { get { return pageCount; } set { pageCount = value; } }
        public List<Article> Articles { get { return articles; } set { articles = value; } }
        public override string ToString()
        {
            string artickesInfo = string.Join("", articles);
            return $"magazine title:\t\t\"{Title}\";\n" +
                    $"publisher:\t\t\"{Publisher}\";\n" +
                    $"publication:\t\t{PublicationDate} year;\n" +
                    $"page count:\t\t{PageCount} pages;\n\n" +
                    $"-= article list =-\n\n{artickesInfo}\n";
        }
        public void ToShow()
        {
            Console.WriteLine($"\n-= magazine informatoin =-\n\n{this}");
        }
        public void InputMagazineInfo()
        {
            Console.WriteLine("enter magazine information:\n");
            Console.Write("title:\t\t\t\t");
            Title = Console.ReadLine();
            Console.Write("publisher:\t\t\t");
            Publisher = Console.ReadLine();
            Console.Write("data(year):\t\t\t");
            PublicationDate = Convert.ToInt32(Console.ReadLine());
            Console.Write("pages:\t\t\t\t");
            PageCount = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            Console.WriteLine("enter article list\n(press 'q' to quit)\n");

            articles = new List<Article>();

            while (true)
            {
                Console.Write("title:\t\t\t\t");
                string article_title = Console.ReadLine();
                if (article_title.ToLower() == "q")
                {
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.WriteLine();
                    break;
                }
                Console.Write("character count(symbols):\t");
                int character_count = Convert.ToInt32(Console.ReadLine());
                Console.Write("announcement:\t\t\t");
                string announcement = Console.ReadLine();
                articles.Add(new Article(article_title, character_count, announcement));
            }
        }
        public string MakePath()
        {
            return $"{Title.Replace(" ", "_")}.txt";
        }
        public string ToSerialize()
        {
            Console.WriteLine($"* file \'{Title}\' converted to json format");
            return JsonConvert.SerializeObject(this);
        }
        public static Magazine ToDeserialize(string json)
        {
            Console.WriteLine($"* file converted from json format");
            return JsonConvert.DeserializeObject<Magazine>(json);
        }
        public void WriteToFile(string file, string path)
        {
            try
            {
                using (StreamWriter stream_writer = new StreamWriter(path, true))
                {
                    stream_writer.Write($"{file}");
                    Console.WriteLine($"* file \'{path}\' created");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"error: {ex.Message}");
            }
        }
        public string ReadFromFile()
        {
            try
            {
                using (StreamReader stream_reader = new StreamReader(MakePath()))
                {
                    return stream_reader.ReadToEnd();
                }
            }
            catch (FileNotFoundException)
            {
                return "* file not found";
            }
            catch (Exception ex)
            {
                return $"* error: {ex.Message}";
            }
        }
    }
}
