using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace Quiz
{

    public static class FileHelper
    {
        public static string fileName { get; set; }
        public static void ReadFromJSON()
        {

        }

        public static void WriteToJSON()
        {

        }
    }
    class Question
    {
        public string describe { get; set; }
        public string area { get; set; }
        public List<string> answer_list { get; set; }
        public List<int> answer_right { get; set; }
    }

    class Program
    {
        public static void Main(string[] args)
        {
            while (Menu.ContinueGame)
            {
                Menu.ShowMenu(100, TypeMenu.MainMenu);
            }
        }
    }
}