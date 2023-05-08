using System;
using System.Collections.Generic;
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
using WebBook.PageWindow;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using static System.Net.Mime.MediaTypeNames;

namespace WebBook.UserControlUI
{
    /// <summary>
    /// Логика взаимодействия для QuestionList.xaml
    /// </summary>
    public partial class QuestionList : UserControl
    {
        public TestPage TestPage;

        public AddEditTestPage AddEditTestPage = null;

        public static User user = null;


        public QuestionModel questionModel = new QuestionModel();

        public QuestionList()
        {
            InitializeComponent();
           
            VivodVariantov();

        }

       
        public void VivodVariantov()
        {
            ListVariant.Children.Clear();
            foreach (var item in ConrolerBroadCast.answerModels.Where(p=> p.IdQuestion == questionModel.Id))
            {
                VariantList variantList = new VariantList();
                variantList.AnswerV.Text = item.Title;
                variantList.answerModel = item;
                ListVariant.Children.Add(variantList);
            }
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AnswerModel answerModel = new AnswerModel();
            if (ConrolerBroadCast.answerModels.Count == 0)
            {
                answerModel.Id = 1;
            }
            else
            {
                answerModel.Id = ConrolerBroadCast.answerModels.Max(p => p.Id) + 1;
            }
            answerModel.IdQuestion = questionModel.Id;
            VariantList variantList = new VariantList();
            variantList.answerModel= answerModel;
            ConrolerBroadCast.answerModels.Add(answerModel);
            ListVariant.Children.Add(variantList);
        }

        private void Border_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            ((StackPanel)this.Parent).Children.Remove(this);
            ConrolerBroadCast.questionModel.Remove(questionModel);
            List<AnswerModel> answerModels = ConrolerBroadCast.answerModels.Where(p => p.IdQuestion == questionModel.Id).ToList(); 
           
            foreach (var item in answerModels)
            {
                ConrolerBroadCast.answerModels.Remove(item); 
            }
        }

        private void TitleQuestion_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (questionModel.Id == 0) return;
            ConrolerBroadCast.questionModel.First(p=> p.Id == questionModel.Id).Title = TitleQuestion.Text;
        }
    }
}
