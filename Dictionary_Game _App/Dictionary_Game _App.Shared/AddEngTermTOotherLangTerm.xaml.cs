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
    public sealed partial class AddEngTermTOotherLangTerm : Page
    {
        public AddEngTermTOotherLangTerm()
        {
            this.InitializeComponent();
            combLanguages.Items.Add("Select language you want to add word");
            combLanguages.Items.Add("Afrikaans");
           // combLanguages.Items.Add("English");
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

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string engT, otherLangT;
            string msg = "";
            engT = txtEngTerm.Text;
            otherLangT=txtOtherLangTerm.Text;
            int index = combLanguages.SelectedIndex ;
            //chech if the textblock is not empty

            if(index == 1)
            {
                index--;
            }


            if (combLanguages.SelectedIndex != 0)
            {
                if (engT == "" || otherLangT == "")
                {
                    msg = "Plaese make sure all fields are filled";
                    messageBox(msg);
                }
                else
                {
                    tblTerminology objNew = new tblTerminology()
                    {
                        engTerm = engT,
                        otherLangTerm = otherLangT,
                        langID = index
                    };

                    await App.conn.InsertAsync(objNew);

                }
            }
            else
            {
                messageBox("please select the language you want to add term to");
                
            }


        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
           // this.Frame.Navigate(typeof(AddTermAndDefinationPage));

            lblDis.Text = combLanguages.SelectedIndex.ToString();
        }

        private void BtnPlayGame_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(GamePage)); 
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void radLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private async void messageBox(string msg)
        {
            var msgDisplay = new Windows.UI.Popups.MessageDialog(msg);
            await msgDisplay.ShowAsync();
        }
    }
}
