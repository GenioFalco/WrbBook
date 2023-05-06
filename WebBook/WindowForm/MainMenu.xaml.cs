using System.Windows;
using System.Windows.Controls;
using WebBook.ClassesApp;
using WebBook.EntityFramework;
using WebBook.PageWindow;

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
            ConrolerBroadCast.test = null;
            fContainer.Navigate(new AddEditTestPage());
        }

        private void RbRes_Click(object sender, RoutedEventArgs e)
        {
            fContainer.Navigate(new ResultStudent());
        }
    }

    
   
}
