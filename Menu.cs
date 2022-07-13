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
        }

        public static void Header()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            System.Console.WriteLine(new string('*', 80));
            System.Console.WriteLine("\t\tВикторина");
            System.Console.WriteLine(new string('*', 80));
            Console.ResetColor();
        }

        public static void MainMenu()
        {
            // выбор между регистарцией и логином - потом
            Header();
            System.Console.WriteLine("1. Возврат к главному");
            System.Console.WriteLine("2. Начать игру");
            System.Console.WriteLine("3. Выход");
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

        private static string InputName() => Console.ReadLine().Trim();
        private static string PassToHash() => Base64Encode(Console.ReadLine());

        public static void Login()
        {

        }

        public static void Register()
        {
            
        }

        public delegate void MenuHandler();

        public static void ShowMenu(int screen, TypeMenu type)
        {
            MenuHandler? menu = MainMenu;
            if (type == TypeMenu.MainMenu)
            {
                switch (screen)
                {
                    case 1:
                        menu = MainMenu;
                        break;
                    case 2:
                        menu = StartGame;
                        break;
                    case 3: 
                        ContinueGame = false;
                        break;
                }
            }
            else if (type == TypeMenu.RegistrationMenu)
            {
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
            menu?.Invoke();
        }
    }
}