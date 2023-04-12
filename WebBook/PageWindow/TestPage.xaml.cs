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

namespace WebBook.PageWindow
{
    /// <summary>
    /// Логика взаимодействия для TestPage.xaml
    /// </summary>
    public partial class TestPage : Page
    {
        public static TestModel test = null;

        public static Results results = null;

        public static User user = null;

        public static Test tests = null;

        public int CountTrue = 0;

        public TestPage()
        {
            InitializeComponent();
            InitilizateVivod();

           
        }

        public void InitilizateVivod()
        {
            TitleTest.Text = test.Test.TitleTest;
            ClassVariablesTest.questionModels = test.QuestionModel;
            ListQuest.Children.Clear();
            foreach (var item in test.QuestionModel)
            {
                QuestionList questionList = new QuestionList(item);
                questionList.TestPage = this;
                questionList.TitleQuestion.Text = item.Title;
                ListQuest.Children.Add(questionList);

                if (user.RoleUser == 2)
                {
                    questionList.BtAddVariant.Visibility = Visibility.Collapsed;
                    questionList.DpContQuest.Visibility = Visibility.Collapsed;

                    questionList.TitleQuestion.IsEnabled = false;
                }
            }
        }

       
        public void OverTest() 
        {
            foreach (var item in ClassVariablesTest.questionModels)
            {
                bool qqq = false;
                List<AsnswerModel> TrueAnswer = item.asnswerModels.Where(p => p.IsTrue == true).ToList();
                int i = TrueAnswer.Count();
                if (i != item.Vibran.Count()) continue;
                foreach (var item1 in item.Vibran)
                {
                    if (TrueAnswer.Where(p => p == item1).FirstOrDefault() == default(AsnswerModel)) 
                    {
                        qqq = true; break; 
                    }
                       
                }
                if (qqq) continue;
                CountTrue++;
                
            }
        }
        
        public void Graed() 
        {
            double resch = 0;

            resch = Convert.ToDouble(CountTrue) / Convert.ToDouble(ClassVariablesTest.questionModels.Count()) * 100;

            
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
            if (MessageBox.Show("Вы точно хотите завершить тест? вернуться нельзя",
                    "",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)

                OverTest();
            falseA.Text = (ClassVariablesTest.questionModels.Count() - CountTrue).ToString();
            trueA.Text = CountTrue.ToString();
            Graed();
            TcTest.SelectedItem = TcTest.Items[1];


            results = new Results();

            results.IdUser = user.IDUser;

            results.IdTest = test.Test.IdTest;

            results.TrueAnswerTest = CountTrue;

            results.FalseAnswerTest = (ClassVariablesTest.questionModels.Count() - CountTrue);

            results.GradeTest = Convert.ToInt32(graed.Text);

            DataBase.webBookEntities.Results.AddOrUpdate(results);
            DataBase.webBookEntities.SaveChanges();
            TabIResult.Visibility = Visibility.Visible;
            TabITest.Visibility = Visibility.Collapsed;
            over.Visibility = Visibility.Collapsed;

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainMenu.frame.Navigate(new HomePage());
        }
    }
}
