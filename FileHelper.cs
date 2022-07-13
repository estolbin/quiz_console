using System.Text.Json;

namespace Quiz
{
    public static class FileHelper
    {
        private const string userFileName = @"d:\0\c_sharp\quiz\users.json";
        private const string questionFileName = @"d:\0\c_sharp\quiz\questions.json";
        public static List<User> ReadUserList()
        {
            if (!File.Exists(userFileName)) return new List<User>();
            var json = File.ReadAllText(userFileName);
            var list = JsonSerializer.Deserialize<List<User>>(json);
            return list;
        }

        public static void SaveUserList(List<User> user_list)
        {
            string json = JsonSerializer.Serialize(user_list);
            File.WriteAllText(userFileName, json);
        }

        public static List<Question> ReadQuestionList()
        {
            if (!File.Exists(questionFileName)) return new List<Question>();
            var json = File.ReadAllText(questionFileName);
            var list = JsonSerializer.Deserialize<List<Question>>(json);
            return list;
        }

        public static void SaveQuestionList(List<Question> question_list)
        {
            string json = JsonSerializer.Serialize(question_list);
            File.WriteAllText(questionFileName, json);
        }

    }
}