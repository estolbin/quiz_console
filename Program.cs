using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace Quiz
{

    public enum TypeMenu
    {
        MainMenu,
        RegistrationMenu
    }   

    class Program
    {
        public static void Main(string[] args)
        {
            while (Menu.ContinueGame)
            {
                Menu.ShowMenu(100, TypeMenu.RegistrationMenu);
            }
        }
    }
}