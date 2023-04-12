using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
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
using WebBook.EntityFramework;
using WebBook.UserControlUI;

namespace WebBook.PageWindow
{
    /// <summary>
    /// Логика взаимодействия для ResultStudent.xaml
    /// </summary>
    public partial class ResultStudent : Page
    {
        public static User user = null;
        public EntityFramework.Task task = null;
        public Results results = null;
        public AnswerPractical answerPractical = null;
        string like = "";
        int choise = 0;

        public ResultStudent()
        {
            InitializeComponent();
            VivodResult();
        }

        public void VivodResult()
        {
            UserName.Text = user.NameUser;
            if (PhotoConvert.loagPhoto(user.ImageUser) == null)
            {
                UserPhoto.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/TopicImg.png"));
            }
            else
            {
                UserPhoto.ImageSource = PhotoConvert.loagPhoto(user.ImageUser);
            }

            if (choise == 0)
            {
                TitleList.Text = "Пройденные тесты студентов";
                ListView.Children.Clear();
                List<Results> results = DataBase.webBookEntities.Results.Where(obj => DbFunctions.Like(obj.IdUser.ToString(), "%" + like + "%")).ToList();
                foreach (var item in results)
                {
                    ResultTestList resultTestList = new ResultTestList(item)
                    {
                        resultStudent = this
                    };

                    var UserId = DataBase.webBookEntities.User.Where(id => id.IDUser == item.IdUser).Select(x => x.NameUser + " " + x.SurnameUser).FirstOrDefault();
                    resultTestList.NameUserList.Text = UserId;


                    var TestId = DataBase.webBookEntities.Test.Where(id => id.IdTest == item.IdTest).Select(x => x.TitleTest).FirstOrDefault();
                    resultTestList.TestUserList.Text = TestId;

                    resultTestList.GraedlUserList.Text = "Оценка за тест:" + " " + item.GradeTest.ToString();

                    ListView.Children.Add(resultTestList);
                }
            }
            else
            {

                if (choise == 1)
                {
                    TitleList.Text = "Практические студентов";
                    ListView.Children.Clear();
                    List<AnswerPractical> answerPracticals = DataBase.webBookEntities.AnswerPractical.Where(obj => DbFunctions.Like(obj.TaskAnswer.ToString(), "%" + like + "%")).ToList();
                    foreach (var item in answerPracticals)
                    {
                        ResultPractical resultPractical = new ResultPractical(item)
                        {
                            resultStudent = this
                        };
                        var UserId = DataBase.webBookEntities.User.Where(id => id.IDUser == item.IdUser).Select(x => x.NameUser + " " + x.SurnameUser).FirstOrDefault();
                        resultPractical.NameUserList.Text = UserId;

                        var TaskId = DataBase.webBookEntities.Task.Where(id => id.IDTask == item.TaskAnswer).Select(x => x.TitleTask).FirstOrDefault();
                        resultPractical.PracticallUserList.Text = TaskId;


                        resultPractical.GraedPrac.Text = item.GradeAnswer.ToString();

                        ListView.Children.Add(resultPractical);
                    }
                }

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            choise = 0;
            VivodResult();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            choise = 1;
            VivodResult();
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            like = Search.Text;
            VivodResult();
        }
    }
}
