using System;
using System.Collections.Generic;
using System.Text;

namespace Dictionary_Game_App.Tables
{
   public class tblUserTerminology
    {
        public int TermID { get; set; }

        public string eng_Term { get; set; }

        public string otherLangTerm { get; set; }
        public int langID { get; set; }
    }
}
