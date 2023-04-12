using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
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
using WebBook.UserControlUI;
using WebBook.WindowForm;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace WebBook.PageWindow
{
    /// <summary>
    /// Логика взаимодействия для HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public static User user = null;
        public Topic topic = null;
        public Test test = null;
        public EntityFramework.Task task = null;
        string like = "";
        int choise = 0;

        public HomePage()
        {
            InitializeComponent();
            VivodTopic();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            choise = 0;
            VivodTopic();
        }

        public void VivodTopic()
        {  
            UserName.Text = user.NameUser;

            if (PhotoConvert.loagPhoto(user.ImageUser) == null)
            {
                UserPhoto.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/TopicImg.png"));
            }
            else
            {
                UserPhoto.ImageSource = PhotoConvert.loagPhoto(user.ImageUser);
            }

            if(choise == 0)
            {
                ListView.Children.Clear();
                List<Topic> topics = DataBase.webBookEntities.Topic.Where(obj => DbFunctions.Like(obj.TitleTopic, "%" + like + "%")).ToList();
                foreach (var item in topics)
                {
                    TopicList topicList = new TopicList(item);
                    topicList.homePage = this;
                    topicList.TitleTopic.Text = item.TitleTopic;
                    topicList.ImageTopic.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/TopicImg.png"));
                    if(user.RoleUser == 2) 
                    {
                        topicList.TeachBt.Visibility = Visibility.Collapsed;
                    }
                    ListView.Children.Add(topicList);
                }
            }
            else 
            {

                if(choise == 1)
                {
                    ListView.Children.Clear();
                    List<Test> tests = DataBase.webBookEntities.Test.Where(obj => DbFunctions.Like(obj.TitleTest, "%" + like + "%")).ToList();
                    foreach (var item in tests)
                    {
                        TestList testList = new TestList(new ClassesApp.Models.TestModel(item));
                        testList.homePage = this;
                        testList.TitleTest.Text = item.TitleTest;   
                        testList.ImageTest.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/TestImg.png"));
                        if (DataBase.webBookEntities.Results.Any(x => x.IdUser == user.IDUser) && DataBase.webBookEntities.Results.Any(x => x.IdTest == item.IdTest))
                        {
                            testList.OpenTest.Background = new SolidColorBrush(Colors.Gray);
                            testList.OpenTest.IsEnabled = false;
                        }
                        if (user.RoleUser == 2)
                        {
                            testList.ControlTeach.Visibility = Visibility.Collapsed;
                        }
                        ListView.Children.Add(testList);
                    }
                }

                else if(choise == 2) 
                {
                    ListView.Children.Clear();
                    List<EntityFramework.Task> tasks = DataBase.webBookEntities.Task.Where(obj => DbFunctions.Like(obj.TitleTask, "%" + like + "%")).ToList();
                    foreach (var item in tasks)
                    {
                        TaskList taskList = new TaskList(item);
                        taskList.homePage = this;
                        taskList.TitleTask.Text = item.TitleTask;
                        taskList.ImageTopic.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/TaskImg.png"));
                        if (user.RoleUser == 2)
                        {
                            taskList.ConrolPanel.Visibility = Visibility.Collapsed;
                        }
                        ListView.Children.Add(taskList);
                    }
                }
                
            }  
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            like = Search.Text;
            VivodTopic();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            choise = 1;
            VivodTopic();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MainMenu.frame.Navigate(new AddEditTopicPage());
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            choise = 2;
            VivodTopic();
        }
    }
}
    

