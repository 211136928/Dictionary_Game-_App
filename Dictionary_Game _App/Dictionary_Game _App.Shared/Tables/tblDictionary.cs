using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictionary_Game_App.Tables
{
   public class tblDictionary
    {
         [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string term { get; set; }

        public string defination { get; set; }
        public int langID { get; set; }

    }
}
