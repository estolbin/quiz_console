namespace Quiz
{
    class Game
    {
        public static User? currentUser { get; set; }
        private const int QuestionCount = 20;
        private static int score;
        public static int Score
        {
            get { return score; }
            set 
            { 
                if ((score - value) < 0) score = 0;
                score = value; 
            }
        }
        
        public static List<Question> question_list { get; set; }
        public Game() {}

        public void CreateQuestionList(string area)
        {
  
            List<Question> temp = FileHelper.ReadQuestionList();
            question_list = new List<Question>();
            question_list.AddRange(temp.Select(x => x).Take(20));
        }

        public static void AddQuestion(Question question)
        {
            List<Question> question_list = FileHelper.ReadQuestionList();
            question_list.Add(question);
            FileHelper.SaveQuestionList(question_list);
        }

        public void Start()
        {
            foreach (var item in question_list)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                System.Console.WriteLine($"{item.describe}\n");
                Console.ResetColor();
                int i = 1;
                foreach (var ans in item.answer_list)
                {
                    System.Console.WriteLine($"\t{i++}). {ans}");
                }

                System.Console.Write("Введите номера правильных ответов через пробел: ");
                try
                {
                    List<int> arr = Array.ConvertAll(Console.ReadLine().Trim().Split(' '), Convert.ToInt32).ToList();

                    if (Enumerable.SequenceEqual(arr, item.answer_right)) 
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        System.Console.WriteLine("Верно!");   
                        Game.score += 100;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        System.Console.WriteLine("не верно!");
                    }

                }
                catch (System.Exception)
                {
                    
                    System.Console.WriteLine("Не выбраны варианты ответов!");;
                }
                Console.ReadKey();
                Console.ResetColor();

            }
        }
    }
}