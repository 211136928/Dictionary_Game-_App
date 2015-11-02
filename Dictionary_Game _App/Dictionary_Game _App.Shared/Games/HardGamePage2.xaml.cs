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
using Windows.UI;
using Windows.UI.Popups;
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
    public sealed partial class HardGamePage2 : Page
    {
        string[] arrEng = new string[100];
        string[] arrOther = new string[100];
        string[] arrAnswer = new string[7];
        int pointsPlayFor = 70;
        int seconds = 30;
        int minutes = 3;
        int totalPoints = 0;
        int pointsScored = 0;
        int numPlay = 0;
        string nam;
        DispatcherTimer mytimer = new DispatcherTimer();
        public HardGamePage2()
        {
            this.InitializeComponent();
            //disable the component
            btnPlay.IsEnabled = false;// btnNext.IsEnabled = false;
            txtAnswer1.IsEnabled = false; txtAnswer2.IsEnabled = false; txtAnswer3.IsEnabled = false;
            txtAnswer4.IsEnabled = false; txtAnswer5.IsEnabled = false; txtAnswer6.IsEnabled = false;
            txtAnswer7.IsEnabled = false;
            radEnglishToOtherLang.IsChecked = true;
            setWordLebal();
            mytimer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            mytimer.Tick += mytimer_Tick;
            lblPoints.Text = pointsPlayFor.ToString();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var part =e.Parameter as string ;
            lblMinutes.Text = part.ToString();
           // myPoints = Convert.ToInt32(part);
            int pos =  part.IndexOf(" ");
 
            totalPoints = Convert.ToInt32( part.Substring(0, pos).Trim());
            lblTotalPoints.Text = pos.ToString();
            part = part.Remove(0, pos + 1);
            pos = part.IndexOf(" ");
           
            numPlay = Convert.ToInt32(part.Substring(0, pos).Trim());


            part = part.Remove(0, pos + 1);
            lblName.Text = part;
            nam = part;
            lblTotalPoints.Text = totalPoints.ToString()+ numPlay.ToString(); //myPoints.ToString();


            if (numPlay == 4) 
            { 
               // elpNum1.Fill = new SolidColorBrush(Colors.Green);
               // numPlay++;
            }
            else if (numPlay == 3) 
            {
               // elpNum2.Fill = new SolidColorBrush(Colors.Red);
                elpNum1.Fill = new SolidColorBrush(Colors.Red);
            }
            else if (numPlay == 2) 
            { 
               // elpNum3.Fill = new SolidColorBrush(Colors.Red);
                elpNum2.Fill = new SolidColorBrush(Colors.Red);
                elpNum1.Fill = new SolidColorBrush(Colors.Red);
            }
            else
            {
                elpNum3.Fill = new SolidColorBrush(Colors.Red);
                elpNum2.Fill = new SolidColorBrush(Colors.Red);
                elpNum1.Fill = new SolidColorBrush(Colors.Red);

            }
        }
        private async void mytimer_Tick(object sender, object e)
        {
            lblSecond.Text = seconds--.ToString();
            insertData objH = new insertData();
            bool isAdded;

            if (lblSecond.Text.Trim() == "0")
            {
                mytimer.Stop();
                if (numPlay == 4) { elpNum1.Fill = new SolidColorBrush(Colors.Red); numPlay--; }
                else if (numPlay == 3) { elpNum2.Fill = new SolidColorBrush(Colors.Red); numPlay--; }
                else if (numPlay == 2) { elpNum3.Fill = new SolidColorBrush(Colors.Red); numPlay--; }
                else if (numPlay == 1) { elpNum4.Fill = new SolidColorBrush(Colors.Red); numPlay--; }
                else if(numPlay == 0)
                {
                  messageBox("Game over try again"); //mytimer.Stop();
                 // btnStartGame.IsEnabled = false; 

                  isAdded = await  objH.insertHighScore(nam, totalPoints);
                    if(isAdded == true)
                    {
                       // messageBox("you have set a new high score reccond");
                    }
                 }
                    seconds = 5;
                    minutes--;
                   lblMinutes.Text = minutes.ToString();
            }

        }


     
        private void btnExitGame_Click(object sender, RoutedEventArgs e)
        {

            msg();

        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            pointsScored = 0;
            bool ans = true;
            int rCount = 0;
            string msg = "";
            //checking the answer for text field 1
            if (txtAnswer1.Text.ToUpper().Trim() != arrAnswer[0].ToUpper().Trim())
            {
                msg = "answer number 1 is incorrect \n";
                txtAnswer1.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else if(txtAnswer1.IsEnabled != false) {
                txtAnswer1.BorderBrush = new SolidColorBrush(Colors.Green);
                if (lblHint1.Text != "") { pointsScored += 10; } else { pointsScored += 5; } rCount++; totalPoints += pointsScored; txtAnswer1.IsEnabled = false;
                }


            //checking the answer for text field 2
            if (txtAnswer2.Text.ToUpper().Trim() != arrAnswer[1].ToUpper().Trim())
            {
                msg += "answer number 2 is incorrect \n"; 
                txtAnswer2.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else if (txtAnswer2.IsEnabled != false)
            {
                txtAnswer2.BorderBrush = new SolidColorBrush(Colors.Green);
                if (lblHint2.Text != "") { pointsScored += 10; } else { pointsScored += 5; } rCount++; totalPoints += pointsScored; txtAnswer2.IsEnabled = false;
            }
           
            //checking the answer for text field 3
            if (txtAnswer3.Text.ToUpper().Trim() != arrAnswer[2].ToUpper().Trim())
            {
                msg += "answer number 3  is incorrect \n"; 
                txtAnswer3.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else if (txtAnswer3.IsEnabled != false)
            {
                txtAnswer3.BorderBrush = new SolidColorBrush(Colors.Green);
                if (lblHint3.Text != "") { pointsScored += 10; } else { pointsScored += 5; } rCount++; totalPoints += pointsScored; txtAnswer3.IsEnabled = false;
            }
           
            //checking the answer for text field 4
            if (txtAnswer4.Text.ToUpper().Trim() != arrAnswer[3].ToUpper().Trim())
            {
                msg += "answer number 4 is incorrect \n";
                txtAnswer4.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else if (txtAnswer4.IsEnabled != false)
            {
                txtAnswer4.BorderBrush = new SolidColorBrush(Colors.Green);
                if (lblHint4.Text != "") { pointsScored += 10; } else { pointsScored += 5; } rCount++; totalPoints += pointsScored; txtAnswer4.IsEnabled = false;
            }
           
            //checking the answer for text field 5
            if (txtAnswer5.Text.ToUpper().Trim() != arrAnswer[4].ToUpper().Trim())
            {
                msg += "answer number 5 is incorrect \n";
                txtAnswer5.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else if (txtAnswer5.IsEnabled != false)
            {
                txtAnswer5.BorderBrush = new SolidColorBrush(Colors.Green);
                if (lblHint5.Text != "") { pointsScored += 10; } else { pointsScored += 5; } rCount++; totalPoints += pointsScored; txtAnswer5.IsEnabled = false;
            }
           
            //checking the answer for text field 6
            if (txtAnswer6.Text.ToUpper().Trim() != arrAnswer[5].ToUpper().Trim())
            {
                msg += "answer number 6 is incorrect \n";
                txtAnswer6.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else if (txtAnswer6.IsEnabled != false)
            {
                txtAnswer6.BorderBrush = new SolidColorBrush(Colors.Green);
                if (lblHint6.Text != "") { pointsScored += 10; } else { pointsScored += 5; } rCount++; totalPoints += pointsScored; txtAnswer6.IsEnabled = false;
            }
          
            //checking the answer for text field 7
            if (txtAnswer7.Text.ToUpper().Trim() != arrAnswer[6].ToUpper().Trim())
            {
                msg += "answer number 7 is incorrect \n"; 
                txtAnswer7.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else if (txtAnswer7.IsEnabled != false)
            {
                txtAnswer7.BorderBrush = new SolidColorBrush(Colors.Green);
                if (lblHint7.Text != "") { pointsScored += 10; } else { pointsScored += 5; } rCount++; totalPoints += pointsScored; txtAnswer7.IsEnabled = false;
            }
            
            if(rCount ==7)
            {
                lblCongrtesMsg.Text = "Congratulation!!!! You got " + rCount.ToString() + " correct you can go to the next round ";
                if (numPlay == 3) { elpNum1.Fill = new SolidColorBrush(Colors.Green); }
                else if (numPlay == 2) { elpNum2.Fill = new SolidColorBrush(Colors.Green); numPlay++; }
                else if (numPlay == 1) { elpNum3.Fill = new SolidColorBrush(Colors.Green); numPlay++; }
                else if (numPlay == 0) { elpNum4.Fill = new SolidColorBrush(Colors.Green); numPlay++; }
                btnNext.IsEnabled = true;
                mytimer.Stop();
            }
            else  if(rCount >= 4)
            { 
                   btnNext.IsEnabled = true;
                   lblCongrtesMsg.Text = "You got " + rCount.ToString() + " correct you can go to the next round or play again to get all answers correct"; 
            }
          
            if(chkDisplayAnswer.IsChecked == true)
            {  
                messageBox(msg);
            }

            lblCurrScore.Text = pointsScored.ToString();
           // myPoints += currentScore;
            lblTotalPoints.Text = totalPoints.ToString();
        }

        private void btnReloadWords_Click(object sender, RoutedEventArgs e)
        {
            setWordLebal();
        }
        private void btnViewAnswers_Click(object sender, RoutedEventArgs e)
        {

            txtAnswer1.Text = arrAnswer[0];
            txtAnswer2.Text = arrAnswer[1];
            txtAnswer3.Text = arrAnswer[2];
            txtAnswer4.Text = arrAnswer[3];
            txtAnswer5.Text = arrAnswer[4];
            txtAnswer6.Text = arrAnswer[5];
            txtAnswer7.Text = arrAnswer[6];


        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(HardGame1Page));
        }
        
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {

            var pont = totalPoints.ToString() + " " + numPlay.ToString() + " " + nam;
            this.Frame.Navigate(typeof(HardGamePage3),pont);
        }

        private void btnStartGame_Click(object sender, RoutedEventArgs e)
        {
            btnStartGame.Content = "Retart Game";
            mytimer.Start();
            seconds = 5;
            minutes = 7;
            btnPlay.IsEnabled = true;

                txtAnswer1.IsEnabled = true; txtAnswer2.IsEnabled = true; txtAnswer3.IsEnabled = true;
                txtAnswer4.IsEnabled = true; txtAnswer5.IsEnabled = true; txtAnswer6.IsEnabled = true;
                txtAnswer7.IsEnabled = true;
           
        }

        private void lnkHint1_Click(object sender, RoutedEventArgs e)
        {
            lnkHint1.IsEnabled = false;
            pointsPlayFor = pointsPlayFor - 3;
          lblPoints.Text = pointsPlayFor.ToString();
            string answer;
            int l;
            answer = arrAnswer[0];
            l = answer.Length - 1;
            lblHint1.Text = answer.Substring(0, 1) + "..." + answer.Substring(l, 1);
        }

        private void lnkHint2_Click(object sender, RoutedEventArgs e)
        {
            lnkHint2.IsEnabled = false;
             pointsPlayFor = pointsPlayFor - 3;
             lblPoints.Text = pointsPlayFor.ToString();
            string answer;
            int l;
            answer = arrAnswer[1];
            l = answer.Length - 1;
            lblHint2.Text = answer.Substring(0, 1) + "..." + answer.Substring(l, 1);
        }

        private void lnkHint3_Click(object sender, RoutedEventArgs e)
        {
            lnkHint3.IsEnabled = false;
             pointsPlayFor = pointsPlayFor - 3;
              lblPoints.Text = pointsPlayFor.ToString();
            string answer;
            int l;
            answer = arrAnswer[2];
            l = answer.Length - 1;
            lblHint3.Text = answer.Substring(0, 1) + "..." + answer.Substring(l, 1);
        }

        private void lnkHint4_Click(object sender, RoutedEventArgs e)
        {
            lnkHint4.IsEnabled = false;
             pointsPlayFor = pointsPlayFor - 3;
             lblPoints.Text = pointsPlayFor.ToString();
            string answer;
            int l;
            answer = arrAnswer[3];
            l = answer.Length - 1;
            lblHint4.Text = answer.Substring(0, 1) + "..." + answer.Substring(l, 1);
        }


        private void lnkHint5_Click_1(object sender, RoutedEventArgs e)
        {
            lnkHint5.IsEnabled = false;
             pointsPlayFor = pointsPlayFor - 3;
             lblPoints.Text = pointsPlayFor.ToString();
            string answer;
            int l;
            answer = arrAnswer[4];
            l = answer.Length - 1;
            lblHint5.Text = answer.Substring(0, 1) + "..." + answer.Substring(l, 1);
        }
     
        private void lnkHint6_Click_1(object sender, RoutedEventArgs e)
        {
            lnkHint6.IsEnabled = false;
             pointsPlayFor = pointsPlayFor - 3;
             lblPoints.Text = pointsPlayFor.ToString();
            string answer;
            int l;
            answer = arrAnswer[5];
            l = answer.Length - 1;
            lblHint6.Text = answer.Substring(0, 1) + "..." + answer.Substring(l, 1);
        }


        private void lnkHint7_Click(object sender, RoutedEventArgs e)
        {
            lnkHint7.IsEnabled = false;
             pointsPlayFor = pointsPlayFor - 3;
              lblPoints.Text = pointsPlayFor.ToString();
            string answer;
            int l;
            answer = arrAnswer[6];
            l = answer.Length - 1;
            lblHint7.Text = answer.Substring(0, 1) + "..." + answer.Substring(l, 1);
        }


        //this methos is for loading data to arrays from the data base;
    private async void setWordLebal()
        {
            lnkHint1.IsEnabled = true; lnkHint2.IsEnabled = true; lnkHint3.IsEnabled = true; lnkHint4.IsEnabled = true; lnkHint5.IsEnabled = true; lnkHint6.IsEnabled = true; lnkHint7.IsEnabled = true;
            lblHint1.Text = ""; lblHint2.Text = ""; lblHint3.Text = ""; lblHint4.Text = ""; lblHint5.Text = ""; lblHint6.Text = ""; lblHint7.Text = "";
            txtAnswer1.Text = ""; txtAnswer2.Text = ""; txtAnswer3.Text = ""; txtAnswer4.Text = ""; txtAnswer5.Text = ""; txtAnswer6.Text = ""; txtAnswer7.Text = ""; 
            int langID = combGameType.SelectedIndex;//GET THE SELECTED INDEX
           if(langID != 0)
           {
               langID += 1;
           }
          
            string term, otherLangTerm;


            int x = 0;
           
          var AllTerm = await App.conn.QueryAsync<tblTerminology>("SELECT * FROM tblTerminology where langID = '"+langID+"'");
         // int i = 0;
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
          if (radEnglishToOtherLang.IsChecked == false)
           {
               if (arrOther[0] != null)
               {
                  
                   rNo = myRandom.Next(0, x);
                   lblWord1.Text = arrEng[rNo]; arrAnswer[0] = arrOther[rNo];
                   rNo = myRandom.Next(0, x);
                   lblWord2.Text = arrEng[rNo]; arrAnswer[1] = arrOther[rNo];
                   rNo = myRandom.Next(0, x);
                   lblWord3.Text = arrEng[rNo]; arrAnswer[2] = arrOther[rNo];
                   rNo = myRandom.Next(0, x);
                   lblWord4.Text = arrEng[rNo]; arrAnswer[3] = arrOther[rNo];
                   rNo = myRandom.Next(0, x);
                   lblWord5.Text = arrEng[rNo]; arrAnswer[4] = arrOther[rNo];
                   rNo = myRandom.Next(0, x);
                   lblWord6.Text = arrEng[rNo]; arrAnswer[5] = arrOther[rNo];
                   rNo = myRandom.Next(0, x);
                   lblWord7.Text = arrEng[rNo]; arrAnswer[6] = arrOther[rNo];

        
               }
           }
           else
           {
               if (x < arrEng.Length)
               {
                  
                   rNo = myRandom.Next(0, x);
                   lblWord1.Text = arrOther[rNo]; arrAnswer[0] = arrEng[rNo];
                   rNo = myRandom.Next(0, x);
                   lblWord2.Text = arrOther[rNo]; arrAnswer[1] = arrEng[rNo];
                   rNo = myRandom.Next(0, x);
                   lblWord3.Text = arrOther[rNo]; arrAnswer[2] = arrEng[rNo];
                   rNo = myRandom.Next(0, x);
                   lblWord4.Text = arrOther[rNo]; arrAnswer[3] = arrEng[rNo];
                   rNo = myRandom.Next(0, x);
                   lblWord5.Text = arrOther[rNo]; arrAnswer[4] = arrEng[rNo];
                   rNo = myRandom.Next(0, x);
                   lblWord6.Text = arrOther[rNo]; arrAnswer[5] = arrEng[rNo];
                   rNo = myRandom.Next(0, x);
                   lblWord7.Text = arrOther[rNo]; arrAnswer[6] = arrEng[rNo];
               }
           }


        }
private async void messageBox(string msg)
        {
            var msgDisplay = new Windows.UI.Popups.MessageDialog(msg);
            await msgDisplay.ShowAsync();
        }
private async void msg()
{
    insertData objH = new insertData();
    bool isAdded;
    var messgeDialog = new MessageDialog("are you sure you want to exit this game?");
    messgeDialog.Commands.Add(new UICommand("Yes"));
    messgeDialog.Commands.Add(new UICommand("No"));
    messgeDialog.DefaultCommandIndex = 0;
    messgeDialog.CancelCommandIndex = 1;
    var result = await messgeDialog.ShowAsync();
    if (result.Label.Equals("Yes"))
     {
         isAdded = await objH.insertHighScore(nam, totalPoints);
        this.Frame.Navigate(typeof(GamePage));
     }
    
}

private void radOtherLang_English_Checked(object sender, RoutedEventArgs e)
{
    combGameType.Items.Clear();
    combGameType.Items.Add("Afrikaans_English");
    combGameType.Items.Add("IsiNdebele_English");
    combGameType.Items.Add("IsiXhosa_English");
    combGameType.Items.Add("IsiZulu_English");
    combGameType.Items.Add("Sesotho_English");
    combGameType.Items.Add("Sesotho_English");
    combGameType.Items.Add("Setswana_English");
    combGameType.Items.Add("siSwati_English");
    combGameType.Items.Add("Tshivenda_English");
    combGameType.Items.Add("Xitsonga_English");
    combGameType.SelectedIndex = 9;
    setWordLebal();

}

private void radEnglishToOtherLang_Checked(object sender, RoutedEventArgs e)
{
    combGameType.Items.Clear();
    combGameType.Items.Add("English_Afrikaans");
    combGameType.Items.Add("English_IsiNdebele");
    combGameType.Items.Add("English_IsiXhosa");
    combGameType.Items.Add("English_IsiZulu");
    combGameType.Items.Add("English_Sesotho");
    combGameType.Items.Add("English_Sesotho");
    combGameType.Items.Add("English_Setswana");
    combGameType.Items.Add("English_siSwati");
    combGameType.Items.Add("English_Tshivenda");
    combGameType.Items.Add("English_Xitsonga");
    combGameType.SelectedIndex = 9;
    setWordLebal();

}

private void combGameType_SelectionChanged(object sender, SelectionChangedEventArgs e)
{

}

private void lblMinutes_SelectionChanged(object sender, RoutedEventArgs e)
{

}






    }
}
