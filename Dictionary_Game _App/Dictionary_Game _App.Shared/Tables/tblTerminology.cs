using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictionary_Game_App.Tables
{
   public  class tblTerminology
    {
       [PrimaryKey, AutoIncrement]
        public int termID { get; set; }
        public string engTerm { get; set; }
        public string  otherLangTerm{ get; set; }
        public int langID { get; set; }

    }
}
