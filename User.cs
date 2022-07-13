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
            set { isAdmin = value;  }
        }

        public User() {}
        public User(string Name, string HashPass, DateTime bDate, bool isAdmin = false)
        {
            this.Name = Name;
            this.HashPass = HashPass;
            this.bDate = bDate;
            this.isAdmin = isAdmin;
        }

        public override string ToString()
        {
            return $"{Name}" + (IsAdmin ? " (admin)":"");
        }

    }
}