
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
    public sealed partial class SimpleGame1Page : Page
    {
         string[]  arrEng = new string[100];
         string[] arrOther = new string[100] ;
        private string[] arrAnswer = new string[]{};
        int[,] arrNum = new int[,] { {} };
        int a1 = 0; int a2 = 0; int a3; int a4 = 0; int a5 = 0; int a6 = 0; int a7 = 0;
        string A1 = ""; string A2 = ""; string A3 = ""; string A4 = ""; string A5 = ""; string A6 = "";
        public SimpleGame1Page()
        {
            this.InitializeComponent();
            radEng_other_Lang.IsChecked = true;
            setLabal();
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            string msg = "";
            int ans1 = Convert.ToInt32(txtWord1.Text);
            int ans2 = Convert.ToInt32(txtWord2.Text);
            int ans3 = Convert.ToInt32(txtWord1.Text);
            int ans4 = Convert.ToInt32(txtWord2.Text);
            int ans5 = Convert.ToInt32(txtWord1.Text);
            int ans6 = Convert.ToInt32(txtWord2.Text);
            int ans7 = Convert.ToInt32(txtWord1.Text);
         

            //if(arrAnswer[ans1 - 1] == lblWord1.Text.Trim())
            //{
            //    lstDisplayEngWord.Items.Add("i love yoyu wisani");
            //}
            lstDisplayEngWord.Items.Add(a1.ToString() + "  " + a2.ToString() + "  " + a2.ToString() + "  " + a3.ToString() + "  " + a4.ToString());
            if (ans1 != a1)
            {
                txtWord1.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else { txtWord1.BorderBrush = new SolidColorBrush(Colors.Green); }

            if(ans2.ToString() != a2.ToString())
            {
                txtWord2.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else { txtWord2.BorderBrush = new SolidColorBrush(Colors.Green); }

            if (ans3.ToString() != a3.ToString())
            {
                txtWord3.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else { txtWord3.BorderBrush = new SolidColorBrush(Colors.Green); }

            if (ans4.ToString() != a4.ToString())
            {
                txtWord4.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else { txtWord4.BorderBrush = new SolidColorBrush(Colors.Green); }

            if (ans5.ToString() != a5.ToString())
            {
                txtWord5.BorderBrush = new SolidColorBrush(Colors.Red);
            }

            if (ans6.ToString() != a6.ToString())
            {
                txtWord6.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else { txtWord6.BorderBrush = new SolidColorBrush(Colors.Green); }

            if (ans7.ToString() != a7.ToString())
            {
                txtWord7.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else { txtWord7.BorderBrush = new SolidColorBrush(Colors.Green); }
        
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(GamePage));
        }

        private void btbReloadWord_Click(object sender, RoutedEventArgs e)
        {
           //for string name ;
            var l = lstDisplayEngWord.SelectedIndex;
            lblWord1_Copy1.Text = l.ToString();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {

        }

        private void combLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {



        }


        private void radEng_other_Lang_Checked(object sender, RoutedEventArgs e)
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
            int index = combGameType.SelectedIndex;
          //  setWord(index);
           
        
        }

        private void radOtherLang_Eng_Checked(object sender, RoutedEventArgs e)
        {
            if (Convert.ToBoolean(radOtherLang_Eng.IsChecked))
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
                int index = combGameType.SelectedIndex;
               // setWord(index);
            }
        }
        private async void messageBox(string msg)
        {
            var msgDisplay = new Windows.UI.Popups.MessageDialog(msg);
            await msgDisplay.ShowAsync();
        }
        private async void setLabal()
        {

            string term, otherLangTerm;
            string[,] ot = new string[,] { { } };
            int x = 0;
            string[] en = new string[7];
            var AllTerm = await App.conn.QueryAsync<tblTerminology>("SELECT * FROM tblTerminology where langID = '" + 10 + "'");
            // int i = 0;
            if (AllTerm != null)
            {
                foreach (var objT in AllTerm)
                {
                    term = objT.engTerm;
                    otherLangTerm = objT.otherLangTerm;
                    if (x < 10)
                    {
                        arrEng[x] = term;
                        arrOther[x] = otherLangTerm;
                        x++;
                    }
                }
            }

            //   string[] arrAnser = new string[7];
            for (int i = 0; i < 7; i++)
            {
                // lstDisplayEngWord.Items.Add(i.ToString() + "  " + arrEng[i] + "" + arrOther[i]);

            }

            Random myRandom = new Random();
            int[] ans = new int[7];
            int[] ans1 = new int[]{0,0,0,0,0,0,0,0,0,0,0,0,0,0} ;
            string[] arrAnswer = new string[4];
            int rNo = myRandom.Next(1, 4);
            bool t;
            lblWord1.Text = arrOther[0]; arrAnswer[0] = arrOther[0]; ans[0] = 0; ans1[0] = 1; A1 = arrEng[0];
            lblWord2.Text = arrOther[1]; arrAnswer[1] = arrOther[1]; ans[1] = 0; ans1[1] = 2; A2 = arrEng[1];
            lblWord3.Text = arrOther[2]; arrAnswer[2] = arrOther[2]; ans[2] = 0; ans1[2] = 3; A3 = arrEng[2];
            lblWord4.Text = arrOther[3]; arrAnswer[3] = arrOther[3]; ans[3] = 0; ans1[3] = 4; A4 = arrEng[3]; ans1[4] = 5; A4 = arrEng[3]; ans1[5] = 6; A4 = arrEng[3]; ans1[6] = 7; A4 = arrEng[3]; ans1[7] = 8; A4 = arrEng[3];

            //lstDisplayEngWord.Items.Add("2 " + arrEng[rNo]);
            //rNo = myRandom.Next(0, x);a3 = rNo;
            // lblWord3.Text = arrOther[2]; arrAnswer[2] = lblWord1.Text;
            // lstDisplayEngWord.Items.Add(1.ToString() + "  " + arrEng[rNo]);
            // ans[0] = 2;

            for (int l = 1; l < 7; l++)
            {
                t = false;
                rNo = myRandom.Next(1, 7); 
                for (int y = 0; y < l; y++)
                {
                    if (rNo == ans[y])
                    {
                        l--;
                        t = true;
                        break;

                    }
                }
                if (t == false)
                {
                 //   lstDisplayEngWord.Items.Add((l).ToString() + "  " + arrEng[rNo] + rNo.ToString());

                    ans[l] = rNo;
                    display(rNo, l);


                }

            }

            for (int y = 0; y < 4; y++)
            {
              //  lstDisplayEngWord.Items.Add((y).ToString() + "  " + arrEng[ans[y]] + rNo.ToString());

            }
            //ans[0] = 4;
            //a1 = ans[1];
            //a2 = ans[2];
            //a3 = ans[3];
            //a4 = ans[3];

            lblWord6.Text = a2.ToString();
            lblWord5.Text = a3.ToString();
            lstDisplayEngWord.Items.Add(4.ToString() + "  " + arrEng[0]);

        }

        private void display(int num, int c)
        {
            int y = num;
            if(num == 1)
            {
                lstDisplayEngWord.Items.Add((c).ToString() + "  " + arrEng[0] + num.ToString());
                a1 = c;
            }else if(num == 2)
            {
                lstDisplayEngWord.Items.Add((c).ToString() + "  " + arrEng[1] + num.ToString());
                a2 = c;
                lblAnswer2.Text = arrEng[c];
            }
            else if (num == 3)
            {
                lstDisplayEngWord.Items.Add((c).ToString() + "  " + arrEng[2] + num.ToString());
                a3 = c;
            }
            else if (num == 4)
            {
                lstDisplayEngWord.Items.Add((c).ToString() + "  " + arrEng[3] + num.ToString());
                a4 = c;
            }
            else if (num == 5)
            {
                lstDisplayEngWord.Items.Add((y).ToString() + "  " + arrEng[4] + num.ToString());
            }
            else if (num == 6)
            {
                lstDisplayEngWord.Items.Add((y).ToString() + "  " + arrEng[5] + num.ToString());
            }
            else if (num == 7)
            {
                lstDisplayEngWord.Items.Add((y).ToString() + "  " + arrEng[6] + num.ToString());
            }
            else
            {

            }
        }
        

      /*   lblWord1.Text = arrOther[rNo]; en[0] = arrEng[rNo]; //lstDisplayEngWord.Items.Add("1 " + arrEng[rNo]);
         ot[0, 0] = arrOther[rNo] = arrEng[rNo];
         rNo = myRandom.Next(0, x);
            lblWord2.Text = arrOther[rNo]; a2 = rNo;
        // lstDisplayEngWord.Items.Add("2 " + arrEng[rNo]);
            rNo = myRandom.Next(0, x);a3 = rNo;
         
            lblWord3.Text = arrOther[rNo]; 
        // lstDisplayEngWord.Items.Add("3 " + arrEng[rNo]);
           
         
       
         
         
         
         /*
          * 
          * 
          * rNo = myRandom.Next(0, x); a4 = rNo;
            lblWord4.Text = arrOther[rNo]; //lstDisplayEngWord.Items.Add("4 " +arrEng[rNo]);
            rNo = myRandom.Next(0, x); a5 = rNo;
            lblWord5.Text = arrOther[rNo]; //lstDisplayEngWord.Items.Add("5 " + arrEng[rNo]);
            rNo = myRandom.Next(0, x); a6 = rNo;
            lblWord6.Text = arrOther[rNo]; //lstDisplayEngWord.Items.Add("6 " + arrEng[rNo]);
            rNo = myRandom.Next(0, x); a7 = rNo;
            lblWord7.Text = arrOther[rNo]; //lstDisplayEngWord.Items.Add("7 " + arrEng[rNo]);
            ot[0, 0] = arrOther[rNo] = arrEng[rNo];

            bool ans;
            
            string[] enL = new string[7];
            int[] answers = new int[7];

            rNo = myRandom.Next(1, 8);
            answers[0] = rNo;
            lstDisplayEngWord.Items.Add(answers[0].ToString());
            answers[0] = rNo;
            for (int j = 1; j < 7 ;j++)
                 { 
                        rNo = myRandom.Next(1, 8);
                        ans = false;

                     for(int i = 0; i < j;i++)
                     {
                        if(answers[i] == rNo)
                        {
                            ans = true;
                        }
                     }
                     if(ans ==false)
                     {
                         answers[j] = rNo;
                         lstDisplayEngWord.Items.Add(answers[j].ToString() );
                     }
                     else { j--; }
                 }*/

      
        
    }
}
