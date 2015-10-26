using Dictionary_Game__App;
using Dictionary_Game_App.Tables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
    public sealed partial class HardGame1Page : Page
    {
        string[] arrEng = new string[100] ;
        string[] arrOther = new string[100];
        string[] arrAnswer = new string[4];
        DispatcherTimer mytimer = new DispatcherTimer();
        int points = 40;
        int seconds = 10;
        int minutes = 6;
        int myPoint = 0;
        int gamePlayed = 0;
        int Numplay = 3;
        string name;
        public HardGame1Page()
        {
            this.InitializeComponent();
            radOtherLang_Eng1.IsChecked = true;
            btnNext.IsEnabled = false;
            btnPlay1.IsEnabled = false;
            btnViewAnswer.IsEnabled = false;
            btnReloadWords.IsEnabled = false;
         
            //call the method and populate the lebel whith data
            setWordLebal();
        
            mytimer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            mytimer.Tick += mytimer_Tick;

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var part = e.Parameter as string;
            lblName.Text = part;
            name = part.ToString();
        }

        private void mytimer_Tick(object sender, object e)
        {
           lblTimer.Text = seconds--.ToString();
           btnStart.IsEnabled = false;
         
           if (lblTimer.Text.Trim() == "0")
           {
               //viewanswer();
               stopTimer();
               if(myPoint < 19)
               {
                   if (Numplay == 3) { elpsLife1.Fill = new SolidColorBrush(Colors.Red); Numplay--; }
                   else if (Numplay == 2) { elpsLife2.Fill = new SolidColorBrush(Colors.Red); Numplay--; }
                   else if (Numplay == 1) { elpsLife3.Fill = new SolidColorBrush(Colors.Red); Numplay--;}
                   else { messageBox("Game over try again"); mytimer.Stop(); btnStart.IsEnabled = false; }
                   seconds = 10;minutes = 5;
                   btnStart.Content = "play again";
                   btnStart.IsEnabled = true;
                   lblMinutes.Text = 0.ToString();
              
               }
               else { btnStart.IsEnabled = false; btnNext.IsEnabled = true; }
               
             
           }

           if (seconds % 2 == 0 && lblTimer.Text != "0")
           {
               minutes--;
               lblMinutes.Text = minutes.ToString();
           }
        }

     

        private void stopTimer()
        {
            mytimer.Stop();
        }

       
        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
          // Brush Color = new Brush();
            bool ans = true;
            string msg = "";
            int rCount = 7;
           
            //checking the answer for text field 1
            if (txtAnswer1.IsEnabled == true)
            {
                if (txtAnswer1.Text.ToUpper().Trim() != arrAnswer[0].ToUpper().Trim())
                {
                    msg = "answer number 1 is incorrect \n"; ans = false; rCount--;
                    txtAnswer1.BorderBrush = new SolidColorBrush(Colors.Red);
                }
                else
                {
                    txtAnswer1.BorderBrush = new SolidColorBrush(Colors.Green);

                    if (lblHint1.Text == "")
                    {
                        myPoint += 10;
                    }
                    else { myPoint += 5; } txtAnswer1.IsEnabled = false;
                }
            }
            //checking the answer for text field 2

            if (txtAnswer2.IsEnabled == true)
            {
                if (txtAnswer2.Text.ToUpper().Trim() != arrAnswer[1].ToUpper().Trim())
                {
                    msg += "answer number 2 is incorrect \n"; ans = false; rCount--;
                    txtAnswer2.BorderBrush = new SolidColorBrush(Colors.Red);
                }
                else
                {
                    txtAnswer2.BorderBrush = new SolidColorBrush(Colors.Green);
                    // txtAnswer2.ForeColor = Colors.Green;
                    if (lblHint2.Text == "")
                    {
                        myPoint += 10;
                    }
                    else { myPoint += 5; } txtAnswer2.IsEnabled = false;
                }
            }
            //checking the answer for text field 3
            if (txtAnswer3.IsEnabled == true)
            {
                if (txtAnswer3.Text.ToUpper().Trim() != arrAnswer[2].ToUpper().Trim())
                {
                    msg += "answer number 3  is incorrect \n"; ans = false; rCount--;
                    txtAnswer3.BorderBrush = new SolidColorBrush(Colors.Red);
                }
                else
                {
                    txtAnswer3.BorderBrush = new SolidColorBrush(Colors.Green);
                    if (lblHint3.Text == "")
                    {
                        myPoint += 10;
                    }
                    else { myPoint += 5; }
                    txtAnswer3.IsEnabled = false;
                }
            }
            //checking the answer for text field 4
            if (txtAnswer4.IsEnabled == true)
            {
                if (txtAnswer4.Text.ToUpper().Trim() != arrAnswer[3].ToUpper().Trim())
                {
                    msg += "answer number 4 is incorrect \n"; ans = false; rCount--;
                    txtAnswer4.BorderBrush = new SolidColorBrush(Colors.Red);
                }
                else
                {
                    txtAnswer4.BorderBrush = new SolidColorBrush(Colors.Green);
                    if (lblHint4.Text == "")
                    {
                        myPoint += 10;
                    }
                    else { myPoint += 5; } txtAnswer4.IsEnabled = false;
                }
            }

            lblMypoints.Text = myPoint.ToString();
            if (rCount >= 4)
            {
                btnNext.IsEnabled = true;

                lblCongrtesMsg.Text = "You got " + rCount.ToString() + " correct you can go to the next round or play again to get all right";
            }

            if (ans == true)
            {
                messageBox("Congratulation You can go to the next round. YOU GOT ALL 10 ANSWER RIGHT");
                btnNext.IsEnabled = true;
                lblCongrtesMsg.Text = " Congratulation!!!! You got " + rCount.ToString() + " correct you can go to the next round ";
                btnReloadWords.IsEnabled = true;
                gamePlayed++;
                btnPlay1.IsEnabled = false;
                lblTimer.Text = myPoint.ToString();
            }
            else
            {
                if (chkMsgWrongAnswer.IsChecked == true)
                { messageBox(msg); }

            }


            /* if (txtAnswer1.Text.ToUpper().Trim() != arrAnswer[0].ToUpper().Trim())
             {
                 msg = "answer number 1 is incorrect \n"; ans = false;
                 txtAnswer1.BorderBrush = new SolidColorBrush(Colors.Red);
             }
             else { txtAnswer1.BorderBrush = new SolidColorBrush(Colors.Green); }

            
             if (txtAnswer2.Text.ToUpper().Trim() != arrAnswer[1].ToUpper().Trim())
             {
                 msg += "answer number 2 is incorrect \n"; ans = false;
                 txtAnswer2.BorderBrush = new SolidColorBrush(Colors.Red);
             }
             else { txtAnswer2.BorderBrush = new SolidColorBrush(Colors.Green); }
             if (txtAnswer3.Text.ToUpper().Trim() != arrAnswer[2].ToUpper().Trim())
             {
                 msg += "answer number 3  is incorrect \n"; ans = false;
                 txtAnswer3.BorderBrush = new SolidColorBrush(Colors.Red);
             }
             else { txtAnswer3.BorderBrush = new SolidColorBrush(Colors.Green); }
          
             if (txtAnswer4.Text.ToUpper().Trim() != arrAnswer[3].ToUpper().Trim())
             {
                 msg += "answer number 4 is incorrect \n"; ans = false;
                 txtAnswer4.BorderBrush = new SolidColorBrush(Colors.Red);
             }
             else{ txtAnswer4.BorderBrush = new SolidColorBrush(Colors.Green); }
          
             if(ans == true)
             {
                 messageBox("Congratulation You can go to the next round");
                 btnNext.IsEnabled = true;
             }else
             {
                 messageBox(msg);
             }*/

        }


        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(GamePage));
           // mytimer.Stop();
        }

        private void btnReloadWords_Click(object sender, RoutedEventArgs e)
        {
            
            txtAnswer1.Text = "";txtAnswer2.Text = "";txtAnswer3.Text = "";txtAnswer4.Text = "";
            lblHint1.Text = "";lblHint2.Text = "";lblHint3.Text = "";lblHint4.Text = "";

            txtAnswer1.BorderBrush = new SolidColorBrush(Colors.White); txtAnswer2.BorderBrush = new SolidColorBrush(Colors.White); txtAnswer3.BorderBrush = new SolidColorBrush(Colors.White);
            txtAnswer4.BorderBrush = new SolidColorBrush(Colors.White);
            txtAnswer1.IsEnabled = true; txtAnswer2.IsEnabled = true; txtAnswer3.IsEnabled = true; txtAnswer4.IsEnabled = true;
            setWordLebal();
            btnPlay1.IsEnabled = true;
        }
        private void btnViewAnswer_Click(object sender, RoutedEventArgs e)
        {
            txtAnswer1.Text = arrAnswer[0];
            txtAnswer2.Text =arrAnswer[1];
            txtAnswer3.Text =arrAnswer[2];
            txtAnswer4.Text = arrAnswer[3];
            btnPlay1.IsEnabled = true;
        }
     
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            mytimer.Start();
            btnPlay1.IsEnabled = true;
            btnViewAnswer.IsEnabled = true;
            btnReloadWords.IsEnabled = true;
            setWordLebal();
            txtAnswer1.Text = "";
            txtAnswer2.Text = "";
            txtAnswer3.Text ="";
            txtAnswer4.Text = "";
            minutes = 6;
        }
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            var pont = myPoint.ToString() + " "+ Numplay.ToString() + " "+name ;
            lblWord1.Text = pont.ToString();
            this.Frame.Navigate(typeof(HardGamePage2),pont);
        }

        private void combGameType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void radOtherLang_Eng_Checked(object sender, RoutedEventArgs e)
        {

            combGameType1.Items.Clear();
            combGameType1.Items.Add("Afrikaans_English");
            combGameType1.Items.Add("IsiNdebele_English");
            combGameType1.Items.Add("IsiXhosa_English");
            combGameType1.Items.Add("IsiZulu_English");
            combGameType1.Items.Add("Sesotho_English");
            combGameType1.Items.Add("Sesotho_English");
            combGameType1.Items.Add("Setswana_English");
            combGameType1.Items.Add("siSwati_English");
            combGameType1.Items.Add("Tshivenda_English");
            combGameType1.Items.Add("Xitsonga_English");
            combGameType1.SelectedIndex = 9;
            setWordLebal();

        }

        private void radEng_other_Lang_Checked(object sender, RoutedEventArgs e)
        {
          
            combGameType1.Items.Clear();
            combGameType1.Items.Add("English_Afrikaans");
            combGameType1.Items.Add("English_IsiNdebele");
            combGameType1.Items.Add("English_IsiXhosa");
            combGameType1.Items.Add("English_IsiZulu");
            combGameType1.Items.Add("English_Sesotho");
            combGameType1.Items.Add("English_Sesotho");
            combGameType1.Items.Add("English_Setswana");
            combGameType1.Items.Add("English_siSwati");
            combGameType1.Items.Add("English_Tshivenda");
            combGameType1.Items.Add("English_Xitsonga");
            combGameType1.SelectedIndex = 9;
            setWordLebal();

        }

        private void txtAnswer1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

      private async void setWordLebal()
        {
            lnkHint1.IsEnabled = true; lnkHint2.IsEnabled = true; lnkHint3.IsEnabled = true; lnkHint4.IsEnabled = true;
            lblHint1.Text = ""; lblHint2.Text = ""; lblHint3.Text = ""; lblHint4.Text = "";
            txtAnswer1.Text = ""; txtAnswer2.Text = ""; txtAnswer3.Text = ""; txtAnswer4.Text = "";
            int langID = combGameType1.SelectedIndex;//GET THE SELECTED INDEX
          
          if(langID != 0)
           {
               langID += 1;
           }
          
            string term, otherLangTerm;


            int x = 0;
           
          //selecting from the data base and sotre words to and arrays
          var AllTerm = await App.conn.QueryAsync<tblTerminology>("SELECT * FROM tblTerminology where langID = '"+langID+"'");
          if (AllTerm != null)
            {
                foreach (var objT in AllTerm)
                {
                    term = objT.engTerm;
                    otherLangTerm = objT.otherLangTerm;
                    if (x < 99)
                    {
                        arrEng[x] = term;
                        arrOther[x] = otherLangTerm;
                 
                        x++;
                    }
                }
            }
            else
            {
                messageBox("The is No  in the this Application dictionary. you can google the word and add it in the App");
            }
         // messageBox(x.ToString());
          Random myRandom = new Random();
           int  rNo ;
          
          //populate the label and answer in the array
           x--;
           string n = arrEng[5];
          if (radOtherLang_Eng1.IsChecked == false)
           {
               if (arrOther[0] != null)
               {
                   lblExample.Text = arrEng[0]; txtExample.Text = arrOther[0];
                   rNo = myRandom.Next(0, x);
                   lblWord1.Text = arrEng[rNo]; arrAnswer[0] = arrOther[rNo];
                   rNo = myRandom.Next(0, x);
                   lblWord2.Text = arrEng[rNo]; arrAnswer[1] = arrOther[rNo];
                   rNo = myRandom.Next(0, x);
                   lblWord3.Text = arrEng[rNo]; arrAnswer[2] = arrOther[rNo];
                   rNo = myRandom.Next(0, x);
                   lblWord4.Text = arrEng[rNo]; arrAnswer[3] = arrOther[rNo];
        
               }
           }
           else
           {
               if (x < arrEng.Length)
               {
                   lblExample.Text = arrOther[0]; txtExample.Text = arrEng[0];
                   rNo = myRandom.Next(0, x);
                   lblWord1.Text = arrOther[rNo]; arrAnswer[0] = arrEng[rNo];
                   rNo = myRandom.Next(0, x);
                   lblWord2.Text = arrOther[rNo]; arrAnswer[1] = arrEng[rNo];
                   rNo = myRandom.Next(0, x);
                   lblWord3.Text = arrOther[rNo]; arrAnswer[2] = arrEng[rNo];
                   rNo = myRandom.Next(0, x);
                   lblWord4.Text = arrOther[rNo]; arrAnswer[3] = arrEng[rNo];
               }
           }


        }

        private void viewanswer()
      {
          txtAnswer1.Text = arrAnswer[0];
          txtAnswer2.Text = arrAnswer[1];
          txtAnswer3.Text = arrAnswer[2];
          txtAnswer4.Text = arrAnswer[3];
          seconds = 10;
          minutes = 5;
      }
        private async void messageBox(string msg)
        {
            var msgDisplay = new Windows.UI.Popups.MessageDialog(msg);
            await msgDisplay.ShowAsync();
        }

        private void btnHint_Click(object sender, RoutedEventArgs e)
        {
          



        }

        private void lnkHint1_Click(object sender, RoutedEventArgs e)
        {
            lnkHint1.IsEnabled = false;
            points = points - 3;
            lblPoints.Text = points.ToString();
            string answer;
            int l;
            answer = arrAnswer[0];
            l = answer.Length - 1;
            lblHint1.Text = answer.Substring(0, 1) + "..." + answer.Substring(l, 1);
        }

        private void lnkHint2_Click(object sender, RoutedEventArgs e)
        {
            lnkHint2.IsEnabled = false;
            points = points - 3;
            lblPoints.Text = points.ToString();
            string answer;
            int l;
            answer = arrAnswer[1];
            l = answer.Length - 1;
            lblHint2.Text = answer.Substring(0, 1) + "..." + answer.Substring(l, 1);
        }

        private void lnkHint3_Click(object sender, RoutedEventArgs e)
        {
            points = points - 3;
            lblPoints.Text = points.ToString();
            string answer;
            int l;
            answer = arrAnswer[2];
            l = answer.Length - 1;
            lblHint3.Text = answer.Substring(0, 1) + "..." + answer.Substring(l, 1);
            lnkHint3.IsEnabled = false;
        }

        private void lnkHint4_Click(object sender, RoutedEventArgs e)
        {
            points = points - 3;
            lblPoints.Text = points.ToString();
            string answer;
            int l;
            answer = arrAnswer[3];
            l = answer.Length - 1;
            lblHint4.Text = answer.Substring(0, 1) + "..." + answer.Substring(l, 1);
            lnkHint4.IsEnabled = false;
        }

        private void chkMsgWrongAnswer_Checked(object sender, RoutedEventArgs e)
        {

        }

     
       

    }
}
