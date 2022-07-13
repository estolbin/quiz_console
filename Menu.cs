namespace Quiz
{
    public enum TypeMenu
    {
        MainMenu,
        RegistrationMenu
    }   
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
            if (Game.currentUser.IsAdmin)
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
            return Console.ReadLine().Trim();
        }
        private static string PassToHash()
        {
            Console.WriteLine("Введите пароль: ");
            return Base64Encode(Console.ReadLine());
        } 

        public static void Login()
        {
            string name = InputName();
            string hashPass = PassToHash();
            List<User> list_users = FileHelper.ReadUserList();
            var users = list_users.Where(x => x.Name == name).Take(1);
            
            foreach (var user in users)
            {
                if (user.HashPass != hashPass)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Неправильный пароль!");
                    Console.ResetColor();
                    Console.ReadKey();
                    return;
                }
                Game.currentUser = user;
                ShowMenu(1, TypeMenu.MainMenu);
            }

        }

        public static void Register()
        {
            List<User> list_users = FileHelper.ReadUserList();
            string name = InputName();
            var users = list_users.Where(x => x.Name == name).Take(1);
            foreach (var item in users)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine($"Пользователь с именем {name} уже зарегистрирован!");
                Console.ReadKey();
                ShowMenu(100, TypeMenu.RegistrationMenu);
            }
            string hashPass = PassToHash();
            System.Console.WriteLine("Дата рождения (в формате \"2022-01-01\"): ");
            string _bDate = Console.ReadLine();
            DateTime bDate = DateTime.Parse(_bDate);
            //List<User> list_users = new List<User>();
            list_users.Add(new User{Name = name, HashPass = hashPass, bDate = bDate}) ;
            FileHelper.SaveUserList(list_users);

        }

        public static void Statistic()
        {
            System.Console.WriteLine("Статистика");
            Console.ReadKey();
            ShowMenu(1, TypeMenu.MainMenu);
        }

        public static void AddQuestion()
        {
            
        }

        public delegate void MenuHandler();

        public static void ShowMenu(int screen, TypeMenu type)
        {
            MenuHandler? menu = (type == TypeMenu.MainMenu ? MainMenu : ShowFirstScreen);
            if (type == TypeMenu.MainMenu)
            {
                menu = MainMenu;
                switch (screen)
                {
                    case 1:
                        menu = MainMenu;
                        break;
                    case 2:
                        menu = StartGame;
                        break;
                    case 3:
                        menu = Statistic;
                        break;
                    case 4: 
                        ContinueGame = false;
                        break;
                    case 5:
                        menu = AddQuestion;
                        break;
                }
            }
            else if (type == TypeMenu.RegistrationMenu)
            {
                menu = ShowFirstScreen;
                switch (screen)
                {
                    case 1:
                        menu = Login;
                        break;
                    case 2:
                        menu = Register;
                        break;
                    case 3:
                        ContinueGame = false;
                        break;
                }
            }
            if (ContinueGame) menu?.Invoke();
        }
    }
}