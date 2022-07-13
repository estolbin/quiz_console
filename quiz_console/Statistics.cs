using System;
using System.Collections.Generic;

namespace Quiz
{
    public class Score
    {
        public User user { get; set; }
        public int score { get; set; }
        public DateTime time {get; set;}
    }
    public static class Statistics
    {
        public static void SaveStatistic()
        {
            Score currentScore = new Score {user = Game.currentUser, score = Game.Score, time = DateTime.Now};
            List<Score> score_list = FileHelper.ReadScoreList();
            score_list.Add(currentScore);
            FileHelper.SaveScoreList(score_list);
        }
    }
}