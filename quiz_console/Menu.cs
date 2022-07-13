using System;
using System.Collections.Generic;
using System.Linq;

namespace Quiz
{

    public static class Menu
    {
        public static bool ContinueGame = true;
        public static void StartGame()
        {
            System.Console.Write("Выбор области знаний: ");
            string area = Console.ReadLine();

            Game game = new Game();
            game.CreateQuestionList(area);
            game.Start();     
            ShowMenu(1, TypeMenu.MainMenu);
        }

        public static void PrintError(string Message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(Message);
            Console.ResetColor();
            Console.ReadKey();
        }


        public static void Header()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            System.Console.WriteLine(new string('*', 80));
            System.Console.WriteLine($"\t\tВикторина\t\t{Game.currentUser}\t{Game.Score}");
            System.Console.WriteLine(new string('*', 80));
            Console.ResetColor();
        }

        public static void MainMenu()
        {
            // выбор между регистарцией и логином - потом
            Header();
            System.Console.WriteLine("1. Выйти из игры");
            System.Console.WriteLine("2. Начать игру");
            System.Console.WriteLine("3. Посмотреть статистику");
            System.Console.WriteLine("4. Выход");
            bool isAdmin = (Game.currentUser == null ? false : Game.currentUser.IsAdmin);
            if (isAdmin)
            {
                System.Console.WriteLine("5. Добавить вопросы.");
            }
            ShowMenu(GetChoice(), TypeMenu.MainMenu);
        }

        public static int GetChoice()
        {
            int choice;
            Int32.TryParse(Console.ReadLine(), out choice);
            return choice;
        }

        public static void ShowFirstScreen()
        {
            Header();
            System.Console.WriteLine("1. Залогинится");
            System.Console.WriteLine("2. Зарегистрироваться");
            System.Console.WriteLine("3. Выход");
            ShowMenu(GetChoice(), TypeMenu.RegistrationMenu);
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        private static string InputName() 
        {
            Console.WriteLine("Введите имя: ");
            string? name = Console.ReadLine();
            return name == null ? "" : name;
        }
        private static string PassToHash()
        {
            Console.WriteLine("Введите пароль: ");
            string? pass = Console.ReadLine();
            return pass == null ? "" : Base64Encode(pass);
        } 

        public static void Login()
        {
            string name = InputName();
            List<User> list_users = FileHelper.ReadUserList();
            var users = list_users.Where(x => x.Name == name).Take(1);
            if (users.Count() != 0)            
            {
                string hashPass = PassToHash();
                foreach (var user in users)
                {
                    if (user.HashPass != hashPass)
                    {
                        PrintError("Неправильный пароль!");
                        return;
                    }
                    Game.currentUser = user;
                    Game.question_list = FileHelper.ReadQuestionList();
                    ShowMenu(1, TypeMenu.MainMenu);
                }
            }
            else
            {
                PrintError($"Не найден пользователь с именем {name}");
            }
        }

        public static void Register()
        {
            bool isAdmin = false;
            List<User> list_users = FileHelper.ReadUserList();
            if (list_users.Count == 0) isAdmin = true;
            string name = InputName();
            var users = list_users.Where(x => x.Name == name).Take(1);
            foreach (var item in users)
            {
                PrintError($"Пользователь с именем {name} уже зарегистрирован!");
                return;
            }
            string hashPass = PassToHash();
            System.Console.WriteLine("Дата рождения (в формате \"2022-01-01\"): ");
            string? _bDate = Console.ReadLine();
            DateTime bDate;
            DateTime.TryParse(_bDate, out bDate);

            list_users.Add(new User{Name = name, HashPass = hashPass, bDate = bDate, IsAdmin = isAdmin}) ;
            FileHelper.SaveUserList(list_users);

        }

        public static void Statistic()
        {
            Header();
            System.Console.WriteLine("\t\tСтатистика:");
            var list = FileHelper.ReadScoreList().OrderByDescending(x => x.score);
            foreach (var item in list)
            {
                Console.WriteLine($"{item.user.Name}\t-\t{item.time}\t--\t\t{item.score}" );
            }
            Console.ReadKey();
            ShowMenu(1, TypeMenu.MainMenu);
        }

        public static void AddQuestion()
        {
            Question question = new Question();
            System.Console.WriteLine("Текст вопроса: ");
            question.describe = Console.ReadLine();
            System.Console.WriteLine("Введите область знаний: ");
            question.area = Console.ReadLine();
            question.answer_list = new List<string>();
            Console.Write("Введите количество ответов: ");
            int n; 
            Int32.TryParse(Console.ReadLine(), out n);
            for (int i = 0; i < n; i++)
            {
                Console.Write($"{i}). ");
                question.answer_list.Add(Console.ReadLine());
            }
            System.Console.WriteLine("Введите номер (номера через пробел) правильного/ных ответа/ов:");
            question.answer_right = new List<int>();
            question.answer_right = Array.ConvertAll(Console.ReadLine().Trim().Split(' '), Convert.ToInt32).ToList();
            Game.AddQuestion(question);
        }

        public static void Logout()
        {
            Game.currentUser = null;
        }

        public static void ShowMenu(int screen, TypeMenu type)
        {
            if (type == TypeMenu.MainMenu)
            {
                switch (screen)
                {
                    case 1:
                        MainMenu();
                        break;
                    case 2:
                        StartGame();
                        break;
                    case 3:
                        Statistic();
                        break;
                    case 4: 
                        ContinueGame = false;
                        break;
                    case 5:
                        bool isAdmin = Game.currentUser == null ? false : Game.currentUser.IsAdmin;
                        if (isAdmin) AddQuestion();
                        else MainMenu();
                        break;
                }
            }
            else if (type == TypeMenu.RegistrationMenu)
            {
                switch (screen)
                {
                    case 1:
                        Login();
                        break;
                    case 2:
                        Register();
                        break;
                    case 3:
                        ContinueGame = false;
                        break;
                    case 100:
                        ShowFirstScreen();
                        break;
                }
            }
        }
    }
}