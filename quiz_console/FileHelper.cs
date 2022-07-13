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

        public static List<Score> ReadScoreList()
        {
            if (!File.Exists(scoreFileName)) return new List<Score>();
            var json = File.ReadAllText(scoreFileName);
            var list = JsonSerializer.Deserialize<List<Score>>(json);
            return list;
        }

        public static void SaveScoreList(List<Score> score_list)
        {
            string json = JsonSerializer.Serialize(score_list);
            File.WriteAllText(scoreFileName, json);
        }
    }
}