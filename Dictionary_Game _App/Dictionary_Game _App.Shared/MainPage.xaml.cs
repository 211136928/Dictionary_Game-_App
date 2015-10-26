using Dictionary_Game_App.Games;
using Dictionary_Game_App.Tables;
using SQLite;
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

namespace Dictionary_Game__App
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        SQLiteAsyncConnection conn = new SQLiteAsyncConnection("Dictionary.db");
       
        public MainPage()
        {
            this.InitializeComponent();

         
        }


        private async void ReadTextFileAsync()
        {
           
            //reading a txt file that contains term and difination
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(@"ms-appx:///afri_eng.txt"));
            StreamReader sRead = new StreamReader(await file.OpenStreamForReadAsync());
            string line, term, defination;

            //getting a line 
            line = sRead.ReadLine();
            int pos;
            string msg = "";
            //loop for each line
            while (line != null)
            {
                if (line != null)//check if line is not null 
                {
                    pos = line.IndexOf("\t");
                    term = line.Substring(0, pos).Trim();
                    line = line.Remove(0, pos + 1);
                    defination = line.Trim();
                    msg += "\n " + term + "               " + defination;
                    setTerminologyAsync(term, defination);
                }
                line = sRead.ReadLine();
            }

            lstDisplay.Items.Add(msg);

        }
        private async void setTerminologyAsync(string otherTerm, string engTerm)
        {

            tblTerminology newTerm = new tblTerminology()
            {
                engTerm = engTerm,
                otherLangTerm = otherTerm,
                langID = 0
            };

            // SQLiteAsyncConnection conn = new SQLiteAsyncConnection("Lang_Dict.db");
            await conn.InsertAsync(newTerm);

        }

        private async void setDataInDatabaseAsync(string term, string dif, int LangId)
        {
            //insert into a database
            tblDictionary newRes = new tblDictionary()
            {
                term = term,
                defination = dif,
                langID = LangId
            };
            // SQLiteAsyncConnection conn = new SQLiteAsyncConnection("Lang_Dict.db");
            await conn.InsertAsync(newRes);
        }


        private async void btnPlayGame_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(GamePage));
        }

        private void btnAddTerm_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddTermAndDefinationPage));
        }

        private void btnAddTeminology_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddEngTermTOotherLangTerm));
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string word = txtSearch.Text;
            displaySeachWord(word);
        }
        private async void displaySeachWord(string sWord)
        {
            lstDisplay.Items.Clear();
            lstDisplay.Items.Add("Term    Difination");
            lstDisplay.Items.Add("");
            sWord = sWord + "%";
            int langID = 1;
            string term, defination;

            var AllTerm = await App.conn.QueryAsync<tblDictionary>("SELECT * FROM tblDictionary where langID = '" + langID + "' AND  term like'" + sWord + "'");
            if (AllTerm != null)
            {
                foreach (var objT in AllTerm)
                {
                    term = objT.term;
                    term.ToUpper();
                    defination = objT.defination;
                    lstDisplay.Items.Add(term.PadRight(30 - term.Length) + "\t\t\t" + defination);
                }
            }
            else
            {
                messageBox("The is No " + sWord + " in the this Application dictionary. you can google the word and add it in the App");
            } 
        }
            private async void messageBox(string msg)
            {
                var msgDisplay = new Windows.UI.Popups.MessageDialog(msg);
                await msgDisplay.ShowAsync();
            }
       
    }
}
