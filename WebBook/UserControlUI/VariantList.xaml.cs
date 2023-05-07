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

        public AsnswerModel asnswer;

        public QuestionModel questionModel;
        public VariantList(AsnswerModel asnswerModel)
        {
            InitializeComponent();
            asnswer = asnswerModel;

        }

        private void CbCheck_Checked_1(object sender, RoutedEventArgs e)
        {
            if(ConrolerBroadCast.CheckTest == false) 
            {
                asnswer.IsTrue = true;  
            }
            else 
            {
                ClassVariablesTest.questionModels.Where(p => p == questionModel).First().Vibran.Add(asnswer);
            }
        }

        private void CbCheck_Unchecked_1(object sender, RoutedEventArgs e)
        {
            if (ConrolerBroadCast.CheckTest == false)
            {
                asnswer.IsTrue = false;
            }
            else 
            {
                ClassVariablesTest.questionModels.Where(p => p == questionModel).First().Vibran.Remove(asnswer);
            }
            
        }

        private void BtDelAnsw_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ((StackPanel)this.Parent).Children.Remove(this);
            QuestionList.QuestionModel.asnswerModels.Remove(asnswer);
        }
    }
}
