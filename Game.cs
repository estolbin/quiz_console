namespace Quiz
{
    class Game
    {
        public static User currentUser { get; set; }
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
        
        List<Question> question_list = new List<Question>();

        public Game() {}

        public void CreateQuestionList(string area)
        {
            List<Question> temp = new List<Question>();
            Question q1 = new Question();
            q1.describe = "Какого цвета небо?";
            q1.area = area;
            q1.answer_list = new List<string>();
            q1.answer_list.Add("Синее");
            q1.answer_list.Add("Зеленое");
            q1.answer_list.Add("Никакое");
            q1.answer_list.Add("Коричневое");

            q1.answer_right = new List<int>();
            q1.answer_right.Add(1);

            temp.Add(q1);

            question_list = temp;
        }

        public void AddQuestion(Question question)
        {
            if (question_list.Count < QuestionCount) question_list.Add(question);
            else System.Console.WriteLine("Уже добавлено необходимое количество вопросов!");
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