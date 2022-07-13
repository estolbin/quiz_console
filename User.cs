namespace Quiz
{
    public class User
    {
        public string Name { get; set; }
        public string HashPass { get; set; }
        public DateTime bDate { get; set; }

        private bool isAdmin;
        public bool IsAdmin
        {
            get { return isAdmin; }
            set 
            { 
                isAdmin = true;
                // try {
                // List<User> user_list = FileHelper.ReadUserList();
                // if (user_list.Count == 0) isAdmin = true; 
                // else isAdmin = false;
                // } 
                // catch 
                // {
                //     isAdmin = true; // нет файла, значит нет пользователей.
                // }
            }
        }
       

        public User() {}
        public User(string Name, string HashPass, DateTime bDate)
        {
            this.Name = Name;
            this.HashPass = HashPass;
            this.bDate = bDate;
        }

        public override string ToString()
        {
            return $"{Name}";
        }

    }
}