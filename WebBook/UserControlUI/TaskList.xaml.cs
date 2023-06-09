using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
    /// Логика взаимодействия для TaskList.xaml
    /// </summary>
    public partial class TaskList : UserControl
    {
        

        public static User user = null;

        public TaskPage taskPage;
        public HomePage homePage;
        EntityFramework.Task task = null;

        public AddEditTaskPage addEditTaskPage = null;

        public int schet = 0;
        public TaskList(EntityFramework.Task task)
        {
            InitializeComponent();
            this.task = task;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TaskPage.task = task;
            MainMenu.frame.Navigate(new TaskPage());
        }

       
        
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите удалить задание?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    if (DataBase.webBookEntities.AnswerPractical.Any(x => x.IdTask == task.IDTask))
                    {
                        MessageBox.Show("Нельзя удалить");
                        return;
                    }
                    DataBase.webBookEntities.Task.Remove(task);
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

        private void Border_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            ConrolerBroadCast.CheckAdd = true;
            ConrolerBroadCast.task = task;
            MainMenu.frame.Navigate(new AddEditTaskPage());
        }

        private void CbCheckPercent_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        private void CbCheckPercent_Unchecked(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
