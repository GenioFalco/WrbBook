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
using WebBook.EntityFramework;
using WebBook.PageWindow;
using WebBook.WindowForm;

namespace WebBook.UserControlUI
{
    /// <summary>
    /// Логика взаимодействия для TopicList.xaml
    /// </summary>
    public partial class TopicList : UserControl
    {
        public TopicsPage topicsPage;
        public HomePage homePage;
        Topic topic = null;

        public AddEditTopicPage addEditTopicPage = null;
        public TopicList(Topic topic)
        {
            InitializeComponent();
            this.topic = topic;

        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                List<Topic> topics = DataBase.webBookEntities.Topic.Where(x => x.IDTopic == topic.IDTopic).ToList();
                DataBase.webBookEntities.Topic.Remove(topics[0]);
                DataBase.webBookEntities.SaveChanges();
                homePage.VivodTopic();
            }
            catch
            {
                MessageBox.Show("Удалить нельзя");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TopicsPage.topic = topic;
            MainMenu.frame.Navigate(new TopicsPage());
        }

        private void Border_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            ConrolerBroadCast.CheckAdd = true;
            ConrolerBroadCast.topic = topic;
            MainMenu.frame.Navigate(new AddEditTopicPage());
        }
    }
}
