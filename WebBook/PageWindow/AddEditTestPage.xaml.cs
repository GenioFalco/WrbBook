using EllipticCurve.Utils;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WebBook.ClassesApp;
using WebBook.ClassesApp.Models;
using WebBook.EntityFramework;
using WebBook.UserControlUI;

namespace WebBook.PageWindow
{
    /// <summary>
    /// Логика взаимодействия для AddEditTestPage.xaml
    /// </summary>
    public partial class AddEditTestPage : Page
    {
        public static Test test = null;

        readonly List<string> topicTitle;

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

        public void InitilizateVivod()
        {
           
            if (test == null) return;

            ConrolerBroadCast.questionModel = JsonConvert.DeserializeObject<List<QuestionModel>>(test.JsonFileQuestion);
            ConrolerBroadCast.answerModels = JsonConvert.DeserializeObject<List<AnswerModel>>(test.JsonFileAnswer);

            ListQuest.Children.Clear();
            foreach (var item in ConrolerBroadCast.questionModel)
            {
                QuestionList questionList = new QuestionList();
                questionList.TitleQuestion.Text = item.Title;
                questionList.questionModel = item;
                questionList.VivodVariantov();
                ListQuest.Children.Add(questionList);
            }

            TitleTest.Text = test.TitleTest;
            TopicTest.SelectedValue = DataBase.webBookEntities.Topic.Where(x => x.IDTopic == test.TopicTest).Select(id => id.TitleTopic).FirstOrDefault();

        }

        public void AddEdTest()
        {
            if(test == null) 
            {
                test= new Test();
            }
            test.TitleTest = TitleTest.Text;
            test.TopicTest = DataBase.webBookEntities.Topic.Where(x => x.TitleTopic == TopicTest.SelectedValue.ToString()).Select(id => id.IDTopic).FirstOrDefault();

            test.JsonFileQuestion = JsonConvert.SerializeObject(ConrolerBroadCast.questionModel);
            test.JsonFileAnswer = JsonConvert.SerializeObject(ConrolerBroadCast.answerModels);
            
            DataBase.webBookEntities.Test.AddOrUpdate(test);
            DataBase.webBookEntities.SaveChanges();

            MessageBox.Show("Тест сохранён");

        }

        private void BtAddQuest_Click(object sender, RoutedEventArgs e)
        {
            QuestionModel questionModel =new QuestionModel();   
            if(ConrolerBroadCast.questionModel.Count == 0)
            {
                questionModel.Id = 1;
            }
            else 
            {
                questionModel.Id = ConrolerBroadCast.questionModel.Max(p => p.Id) + 1;
            }
            QuestionList questionList = new QuestionList();
            questionList.questionModel = questionModel;
            ConrolerBroadCast.questionModel.Add(questionModel);
            ListQuest.Children.Add(questionList);
        }

        private void BtSaveTest_Click(object sender, RoutedEventArgs e)
        {
            AddEdTest();
            ConrolerBroadCast.questionModel = new List<QuestionModel>();
            ConrolerBroadCast.answerModels = new List<AnswerModel>();
        }
    }
}
