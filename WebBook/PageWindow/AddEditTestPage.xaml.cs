using System;
using System.Collections.Generic;
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
using WebBook.ClassesApp.Models;
using WebBook.EntityFramework;
using WebBook.UserControlUI;
using WebBook.WindowForm;
using static System.Net.Mime.MediaTypeNames;

namespace WebBook.PageWindow
{
    /// <summary>
    /// Логика взаимодействия для AddEditTestPage.xaml
    /// </summary>
    public partial class AddEditTestPage : Page
    {
        public static List<QuestionList> questionLists = new List<QuestionList>();

        public static TestModel test = null;

        List<string> topicTitle;

        public static User user = null;

        public AddEditTestPage()
        {
            InitializeComponent();
            test = ConrolerBroadCast.test;
            InitilizateVivod();

            topicTitle = DataBase.webBookEntities.Topic.Select(x => x.TitleTopic).Distinct().ToList();

            foreach (var item in topicTitle)
            {
                TopicTest.Items.Add(item);
            }

        }

        public void Saves() 
        {
            MessageBox.Show("");
        }



        public void InitilizateVivod()
        {
            if (test != null)
            {
                TitleTest.Text = test.Test.TitleTest;

                var topicId = DataBase.webBookEntities.Topic.Where(x => x.IDTopic == test.Test.TopicTest).Select(id => id.TitleTopic).FirstOrDefault();
               
                TopicTest.SelectedValue = topicId;

                ListQuest.Children.Clear();
                foreach (var item in test.QuestionModel)
                {
                    QuestionList questionList = new QuestionList(item);
                    questionList.AddEditTestPage = this;
                    questionList.TitleQuestion.Text = item.Title;
                    ListQuest.Children.Add(questionList);
                }

            }
            else
            {
                test = new TestModel(new Test());
            }

        }

        public void AddEdTopic()
        {

            if (ConrolerBroadCast.CheckAdd == false)
            {
                test = new TestModel(new Test());

                foreach (var item in questionLists)
                {
                    item.QuestionModel.Title = item.TitleQuestion.Text;
                    item.Saves();
                    test.QuestionModel.Add(item.QuestionModel);
                }

                test.SaveJs();

                if (!Checks.LetteLatinAndCyrillic(TitleTest.Text, "Поле наименование теста")) return;
                test.Test.TitleTest = TitleTest.Text;


                var topicId = DataBase.webBookEntities.Topic.Where(id => id.TitleTopic == TopicTest.Text).Select(x => x.IDTopic).FirstOrDefault();
                test.Test.TopicTest = topicId;

                DataBase.webBookEntities.Test.Add(test.Test);
                DataBase.webBookEntities.SaveChanges();

                MessageBox.Show("Тест добавлен");
            }
            else
            {
                foreach (var item in questionLists)
                {
                    item.QuestionModel.Title = item.TitleQuestion.Text;
                    item.Saves();
                    test.QuestionModel.Add(item.QuestionModel);
                }

                test.SaveJs();

                if (!Checks.LetteLatinAndCyrillic(TitleTest.Text, "Поле наименование теста")) return;
                test.Test.TitleTest = TitleTest.Text;

                var topicId = DataBase.webBookEntities.Topic.Where(id => id.TitleTopic == TopicTest.Text).Select(x => x.IDTopic).FirstOrDefault();
                test.Test.TopicTest = topicId;

                DataBase.webBookEntities.Test.AddOrUpdate(test.Test);
                DataBase.webBookEntities.SaveChanges();

                MessageBox.Show("Тест обновлен");
            }

        }

        private void BtAddQuest_Click(object sender, RoutedEventArgs e)
        {
            QuestionList questionList = new QuestionList(new QuestionModel());
            questionLists.Add(questionList);
            ListQuest.Children.Add(questionList);
        }

        private void BtSaveTest_Click(object sender, RoutedEventArgs e)
        {
            AddEdTopic();
        }
    }
}
