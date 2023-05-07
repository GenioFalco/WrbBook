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



        List<VariantList> variantLists = new List<VariantList>();
        public QuestionModel QuestionModel { get; set; }


        public QuestionList(QuestionModel questionModel)
        {
            InitializeComponent();
            this.QuestionModel = questionModel;
            VivodVariantov();

        }

        public void Saves() 
        {
            foreach (var item in variantLists)
            {
                item.asnswer.Title = item.AnswerV.Text;
                QuestionModel.asnswerModels.Add(item.asnswer);
            }
        }
        public void VivodVariantov()
        {

            ListVariant.Children.Clear();

            foreach (var item in QuestionModel.asnswerModels)
            {
                VariantList variantList = new VariantList(item);
                variantList.QuestionList = this;
                variantList.questionModel = QuestionModel;
                variantList.AnswerV.Text = item.Title;
                ListVariant.Children.Add(variantList);
                
                if(ConrolerBroadCast.CheckTest == true)
                {
                    variantList.BtDelAnsw.Visibility = Visibility.Collapsed;
                    variantList.AnswerV.IsEnabled = false;
                }
                

            }

        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            VariantList variantList = new VariantList(new AsnswerModel());
            variantLists.Add(variantList);
            ListVariant.Children.Add(variantList);
        }

        private void Border_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            ((StackPanel)this.Parent).Children.Remove(this);
            ConrolerBroadCast.test.QuestionModel.Remove(QuestionModel);
        }
    }
}
