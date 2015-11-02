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
    public sealed partial class HardGamePage3 : Page
    {
        string[] arrEng = new string[100];
        string[] arrOther = new string[100];
        string[] arrAnswer = new string[10];
        int pointsScored = 0;
        int pointsPlayFor = 100;
        int seconds = 25;
        int minutes = 2;
        int TotalPoints = 0;
        int gamePlayed = 0;
        int numPlay = 0;
        string nam;
        DispatcherTimer mytimer = new DispatcherTimer();
        public HardGamePage3()
        {
            this.InitializeComponent();
            radOtherLang_Eng.IsChecked = true;
            btnPlay.IsEnabled = false; 
            txtAnswer1.IsEnabled = false; txtAnswer2.IsEnabled = false; txtAnswer3.IsEnabled = false;
            txtAnswer4.IsEnabled = false; txtAnswer5.IsEnabled = false; txtAnswer6.IsEnabled = false;
            txtAnswer7.IsEnabled = false; txtAnswer8.IsEnabled = false; txtAnswer9.IsEnabled = false; txtAnswer10.IsEnabled = false;
            mytimer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            mytimer.Tick += mytimer_Tick;
            lblMinutes.Text = minutes.ToString();
            lblSecond.Text = seconds.ToString();
            lblPointsPlayFor.Text = pointsPlayFor.ToString();
            
            //call the method and populate the lebel whith data
            setWordLebal();

            mytimer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            mytimer.Tick += mytimer_Tick;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var part = e.Parameter as string;
            //lblMinutes.Text = part.ToString();
            // myPoints = Convert.ToInt32(part);
            int pos = part.IndexOf(" ");

            TotalPoints = Convert.ToInt32(part.Substring(0, pos).Trim());
            lblTotalPoints.Text = pos.ToString();
            part = part.Remove(0, pos + 1);
            pos = part.IndexOf(" ");

            numPlay = Convert.ToInt32(part.Substring(0, pos).Trim());
            part = part.Remove(0, pos + 1);
            lblName.Text = part;
            nam = part;
            lblTotalPoints.Text = TotalPoints.ToString(); //myPoints.ToString();
            lblName.Text = nam;

            if (numPlay == 3)
            {
                elpNum1.Fill = new SolidColorBrush(Colors.Red);
                elpNum2.Fill = new SolidColorBrush(Colors.Red);
            }
            else if (numPlay == 2)
            {
                elpNum3.Fill = new SolidColorBrush(Colors.Red);
                elpNum2.Fill = new SolidColorBrush(Colors.Red);
                elpNum1.Fill = new SolidColorBrush(Colors.Red);
            }
            else if (numPlay == 1)
            {
                elpNum4.Fill = new SolidColorBrush(Colors.Red);
                elpNum3.Fill = new SolidColorBrush(Colors.Red);
                elpNum2.Fill = new SolidColorBrush(Colors.Red);
                elpNum1.Fill = new SolidColorBrush(Colors.Red);
            }
        }
        private async void mytimer_Tick(object sender, object e)
        {
            bool isAdded;
            insertData objH = new insertData();
            lblSecond.Text = seconds--.ToString();
            lblMinutes.Text = minutes.ToString();

            if (lblSecond.Text.Trim() == "0")
            {
                mytimer.Stop();
                if(numPlay == 5 ){ elpNum1.Fill = new SolidColorBrush(Colors.Red); numPlay--;}
                else if (numPlay == 4) { elpNum2.Fill = new SolidColorBrush(Colors.Red); numPlay--; }
                else if (numPlay == 3) { elpNum3.Fill = new SolidColorBrush(Colors.Red); numPlay--; }
                else if (numPlay == 2) { elpNum4.Fill = new SolidColorBrush(Colors.Red); numPlay--; }
                else if (numPlay == 1) { elpNum5.Fill = new SolidColorBrush(Colors.Red); numPlay--; }
             
                else
                {
                   
                    btnStart.IsEnabled = false;

                    isAdded = await objH.insertHighScore(nam, TotalPoints);
                    messageBox("Game over try again"); //mytimer.Stop();
                }
                seconds = 25;
            }

            if (seconds % 10 == 0)
            {
                minutes--;
                lblMinutes.Text = minutes.ToString();
            }
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            bool ans = true;
            int rCount = 10;
            string msg = "";
            //checking the answer for text field 1
            if (txtAnswer1.IsEnabled == true)
            {
                if (txtAnswer1.Text.ToUpper().Trim() != arrAnswer[0].ToUpper().Trim())
                {
                    msg = "Answer number 1 is incorrect \n"; ans = false; rCount--;
                    txtAnswer1.BorderBrush = new SolidColorBrush(Colors.Red);
                }
                else
                {
                    txtAnswer1.BorderBrush = new SolidColorBrush(Colors.Green);

                    if (lblHint1.Text == "")
                    {
                        TotalPoints += 10;
                        pointsScored += 10;
                    }
                    else { TotalPoints += 5; pointsScored += 5; } txtAnswer1.IsEnabled = false;
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
                        TotalPoints += 10;
                        pointsScored += 10;
                    }
                    else { TotalPoints += 5; pointsScored += 5; } txtAnswer2.IsEnabled = false;
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
                        TotalPoints += 10;
                        pointsScored += 10;
                    }
                    else { TotalPoints += 5; pointsScored += 5; }
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
                        TotalPoints += 10;
                        pointsScored += 10;
                    }
                    else { TotalPoints += 5; pointsScored += 5; } txtAnswer4.IsEnabled = false;
                }
            }
            //checking the answer for text field 5
            if (txtAnswer5.IsEnabled == true)
            {
                if (txtAnswer5.Text.ToUpper().Trim() != arrAnswer[4].ToUpper().Trim())
                {
                    msg += "answer number 5 is incorrect \n"; ans = false; rCount--;
                    txtAnswer5.BorderBrush = new SolidColorBrush(Colors.Red);
                }
                else
                {
                    txtAnswer5.BorderBrush = new SolidColorBrush(Colors.Green);
                    if (lblHint5.Text == "")
                    {
                        TotalPoints += 10;
                        pointsScored += 10;
                    }
                    else { TotalPoints += 5; pointsScored += 5; } txtAnswer5.IsEnabled = false;

                }
            }
            //checking the answer for text field 6
            if (txtAnswer6.IsEnabled == true)
            {
                if (txtAnswer6.Text.ToUpper().Trim() != arrAnswer[5].ToUpper().Trim())
                {
                    msg += "answer number 6 is incorrect \n"; ans = false; rCount--;
                    txtAnswer6.BorderBrush = new SolidColorBrush(Colors.Red);
                }
                else
                {

                    txtAnswer6.BorderBrush = new SolidColorBrush(Colors.Green);
                    if (lblHint6.Text == "")
                    {
                        TotalPoints += 10;
                        pointsScored += 10;
                    }
                    else { TotalPoints += 5; pointsScored += 5; } txtAnswer6.IsEnabled = false;

                }
            }
            //checking the answer for text field 7
            if (txtAnswer7.IsEnabled == true)
            {
                if (txtAnswer7.Text.ToUpper().Trim() != arrAnswer[6].ToUpper().Trim())
                {
                    msg += "answer number 7 is incorrect \n"; ans = false; rCount--;
                    txtAnswer7.BorderBrush = new SolidColorBrush(Colors.Red);
                }

                else
                {
                    txtAnswer7.BorderBrush = new SolidColorBrush(Colors.Green);
                    if (lblHint7.Text == "")
                    {
                        TotalPoints += 10;
                        pointsScored += 10;
                    }
                    else { TotalPoints += 5; pointsScored += 5; } txtAnswer7.IsEnabled = false;
                }
            }
            //checking the answer for text field 8
            if (txtAnswer8.IsEnabled == true)
            {
                if (txtAnswer8.Text.ToUpper().Trim() != arrAnswer[7].ToUpper().Trim())
                {
                    msg += "answer number 8 is incorrect \n"; ans = false; rCount--;
                    txtAnswer8.BorderBrush = new SolidColorBrush(Colors.Red);
                }
                else 
                { 
                    txtAnswer8.BorderBrush = new SolidColorBrush(Colors.Green);
                    if (lblHint8.Text == "")
                    {
                        TotalPoints += 10;
                        pointsScored += 10;
                    }
                    else { TotalPoints += 5; pointsScored += 5; } txtAnswer8.IsEnabled = false;
                }
            }
            //checking the answer for text field 9
            if (txtAnswer9.IsEnabled == true)
            {
                if (txtAnswer9.Text.ToUpper().Trim() != arrAnswer[8].ToUpper().Trim())
                {
                    msg += "answer number 9 is incorrect \n"; ans = false; rCount--;
                    txtAnswer9.BorderBrush = new SolidColorBrush(Colors.Red);
                }
                else {
                    txtAnswer9.BorderBrush = new SolidColorBrush(Colors.Green);
                       if (lblHint9.Text == "")
                    {
                        TotalPoints += 10;
                        pointsScored += 10;
                    }
                       else { TotalPoints += 5; pointsScored += 5; } txtAnswer9.IsEnabled = false;
                }
            }
            //checking the answer for text field 10
            if (txtAnswer10.IsEnabled == true)
            {
                if (txtAnswer10.Text.ToUpper().Trim() != arrAnswer[9].ToUpper().Trim())
                {
                    msg += "answer number 10 is incorrect \n"; ans = false; rCount--;
                    txtAnswer10.BorderBrush = new SolidColorBrush(Colors.Red);
                }
                else {
                    txtAnswer10.BorderBrush = new SolidColorBrush(Colors.Green);
                    if (lblHint10.Text == "")
                    {
                        TotalPoints += 10;
                        pointsScored += 10;
                    }
                    else { TotalPoints += 5; pointsScored += 5; } txtAnswer10.IsEnabled = false;
                }
            }

            lblPointsScored.Text = pointsScored.ToString();
            lblTotalPoints.Text = TotalPoints.ToString();


            if (rCount == 10)
            {
                lblCongrtesMsg.Text = "Congratulation!!!! You got " + rCount.ToString() + " correct you can go to the next round ";
                if (numPlay == 4) { elpNum1.Fill = new SolidColorBrush(Colors.Green); numPlay++; }
                else if (numPlay == 3) { elpNum2.Fill = new SolidColorBrush(Colors.Green); numPlay++; }
                else if (numPlay == 2) { elpNum3.Fill = new SolidColorBrush(Colors.Green); numPlay++; }
                else if (numPlay == 1) { elpNum4.Fill = new SolidColorBrush(Colors.Green); numPlay++; }
                else if (numPlay == 0) { elpNum5.Fill = new SolidColorBrush(Colors.Green); numPlay++; }
                minutes = 2;
                seconds = 25;
                btnStart.IsEnabled = false;
                mytimer.Stop();
            }




            if (ans == true)
            {
                messageBox("Congratulation You can go to the next round. YOU GOT ALL 10 ANSWER RIGHT");
               
                lblCongrtesMsg.Text = " Congratulation!!!! You got " + rCount.ToString() + " correct you can go to the next round ";
                btnReloadWords.IsEnabled = true;
                gamePlayed++;
                btnPlay.IsEnabled = false;
            }
            else
            {
                if (chkMsgWrongAnswer.IsChecked == true)
                { messageBox(msg);}
                
            }
        }

        private void btnReloadWords_Click(object sender, RoutedEventArgs e)
        {
            setWordLebal();
            txtAnswer1.IsEnabled = true; txtAnswer2.IsEnabled = true; txtAnswer3.IsEnabled = true; txtAnswer4.IsEnabled = true; txtAnswer5.IsEnabled = true;
            txtAnswer6.IsEnabled = true; txtAnswer7.IsEnabled = true; txtAnswer8.IsEnabled = true; txtAnswer9.IsEnabled = true; txtAnswer10.IsEnabled = true;

            txtAnswer1.BorderBrush = new SolidColorBrush(Colors.White); txtAnswer2.BorderBrush = new SolidColorBrush(Colors.White); txtAnswer3.BorderBrush = new SolidColorBrush(Colors.White);
            txtAnswer4.BorderBrush = new SolidColorBrush(Colors.White); txtAnswer5.BorderBrush = new SolidColorBrush(Colors.White); txtAnswer6.BorderBrush = new SolidColorBrush(Colors.White);
            txtAnswer7.BorderBrush = new SolidColorBrush(Colors.White); txtAnswer8.BorderBrush = new SolidColorBrush(Colors.White); txtAnswer9.BorderBrush = new SolidColorBrush(Colors.White);
            txtAnswer10.BorderBrush = new SolidColorBrush(Colors.White);
            btnReloadWords.IsEnabled = false;
            btnPlay.IsEnabled =  false;
            btnStart.IsEnabled = true;
            pointsScored = 0;
            pointsPlayFor = 100;
            lblPointsPlayFor.Text = pointsPlayFor.ToString();
            lblPointsScored.Text = pointsScored.ToString();
        }

      

    
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            mytimer.Start();


            mytimer.Start();
            btnPlay.IsEnabled = true;
            txtAnswer1.IsEnabled = true; txtAnswer2.IsEnabled = true; txtAnswer3.IsEnabled = true;
            txtAnswer4.IsEnabled = true; txtAnswer5.IsEnabled = true; txtAnswer6.IsEnabled = true;
            txtAnswer7.IsEnabled = true; txtAnswer8.IsEnabled = true; txtAnswer9.IsEnabled = true; txtAnswer10.IsEnabled = true;

            btnStart.Content = "Retart again";


        }

        private void btnViewAnswer_Click(object sender, RoutedEventArgs e)
        {
            txtAnswer1.Text = arrAnswer[0];
            txtAnswer2.Text = arrAnswer[1];
            txtAnswer3.Text = arrAnswer[2];
            txtAnswer4.Text = arrAnswer[3];
            txtAnswer5.Text = arrAnswer[4];
            txtAnswer6.Text = arrAnswer[5];
            txtAnswer7.Text = arrAnswer[6];
            txtAnswer8.Text = arrAnswer[7];
            txtAnswer9.Text = arrAnswer[8];
            txtAnswer10.Text = arrAnswer[9];
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
            var messgeDialog = new MessageDialog("Are you sure you want to exit this game?");
            messgeDialog.Commands.Add(new UICommand("Yes"));
            messgeDialog.Commands.Add(new UICommand("No"));
            messgeDialog.DefaultCommandIndex = 0;
            messgeDialog.CancelCommandIndex = 1;
            var result = await messgeDialog.ShowAsync();
            if (result.Label.Equals("Yes"))
            {
                isAdded = await objH.insertHighScore(nam, TotalPoints);
                this.Frame.Navigate(typeof(GamePage));
            }

        }
        //this methos is for loading data to arrays from the data base;
        private async void setWordLebal()
        {
            lnkHint1.IsEnabled = true; lnkHint2.IsEnabled = true; lnkHint3.IsEnabled = true; lnkHint4.IsEnabled = true; lnkHint5.IsEnabled = true; lnkHint6.IsEnabled = true; lnkHint7.IsEnabled = true; lnkHint8.IsEnabled = true; lnkHint9.IsEnabled = true; lnkHint10.IsEnabled = true;
            lblHint1.Text = ""; lblHint2.Text = ""; lblHint3.Text = ""; lblHint4.Text = ""; lblHint5.Text = ""; lblHint6.Text = ""; lblHint7.Text = ""; lblHint8.Text = ""; lblHint9.Text = ""; lblHint10.Text = "";
            txtAnswer1.Text = ""; txtAnswer2.Text = ""; txtAnswer3.Text = ""; txtAnswer4.Text = ""; txtAnswer5.Text = ""; txtAnswer6.Text = ""; txtAnswer7.Text = ""; txtAnswer8.Text = ""; txtAnswer9.Text = ""; txtAnswer10.Text = "";
            int langID = combGameType.SelectedIndex;//GET THE SELECTED INDEX

            langID = 9;
            if (langID != 0)
            {
                langID += 1;
            }

            string term, otherLangTerm;


            int x = 0;

            var AllTerm = await App.conn.QueryAsync<tblTerminology>("SELECT * FROM tblTerminology where langID = '" + langID + "'");
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
            int rNo;

            //populate the label and answer in the array
            x--;
            string n = arrEng[5];
            if (radEng_other_Lang.IsChecked == false)
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
                    rNo = myRandom.Next(0, x);
                    lblWord8.Text = arrEng[rNo]; arrAnswer[7] = arrOther[rNo];
                    rNo = myRandom.Next(0, x);
                   lblword9.Text = arrEng[rNo]; arrAnswer[8] = arrOther[rNo];
                    rNo = myRandom.Next(0, x);
                    lblWord10.Text = arrEng[rNo]; arrAnswer[9] = arrOther[rNo];

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
                    rNo = myRandom.Next(0, x);
                    lblWord8.Text = arrOther[rNo]; arrAnswer[7] = arrEng[rNo];
                    rNo = myRandom.Next(0, x);
                    lblword9.Text = arrOther[rNo]; arrAnswer[8] = arrEng[rNo];
                    rNo = myRandom.Next(0, x);
                    lblWord10.Text = arrOther[rNo]; arrAnswer[9] = arrEng[rNo];
                   
                }
            }


        }



        private void lnkHint1_Click(object sender, RoutedEventArgs e)
        {
            lnkHint1.IsEnabled = false;
            pointsPlayFor = pointsPlayFor - 5;
            lblPointsPlayFor.Text = pointsPlayFor.ToString();
            string answer;
            int l;
            answer = arrAnswer[0];
            l = answer.Length - 1;
            lblHint1.Text = answer.Substring(0, 1) + "..." + answer.Substring(l, 1);
        }

        private void lnkHint2_Click(object sender, RoutedEventArgs e)
        {
            lnkHint2.IsEnabled = false;
            pointsPlayFor = pointsPlayFor - 5;
            lblPointsPlayFor.Text = pointsPlayFor.ToString();
            string answer;
            int l;
            answer = arrAnswer[1];
            l = answer.Length - 1;
            lblHint2.Text = answer.Substring(0, 1) + "..." + answer.Substring(l, 1);
        }

        private void lnkHint3_Click(object sender, RoutedEventArgs e)
        {
            lnkHint3.IsEnabled = false;
            pointsPlayFor = pointsPlayFor - 5;
            lblPointsPlayFor.Text = pointsPlayFor.ToString();
            string answer;
            int l;
            answer = arrAnswer[2];
            l = answer.Length - 1;
            lblHint3.Text = answer.Substring(0, 1) + "..." + answer.Substring(l, 1);
        }

        private void lnkHint4_Click(object sender, RoutedEventArgs e)
        {
            lnkHint4.IsEnabled = false;
            pointsPlayFor = pointsPlayFor - 5;
            lblPointsPlayFor.Text = pointsPlayFor.ToString();
        
            string answer;
            int l;
            answer = arrAnswer[3];
            l = answer.Length - 1;
            lblHint4.Text = answer.Substring(0, 1) + "..." + answer.Substring(l, 1);
        }
        private void lnkHint5_Click(object sender, RoutedEventArgs e)
        {
            lnkHint5.IsEnabled = false;
            pointsPlayFor = pointsPlayFor - 5;
            lblPointsPlayFor.Text = pointsPlayFor.ToString(); 
            string answer;
            int l;
            answer = arrAnswer[4];
            l = answer.Length - 1;
            lblHint5.Text = answer.Substring(0, 1) + "..." + answer.Substring(l, 1);
        }
        private void lnkHint6_Click(object sender, RoutedEventArgs e)
        {
            lnkHint6.IsEnabled = false;
            pointsPlayFor = pointsPlayFor - 5;
            lblPointsPlayFor.Text = pointsPlayFor.ToString();
            string answer;
            int l;
            answer = arrAnswer[5];
            l = answer.Length - 1;
            lblHint6.Text = answer.Substring(0, 1) + "..." + answer.Substring(l, 1);
        }
        private void lnkHint7_Click(object sender, RoutedEventArgs e)
        {
            lnkHint7.IsEnabled = false;
            pointsPlayFor = pointsPlayFor - 5;
            lblPointsPlayFor.Text = pointsPlayFor.ToString();
            string answer;
            int l;
            answer = arrAnswer[6];
            l = answer.Length - 1;
            lblHint7.Text = answer.Substring(0, 1) + "..." + answer.Substring(l, 1);
        }
        private void lnkHint8_Click(object sender, RoutedEventArgs e)
        {
            lnkHint8.IsEnabled = false;
            pointsPlayFor = pointsPlayFor - 5;
            lblPointsPlayFor.Text = pointsPlayFor.ToString();
            string answer;
            int l;
            answer = arrAnswer[7];
            l = answer.Length - 1;
            lblHint8.Text = answer.Substring(0, 1) + "..." + answer.Substring(l, 1);    
        }
        private void lnkHint9_Click(object sender, RoutedEventArgs e)
        {

            lnkHint9.IsEnabled = false;
            pointsPlayFor = pointsPlayFor - 5;
            lblPointsPlayFor.Text = pointsPlayFor.ToString();
            string answer;
            int l;
            answer = arrAnswer[8];
            l = answer.Length - 1;
            lblHint9.Text = answer.Substring(0, 1) + "..." + answer.Substring(l, 1);
        }

        private void lnkHint10_Click(object sender, RoutedEventArgs e)
        {

            lnkHint10.IsEnabled = false;
            pointsPlayFor = pointsPlayFor - 5;
            lblPointsPlayFor.Text = pointsPlayFor.ToString();
            string answer;
            int l;
            answer = arrAnswer[9];
            l = answer.Length - 1;
            lblHint10.Text = answer.Substring(0, 1) + "..." + answer.Substring(l, 1);
        }

        private void lblWord10_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void lblPointsPlayFor_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void lblPointsScored_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void lblMinutes_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            msg();
        }

       

     


      

        
       
        

        

       
        

        
       
    }
}
