
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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
        public  Test test = new Test();

        readonly List<string> topicTitle;

        public static User user = null;
        
        int currentNumber = 1;


        public AddEditTestPage()
        {
            InitializeComponent();
            ConrolerBroadCast.questionModel = new List<QuestionModel>();
            ConrolerBroadCast.answerModels = new List<AnswerModel>();
            
            
            if (ConrolerBroadCast.test == null) 
            {
                test = new Test();
            }
            else 
            {
                test = ConrolerBroadCast.test;
            }
            InitilizateVivod();
            topicTitle = DataBase.webBookEntities.Topic.Select(x => x.TitleTopic).Distinct().ToList();
            foreach (var item in topicTitle)
            {
                TopicTest.Items.Add(item);
            }
        }

  

      

        public void InitilizateVivod()
        {
           
            if (test.IdTest == 0) return;

            ConrolerBroadCast.questionModel = JsonConvert.DeserializeObject<List<QuestionModel>>(test.JsonFileQuestion);
            ConrolerBroadCast.answerModels = JsonConvert.DeserializeObject<List<AnswerModel>>(test.JsonFileAnswer);

            ListQuest.Children.Clear();
            foreach (var item in ConrolerBroadCast.questionModel.Where(p=> p.IdtTest == test.IdTest).ToList())
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
            

            if (test == null) 
            {
                test= new Test();
            }

            if (!Checks.LetteLatinAndCyrillic(TitleTest.Text, "Поле наименование теста")) return;
            test.TitleTest = TitleTest.Text;

            if (TopicTest.SelectedValue == null)
            {
                MessageBox.Show("Выберите тему задания"); return;
            }
            test.TopicTest = DataBase.webBookEntities.Topic.Where(id => id.TitleTopic == TopicTest.Text).Select(x => x.IDTopic).FirstOrDefault();
            

            test.JsonFileQuestion = JsonConvert.SerializeObject(ConrolerBroadCast.questionModel);
            test.JsonFileAnswer = JsonConvert.SerializeObject(ConrolerBroadCast.answerModels);

            if (ConrolerBroadCast.questionModel.Count <= 4) 
            {
                MessageBox.Show("Добавьте не менее 5 вопросов"); return;
            }


            if (!Checks.WordAndNumber2(ConrolerBroadCast.questionModel, "Поле текст вопроса")) return;

            if (!Checks.WordAndNumber3(ConrolerBroadCast.answerModels, "Поля ответов")) return;


            foreach (var question in ConrolerBroadCast.questionModel)
            {
                var count = ConrolerBroadCast.answerModels.Count(a => a.IdQuestion == question.Id);
                if (count < 2)
                {
                    MessageBox.Show("Проверьте у каждого ли вопроса есть как минимум 2 ответа"); return;
                }
            }



            var groupedAnswers = ConrolerBroadCast.answerModels.GroupBy(a => a.IdQuestion);

            // Проверка наличия хотя бы одного значения IsTrue = true в каждой группе
            bool allGroupsHaveTrueAnswer = groupedAnswers.All(g => g.Any(a => a.IsTrue));

            if (!allGroupsHaveTrueAnswer)
            {
                MessageBox.Show("Проверьте у всех ли вопросов отмечен правильный ответ"); return;
            }
          
            


            DataBase.webBookEntities.Test.AddOrUpdate(test);
            DataBase.webBookEntities.SaveChanges();


           
            int id1 = DataBase.webBookEntities.Test.Max(p=> p.IdTest);
            foreach (var item in ConrolerBroadCast.questionModel)
            {
                item.IdtTest = id1;
            }

          

            test.JsonFileQuestion = JsonConvert.SerializeObject(ConrolerBroadCast.questionModel);
            DataBase.webBookEntities.Test.AddOrUpdate(test);
            DataBase.webBookEntities.SaveChanges();


            MessageBox.Show("Тест сохранён");

            ConrolerBroadCast.questionModel = new List<QuestionModel>();
            ConrolerBroadCast.answerModels = new List<AnswerModel>();
            ConrolerBroadCast.test = new Test();

            ListQuest.Children.Clear();
            TitleTest.Text = "";
            TopicTest.SelectedValue = null;
        }

        

        private void BtAddQuest_Click(object sender, RoutedEventArgs e)
        {
            QuestionModel questionModel = new QuestionModel();   
            if(ConrolerBroadCast.questionModel.Count == 0)
            {
                questionModel.Id = 1;
            }
            else 
            {
                questionModel.Id = ConrolerBroadCast.questionModel.Max(p => p.Id) + 1;
            }
            questionModel.IdtTest = test.IdTest;
            QuestionList questionList = new QuestionList();
            questionList.questionModel = questionModel;
            ConrolerBroadCast.questionModel.Add(questionModel);

            // Установка номера элемента в текстовом блоке внутри user control
            questionList.NumberStack.Text = currentNumber.ToString();
            // Увеличение номера элемента
            currentNumber++;

            ListQuest.Children.Add(questionList);
           
        }

        private void BtSaveTest_Click(object sender, RoutedEventArgs e)
        {
            AddEdTest();

        }
    }
}
