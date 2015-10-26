using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictionary_Game__App.Tables
{
    public class tblHighScore
    {
         [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string name { get; set; }
        public int score { get; set; }
        public string gameType { get; set; }

    }
}
