namespace Quiz
{
    [Serializable]
    public class Question
    {
        public string describe { get; set; }
        public string area { get; set; }
        public List<string> answer_list { get; set; }
        public List<int> answer_right { get; set; }
    }
}