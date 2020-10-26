//DZ3 - Буй Тхе Зунг - УТС22
//Вариант 2
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.CodeDom;

namespace DZ3Fixed
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// Application Math Quiz 
    /// Show : Let's start - when application was started
    //**************************************/
    // *  Number_1_1 * Number_1_3 = ?      */

    //   4 answers to choose:
    //        1:
    //        2:
    //        3:
    //        4:
    //**************************************/
    ///User Choose the Right Answer
    ///Click Button  |Check|   to check the answer. 1 Right Answer = 1 Score. 1 Wrong Answer = 0 Score
    ///Show : Well done! - when there is Right answer
    ///Show : Oh! Let's try again - when there is Wrong answer
    ///Click Button  |Next|    to ignore and go to next question. 1 Ignore = 0 Score
    public partial class MainWindow : Window
    {
        System.Windows.Forms.Timer tmr = new System.Windows.Forms.Timer();
        public MainWindow()
        {
            InitializeComponent();
        }

        //Create Number_1_1 with variable random betwen (1-9). Number_1_1 will be created when Page Loaded 
        private void Number1_1_change(object sender, System.EventArgs e)
        {
            Random r1 = new Random();
            int num1 = r1.Next(1, 10);
            Number1_1.Text = num1.ToString();
        }

        //Create Number_1_3 with variable random betwen (1-9). Number_1_3 will be created when Page Loaded 
        private void Number1_3_change(object sender, System.EventArgs e)
        {
            Random r2 = new Random();
            int num1 = Int32.Parse(Number1_1.Text);
            int num3 = 0;
            // for() - make sure that Value of Number_1_3 will have close 30% the same Value of Number_1_1
            for (int i = num1; i <= 9; i++)
            {
                num3 = r2.Next(1, 10);
                if (i % 3 != 0)
                {
                    if (num3 != num1)
                    {
                        break;
                    }
                }
                else if (i % 3 == 0)
                {
                    if (num3 == num1)
                    {
                        break;
                    }
                }
            }
            Number1_3.Text = num3.ToString();
        }

        private int[] CheckContent(int[] Content, int ResultPos)
        {
            Random r = new Random();
            int[] MediumContent = Content;
            int Max = Content[0];
            for (int i = 0; i < 4; i++)
            {
                if (Max < Content[i])
                {
                    Max = Content[i];
                }
            }

            for (int i = 0; i < 4; i++)
            {
                if (i == (ResultPos - 1))
                {
                    continue;
                }
                else
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (i == j)
                        {
                            continue;
                        }
                        else 
                        {
                            if(Content[i] == MediumContent[j])
                            {
                                if (Content[i] == 99)
                                {
                                    Content[i] = r.Next(82, 98);
                                }
                                else
                                {
                                    Content[i] = r.Next(Max, 100);
                                }
                            }
                        }
                    }
                }
            }
            return Content;
        }


        //SetContentBtn() - Create random Value for Radiso button 
        //                - Make sure 1 in 4 Radio Button will have the Right Answer
        public void SetContentBtn()
        {
            int a = Int32.Parse(Number1_1.Text);
            int b = Int32.Parse(Number1_3.Text);
            int result = a * b;
            Random r = new Random();
            int RanPosition = r.Next(1, 5);
            int[] SetContent = new int[4];
            int[] ContentAfterChecking = new int[4];
            for (int i = 1; i <= 4; i++)
            {
                if (i == RanPosition)
                {
                    SetContent[i - 1] = result;
                }
                else
                {
                    SetContent[i - 1] = r.Next(1, 100);
                }
            }
            ContentAfterChecking = CheckContent(SetContent, RanPosition);
            Console.WriteLine("Radio BTN 1:{0} 2:{1} 3:{2} 4:{3}", SetContent[0], SetContent[1], SetContent[2], SetContent[3]);
            RadioBtn1.Content = SetContent[0].ToString();
            RadioBtn2.Content = SetContent[1].ToString();
            RadioBtn3.Content = SetContent[2].ToString();
            RadioBtn4.Content = SetContent[3].ToString();
        }
        /*
        // Score() - Show Score. Each right answer, user will collect 1 score
        private void Score()
        {
            Console.WriteLine("Score Loaded");
            int Score = Int32.Parse(ScoreValue.Text);
            int NewScore = Score + 1;
            Console.WriteLine("NewScore = {0}", NewScore);
            ScoreValue.Text = NewScore.ToString();
        }
        */
        private void Page_Load(object sender, System.EventArgs e)
        {
            Console.WriteLine("Page Loaded");
            Number1_1_change(sender, e);
            Number1_3_change(sender, e);
            SetContentBtn();

        }

        //SetCollaped() - Hide "Well done!"  "Oh! Let's try again" When there is new question
        private void SetCollaped(object sender, System.EventArgs e)
        {
            RightAnswer.Visibility = System.Windows.Visibility.Collapsed;
            WrongAnswer.Visibility = System.Windows.Visibility.Collapsed;
            tmr.Stop();
        }

        private void RightAnswerChecking()
        {
            int RightAnswerIndex = Int32.Parse(RightAnswersText.Text);
            int NewRightAnswer = RightAnswerIndex + 1;
            Console.WriteLine("New Correct Answer = {0}", NewRightAnswer);
            RightAnswersText.Text = NewRightAnswer.ToString();
        }

        private void WrongAnswerChecking()
        {
            int WrongAnswerIndex = Int32.Parse(WrongAnswersText.Text);
            int NewWrongAnswer = WrongAnswerIndex + 1;
            Console.WriteLine("New Wrong Answer = {0}", NewWrongAnswer);
            WrongAnswersText.Text = NewWrongAnswer.ToString();
        }

        //PressCheckBtn() - Check the answer
        //                - After that load the new answer
        private void PressCheckBtn(object sender, System.EventArgs e)
        {
            int a = Int32.Parse(Number1_1.Text);
            int b = Int32.Parse(Number1_3.Text);
            int result = a * b;
            int[] ContentBtn = new int[4];
            int TruePosition = 1;
            bool CheckResult = true;
            ContentBtn[0] = Int32.Parse((string)RadioBtn1.Content);
            ContentBtn[1] = Int32.Parse((string)RadioBtn2.Content);
            ContentBtn[2] = Int32.Parse((string)RadioBtn3.Content);
            ContentBtn[3] = Int32.Parse((string)RadioBtn4.Content);

            for (int i = 1; i <= 4; i++)
            {
                if (ContentBtn[i - 1] == result)
                {
                    TruePosition = i;
                    break;
                }
            }

            if (TruePosition == 1)
            {
                if (RadioBtn1.IsChecked == true)
                {
                    CheckResult = true;
                }
                else
                {
                    CheckResult = false;
                }
            }
            else if (TruePosition == 2)
            {
                if (RadioBtn2.IsChecked == true)
                {
                    CheckResult = true;
                }
                else
                {
                    CheckResult = false;
                }
            }
            else if (TruePosition == 3)
            {
                if (RadioBtn3.IsChecked == true)
                {
                    CheckResult = true;
                }
                else
                {
                    CheckResult = false;
                }
            }
            else if (TruePosition == 4)
            {
                if (RadioBtn4.IsChecked == true)
                {
                    CheckResult = true;
                }
                else
                {
                    CheckResult = false;
                }
            }

            Console.WriteLine("Check Result = {0}", CheckResult);

            //Show "Well done!" or "Oh! Let's try again" in 1400 miliseconds. After that, hide them for new question
            if (CheckResult == true)
            {
                //Score();
                Start.Visibility = System.Windows.Visibility.Collapsed;
                RightAnswer.Visibility = System.Windows.Visibility.Visible;
                tmr.Interval = 1400;
                tmr.Tick += new EventHandler(SetCollaped);
                tmr.Start();
                Console.WriteLine("Right Answer");
                RightAnswerChecking();
            }
            else if (CheckResult == false)
            {
                Start.Visibility = System.Windows.Visibility.Collapsed;
                WrongAnswer.Visibility = System.Windows.Visibility.Visible;
                tmr.Interval = 1400;
                tmr.Tick += new EventHandler(SetCollaped);
                tmr.Start();
                Console.WriteLine("Wrong Answer");
                WrongAnswerChecking();
            }
            Page_Load(sender, e);
        }

    }
}
