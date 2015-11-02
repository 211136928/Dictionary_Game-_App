
using Dictionary_Game__App;
using Dictionary_Game__App.Tables;
using Dictionary_Game_App.Tables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Dictionary_Game_App.Games
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {
        string[] arrLan = new string[] { "Afrikaans", "English", "isiNdebele", "isiXhosa", "isiZulu", "Sepedi", "Sesotho", "Setswana", "siSwati", "Tshivenda", "Xitsonga" };
        public GamePage()
        {
            this.InitializeComponent();
         
            for(int x = 0 ; x < 11 ; x++)
            {
                combLanguage.Items.Add(arrLan[x]);
            }
            combLanguage.SelectedIndex = 10;
            radSearchEnglishWord.IsChecked = true;
          //ReadTextFileAsync();

            lstHighScore.Items.Add("Name        Score   Game Type");
            lstHighScore.Items.Add("");
            lstHighScore.Items.Add("wisani      360     Hard");
            lstHighScore.Items.Add("Brain       450     Easy");
            lstHighScore.Items.Add("Nkuna       100     Hard");

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            //this.Frame.Navigate(typeof(HardGamePage3));
        }

     
        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searshWord = txtSearch.Text;
            displaySeachWord(searshWord);
        }


        private async void ReadTextFileAsync()
        {
   
            //reading a txt file that contains term and difination
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(@"ms-appx:///xits_eng.txt"));
            StreamReader sRead = new StreamReader(await file.OpenStreamForReadAsync());
            string line, term, lTerm;

            //getting a line 
            line = sRead.ReadLine();
            int pos;

            //loop for each line
            while (line != null)
            {
                if (line != null)//check if line is not null 
                {
                    pos = line.IndexOf(";");
                    term = line.Substring(0, pos).Trim();
                    line = line.Remove(0, pos + 1);
                    lTerm = line.Trim();
                    setDataInDatabaseAsync(term, lTerm);
                }
                line = sRead.ReadLine();
            }

        }
        //thid method will set the data into table dictionary
        private async void setDataInDatabaseAsync(string term, string lTerm)
        {
            //insert into a database
            int LangId = 10;
            tblTerminology newRes = new tblTerminology()
            {
                engTerm = lTerm,
                otherLangTerm = term,
                langID = LangId
            };
            // SQLiteAsyncConnection conn = new SQLiteAsyncConnection("Lang_Dict.db");
            await App.conn.InsertAsync(newRes);
        }

        private void radSearchEnglishWord_Checked(object sender, RoutedEventArgs e)
        {
            lblSearch.Text = "Search English word";
        }

        private void btnSearchOtherLangWord_Checked(object sender, RoutedEventArgs e)
        {
            var lang = combLanguage.SelectedValue;
          
          
            lblSearch.Text = "Search  word ";
        }

        private void btnSearchOtherLangWord_Checked_1(object sender, RoutedEventArgs e)
        {
            var lang = combLanguage.SelectedValue;
            lblSearch.Text = "Search  word in "+lang;

        }

        private void combLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var lang = combLanguage.SelectedValue;

            lblDisplay.Text = lang + " to English";

        }
        private async void displaySeachWord(string sWord)
        {
            lstDisplay.Items.Clear();
            lstDisplay.Items.Add("Term                        Term");
            lstDisplay.Items.Add("");
            sWord = sWord + "%";
            int langID = combLanguage.SelectedIndex;//GET THE SELECTED INDEX
            bool isEng = false;
            if(radSearchEnglishWord.IsChecked== true)
            {
                isEng = true;
            }

            string EngTerm, otherTerm;

            var AllTerm = await App.conn.QueryAsync<tblTerminology>("SELECT * FROM tblTerminology where langID = '" + langID + "' AND  engTerm like'" + sWord + "'");
            var AllTerm1 = await App.conn.QueryAsync<tblTerminology>("SELECT * FROM tblTerminology where langID = '" + langID + "' AND  otherLangTerm like'" + sWord + "'");
           
            if(isEng == true){
                if (AllTerm != null)
                {
                    foreach (var objT in AllTerm)
                    {
                        EngTerm = objT.engTerm;
                        otherTerm = objT.otherLangTerm;
                         lstDisplay.Items.Add(EngTerm.PadRight(30-EngTerm.Length) + "\t\t" + otherTerm); 
                      
                    }
                }
            }
            else{
                foreach (var objT in AllTerm1)
                {
                    EngTerm = objT.engTerm;
                    otherTerm = objT.otherLangTerm;
                     lstDisplay.Items.Add(otherTerm.PadRight(30 - otherTerm.Length) + "                " + EngTerm.PadRight(100)); 
                }

            }

        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            var name = txtName.Text;
            if(txtName.Text != "")
            {
                if (radHard_Game.IsChecked == true)
                {

                    this.Frame.Navigate(typeof(HardGame1Page ),name);
                }
                else if (radEasy_Game.IsChecked == true) { this.Frame.Navigate(typeof(SimpleGame1Page),name); }
                else { messageBox("Please select the type of game you want to play"); };
            }
            else { messageBox("Please fill in your name"); }
          
        }


        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void radEasy_Game_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void radHard_Game_Checked(object sender, RoutedEventArgs e)
        {

        }
        private async void messageBox(string msg)
        {
            var msgDisplay = new Windows.UI.Popups.MessageDialog(msg);
            await msgDisplay.ShowAsync();
        }

        private async void btnViewHighScore_Click(object sender, RoutedEventArgs e)
        {    lstHighScore.Items.Clear();
            lstHighScore.Items.Add("Name\tScore\tScore\tId");
            string[] arrName = new string[21];
            int[] arrScore = new int[21];
            int[] arrID = new int[21];
            string Nam = "Wisani";
            int score = 10;

          int  count = 0; 
            var AllTerm = await App.conn.QueryAsync<tblHighScore>("SELECT * FROM tblHighScore");
            if (AllTerm != null)
            {
                foreach (var objT in AllTerm)
                {
                        arrName[count] = objT.name;
                        arrScore[count] = objT.score;
                        arrID[count] = objT.ID;
                   // lstHighScore.Items.Add(objT.name + "    "+objT.score+"  "+objT.ID +"    "+ objT.gameType);
                        count++;
                }
            }
            else { messageBox("Bable is empty"); }

       

            for(int x = 0; x < 21; x++)
            {
                int temp = 0;
                string nam = "";

                for(int y = 0;y < 21; y++)
                {
                    if(arrScore[x] > arrScore[y])
                    {
                        temp = arrScore[x];
                        nam = arrName[x];
                        arrName[x] = arrName[y];
                        arrScore[x] = arrScore[y];
                        arrName[y] = nam;
                        arrScore[y] = temp;
                    }
                }

            }

            for(int y = 0; y< 21;y++)
             {
                  lstHighScore.Items.Add(arrName[y] +"\t"+arrScore[y]+"    "+arrID[y]); 


                //update the score

               /*  var user = await App.conn.Table<tblHighScore>().Where(x => x.name == "wisani8").FirstOrDefaultAsync();
                 if (user != null)
                 {
                     // Modify user
                     user.score = 50;
                     user.name = "Wisani";//+ y.ToString();

                     // Update record
                     await App.conn.UpdateAsync(user);
                 }*/

            }

            
             /* for(int y = 0; y< 10;y++)
                {
                     lstHighScore.Items.Add(arrName[y] + "\t" + arrScore[y]);


                   tblHighScore objnewHighScore = new tblHighScore()
                   {
                       name = Nam,
                        score = score,
                        gameType = "Easy"
                    };
                   await App.conn.InsertAsync(objnewHighScore);
                  Nam ="wisani"+ y.ToString();
                  score = score *( y+1);
               }*/


        }

    }
}
