namespace Quiz
{
    public class User
    {
        public string Name { get; set; }
        public string HashPass { get; set; }
        public DateTime bDate { get; set; }

        public User() {}
        public User(string Name, string HashPass, DateTime bDate)
        {
            this.Name = Name;
            this.HashPass = HashPass;
            this.bDate = bDate;
        }

    }
}