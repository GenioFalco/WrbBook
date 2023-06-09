using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
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
using WebBook.ClassesApp;
using WebBook.ClassesApp.Models;
using WebBook.EntityFramework;
using WebBook.UserControlUI;
using WebBook.WindowForm;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using static System.Net.Mime.MediaTypeNames;

namespace WebBook.PageWindow
{
    /// <summary>
    /// Логика взаимодействия для TestPage.xaml
    /// </summary>
    public partial class TestPage : Page
    {
       

        public static Results results = null;

        public static User user = null;

        public static Test test = null;

        public int CountTrue = 0;

        int currentNumber = 1;

        public TestPage()
        {
            InitializeComponent();
            test = ConrolerBroadCast.test;
            InitilizateVivod();
        }

        public void InitilizateVivod()
        {
            ConrolerBroadCast.questionModel = JsonConvert.DeserializeObject<List<QuestionModel>>(test.JsonFileQuestion);
            ConrolerBroadCast.answerModels = JsonConvert.DeserializeObject<List<AnswerModel>>(test.JsonFileAnswer);

            ListQuest.Children.Clear();
            foreach (var item in ConrolerBroadCast.questionModel)
            {
                QuestionList questionList = new QuestionList();
                questionList.TitleQuestion.Text = item.Title;
                questionList.questionModel = item;
                questionList.NumberStack.Text = currentNumber.ToString();
                // Увеличение номера элемента
                currentNumber++;
                questionList.VivodVariantov();
                ListQuest.Children.Add(questionList);

                questionList.BtAddVariant.Visibility = Visibility.Collapsed;
                questionList.DpContQuest.Visibility = Visibility.Collapsed;
                questionList.TitleQuestion.IsEnabled = false;
            }

            TitleTest.Text = test.TitleTest;
        }

       
        public void OverTest() 
        {

            List<AnswerModel> TrueAnswer = ConrolerBroadCast.answerModels.Where(p => p.IsTrue == true).ToList();
            foreach (var item in ClassVariablesTest.answerModelsTrue)
            {
                if (TrueAnswer.FirstOrDefault(p => p == item) != default(AnswerModel))
                {
                    CountTrue++;
                }
            }
        }
        
        public void Graed() 
        {
            double resch = 0;

            resch = Convert.ToDouble(CountTrue) / Convert.ToDouble(ClassVariablesTest.answerModelsTrue.Count()) * 100;

            
            if(resch >= 80) 
            {
                graed.Text = "5";
            }
            else 
            {
                if (resch >= 60) 
                {
                    graed.Text = "4";
                }
                else 
                {
                    if(resch >= 50) 
                    {
                        graed.Text = "3";
                    }
                    else if(resch < 50)
                    {
                        graed.Text = "2";
                    }
                }
            }
        }

        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите завершить тест? вернуться нельзя","Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                OverTest();
            }
            else
            {
                return; 
            }


            falseA.Text = (ClassVariablesTest.answerModelsTrue.Count() - CountTrue).ToString();
            trueA.Text = CountTrue.ToString();
            Graed();

            TcTest.SelectedItem = TcTest.Items[1];


            results = new Results();

            results.IdUser = user.IDUser;

            results.IdTest = test.IdTest;

            results.TrueAnswerTest = CountTrue;

            results.FalseAnswerTest = (ClassVariablesTest.answerModelsTrue.Count() - CountTrue);
            if (graed.Text == "" ) 
            {
                graed.Text = 2.ToString();
                results.GradeTest = Convert.ToInt32(graed.Text);
            }
            else 
            {
                results.GradeTest = Convert.ToInt32(graed.Text);
            }

            DataBase.webBookEntities.Results.AddOrUpdate(results);
            DataBase.webBookEntities.SaveChanges();
            TabIResult.Visibility = Visibility.Visible;
            TabITest.Visibility = Visibility.Collapsed;
            over.Visibility = Visibility.Collapsed;

            ClassVariablesTest.answerModelsTrue = new List<AnswerModel>();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainMenu.frame.Navigate(new HomePage());
        }
    }
}
