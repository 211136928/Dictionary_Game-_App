using Dictionary_Game_App.Games;
using Dictionary_Game_App.Tables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class AddTermAndDefinationPage : Page
    {
        public AddTermAndDefinationPage()
        {
            this.InitializeComponent();

            combLanguages.Items.Add("Select language you want to add word"); 
            combLanguages.Items.Add("Afrikaans");
            combLanguages.Items.Add("English");
            combLanguages.Items.Add("isiNdebele");
            combLanguages.Items.Add("isiXhosa");
            combLanguages.Items.Add("isiZulu");
            combLanguages.Items.Add("Sepedi");
            combLanguages.Items.Add("Sesotho");
            combLanguages.Items.Add("Setswana");
            combLanguages.Items.Add("siSwati");
            combLanguages.Items.Add("Tshivenda");
            combLanguages.Items.Add("Xitsonga");
            combLanguages.SelectedIndex = 0;
        }

      

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
           // this.Frame.Navigate(typeof(MainPage));

            lblDisplay.Text = (combLanguages.SelectedIndex- 1).ToString();
        }

        private void btnAddTerminology_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void btnPlayGame_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(GamePage));
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
        

            int index = combLanguages.SelectedIndex - 1;
            string trm, definition;

            trm = txtTerm.Text;
            definition = txtDefinition.Text;

          if(trm.Contains("123456789") )
          {
              messageBox("NUMERIC VALUES");
          }
            
                if (index == -1)
                {
                    messageBox("please select the language you want to add word and definition to");
                }else
                    if (trm == "" || definition == "")
                    {
                        messageBox("Please fill all the field");
                    }
                    else
                    {

                        tblDictionary objNewWord = new tblDictionary()
                        {
                            term = trm,
                            defination = definition,
                            langID = index
                        };
                       //await App.conn.InsertAsync(objNewWord);
                    }
               
            
        }
        private async void messageBox(string msg)
        {
            var msgDisplay = new Windows.UI.Popups.MessageDialog(msg);
            await msgDisplay.ShowAsync();
        }
    }
}
