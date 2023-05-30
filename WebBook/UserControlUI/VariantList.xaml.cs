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
    /// Логика взаимодействия для VariantList.xaml
    /// </summary>
    public partial class VariantList : UserControl
    {
        public QuestionList QuestionList;

        public AnswerModel answerModel = new AnswerModel();

       
        public VariantList()
        {
            InitializeComponent();
            

        }

        private void CbCheck_Checked_1(object sender, RoutedEventArgs e)
        {
               
            if (ConrolerBroadCast.CheckTest == false)
            {
                answerModel.IsTrue = true;
            }
            else
            {
                ClassVariablesTest.answerModelsTrue.Add(answerModel);
            }
        }

        private void CbCheck_Unchecked_1(object sender, RoutedEventArgs e)
        {
            if (ConrolerBroadCast.CheckTest == false)
            {
                answerModel.IsTrue = false;
            }
            else
            {
                ClassVariablesTest.answerModelsTrue.Remove(answerModel);
            }

        }

        private void BtDelAnsw_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ((StackPanel)this.Parent).Children.Remove(this);
            ConrolerBroadCast.answerModels.Remove(answerModel);
        }

        private void AnswerV_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (answerModel.Id == 0) return;
            if (!Checks.WordAndNumber2(AnswerV.Text, "Поле наименование ответа")) return;
            ConrolerBroadCast.answerModels.First(p => p.Id == answerModel.Id).Title = AnswerV.Text;
        }
    }
}
