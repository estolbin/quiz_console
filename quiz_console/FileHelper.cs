using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Quiz
{
    public static class FileHelper
    {
        private const string userFileName = @"..\..\..\users.json";
        private const string questionFileName = @"..\..\..\questions.json";
        private const string scoreFileName = @"..\..\..\score.json";

        private static void SaveListToFile<T>(List<T> list, string fileName)
        {
            string json = JsonSerializer.Serialize(list);
            File.WriteAllText(fileName, json);
        }

        private static List<T> ReadListFromFile<T>(string fileName)
        {
            //if (!File.Exists(userFileName)) return new List<User>();
            if (!File.Exists(fileName)) return new List<T>();
            var json = File.ReadAllText(fileName);
            var list = JsonSerializer.Deserialize<List<T>>(json);
            return list;

        }

        public static List<User> ReadUserList()
        {
            return ReadListFromFile<User>(userFileName);
        }

        public static List<Question> ReadQuestionList()
        {
            return ReadListFromFile<Question>(questionFileName);
        }
        public static List<Score> ReadScoreList()
        {
            return ReadListFromFile<Score>(scoreFileName);
        }
        public static void SaveUserList(List<User> user_list)
        {
            SaveListToFile(user_list, userFileName);
        }

        public static void SaveQuestionList(List<Question> question_list)
        {
            SaveListToFile(question_list, questionFileName);
        }

        public static void SaveScoreList(List<Score> score_list)
        {
            SaveListToFile(score_list, scoreFileName);
        }
    }
}