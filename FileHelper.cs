using System.Text.Json;

namespace Quiz
{
    public static class FileHelper
    {
        private const string userFileName = @"d:\0\c_sharp\quiz\users.json";
        public static List<User> ReadUserList()
        {
            var json = File.ReadAllText(userFileName);
            var list = JsonSerializer.Deserialize<List<User>>(json);
            return list;
        }

        public static void SaveUserList(List<User> user_list)
        {
            string json = JsonSerializer.Serialize(user_list);
            File.WriteAllText(userFileName, json);
        }

    }
}