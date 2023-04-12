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
using System.Windows.Shapes;
using WebBook.ClassesApp;
using WebBook.ClassesApp.Models;
using WebBook.EntityFramework;
using WebBook.PageWindow;
using WebBook.UserControlUI;
using Xceed.Wpf.AvalonDock.Themes;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;

namespace WebBook.WindowForm
{
    /// <summary>
    /// Логика взаимодействия для MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        public static User user = null;
        public static Frame frame;

        public MainMenu()
        {
            InitializeComponent();
            frame = fContainer;

            fContainer.Navigate(new HomePage());
            CheckUserRole();

        }


        public void CheckUserRole() 
        {
            if (user.RoleUser == 2) 
            {
                RbUsers.Visibility = Visibility.Collapsed;
                RbRes.Visibility = Visibility.Collapsed;
                RbAddTask.Visibility = Visibility.Collapsed;
                RbAddTest.Visibility = Visibility.Collapsed;
                RbAddTopic.Visibility = Visibility.Collapsed;
            }

            if(user.RoleUser == 1) {RbUsers.Visibility = Visibility.Collapsed;}

            //if(ConrolerBroadCast.CheckAdmin == true) 
            //{
            //    RbHome.Visibility = Visibility.Collapsed;
            //    RbUser.Visibility = Visibility.Collapsed;
            //    RbRes.Visibility = Visibility.Collapsed;
            //    RbAddTask.Visibility = Visibility.Collapsed;
            //    RbAddTest.Visibility = Visibility.Collapsed;
            //    RbAddTopic.Visibility = Visibility.Collapsed;
            //}
        }

        private void rdHome_Click(object sender, RoutedEventArgs e)
        {
            fContainer.Navigate(new HomePage());
        }

        private void rdUser_Click(object sender, RoutedEventArgs e)
        {
            fContainer.Navigate(new ProfilePage());
        }

        private void rdExit_Click(object sender, RoutedEventArgs e)
        {
            ConrolerBroadCast.CheckRegUser = false;
            Authorization authorization = new Authorization();
            authorization.Show();
            Close();
        }

        private void rdUsers_Click(object sender, RoutedEventArgs e)
        {
            fContainer.Navigate(new listUserPage());
        }

        private void RdAddTopic_Click(object sender, RoutedEventArgs e)
        {
            ConrolerBroadCast.CheckAdd = false;
            ConrolerBroadCast.topic = null;
            fContainer.Navigate(new AddEditTopicPage());
        }

        private void RdAddTask_Click(object sender, RoutedEventArgs e)
        {
            ConrolerBroadCast.CheckAdd = false;
            ConrolerBroadCast.task = null;
            fContainer.Navigate(new AddEditTaskPage());
        }

        private void RdAddTest_Click(object sender, RoutedEventArgs e)
        {
            ConrolerBroadCast.CheckAdd = false;
            ConrolerBroadCast.CheckTest = false;
            fContainer.Navigate(new AddEditTestPage());
        }

        private void RbRes_Click(object sender, RoutedEventArgs e)
        {
            fContainer.Navigate(new ResultStudent());
        }
    }

    
   
}
