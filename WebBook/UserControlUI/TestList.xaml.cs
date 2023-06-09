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
using WebBook.WindowForm;

namespace WebBook.UserControlUI
{
    /// <summary>
    /// Логика взаимодействия для TestList.xaml
    /// </summary>
    public partial class TestList : UserControl
    {
        public HomePage homePage;

        public Test test;
        public TestList(Test test)
        {
            InitializeComponent();
            this.test = test;
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ConrolerBroadCast.test = test;
            ConrolerBroadCast.CheckTest = true;
            MainMenu.frame.Navigate(new TestPage());
        }

        private void Border_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            ConrolerBroadCast.CheckTest = false;
            ConrolerBroadCast.test = test;
            MainMenu.frame.Navigate(new AddEditTestPage());
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите удалить задание?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    if (DataBase.webBookEntities.Results.Any(x => x.IdTest == test.IdTest))
                    {
                        MessageBox.Show("Нельзя удалить");
                        return;
                    }
                    DataBase.webBookEntities.Test.Remove(test);
                    DataBase.webBookEntities.SaveChanges();
                    homePage.VivodTopic();
                }
                catch
                {
                    MessageBox.Show("Удалить нельзя");
                }

            }
            else
            {
                return;
            }
           
        }
    }
}
