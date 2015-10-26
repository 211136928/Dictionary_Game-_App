using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictionary_Game_App.Tables
{
    public class tblLanguage
    {
        [PrimaryKey]
        public int langID { get; set; }
        public string language { get; set; }
    }
}
