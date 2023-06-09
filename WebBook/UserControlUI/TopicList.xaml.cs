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
using static System.Net.Mime.MediaTypeNames;

namespace WebBook.UserControlUI
{
    /// <summary>
    /// Логика взаимодействия для TopicList.xaml
    /// </summary>
    public partial class TopicList : UserControl
    {
        public TopicsPage topicsPage;
        public HomePage homePage;
        public Topic topic = new Topic();

        public AddEditTopicPage addEditTopicPage = null;
        public TopicList(Topic topic)
        {
            InitializeComponent();
            this.topic = topic;

        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите удалить тему?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try 
                {
                    if (DataBase.webBookEntities.Test.Any(x => x.TopicTest == topic.IDTopic))
                    {
                        MessageBox.Show("Нельзя удалить");
                        return;
                    }

                    if (DataBase.webBookEntities.Task.Any(x => x.TopicTask == topic.IDTopic))
                    {
                        MessageBox.Show("Нельзя удалить");
                        return;
                    }

                    DataBase.webBookEntities.Topic.Remove(topic);
                    DataBase.webBookEntities.SaveChanges();
                    homePage.VivodTopic();
                }
                catch
                {
                    MessageBox.Show("Нельзя удалить");
                }

            }
            else
            {
                return;
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
