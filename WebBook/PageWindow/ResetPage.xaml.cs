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
using WebBook.UserControlUI;
using WebBook.WindowForm;

namespace WebBook.PageWindow
{
    /// <summary>
    /// Логика взаимодействия для ResetPage.xaml
    /// </summary>
    public partial class ResetPage : Page
    {
        
        public ResetPage()
        {
            InitializeComponent();
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Authorization.frame.Navigate(new AuthorizationPage());
        }

        private void BNext_Click(object sender, RoutedEventArgs e)
        {
            User user = DataBase.webBookEntities.User.FirstOrDefault(x => x.EmailUser == EmailReset.Text);
            if (user == null)
            {
                MessageBox.Show("Неверные данные");
                return;
            }
            if (user.SecretWordUser == null)
            {
                MessageBox.Show("Секретное слово отсуствует \n Обратитесь к преподавателю");
                return;
            }
            SAnswerReset.Visibility = Visibility.Visible;
            BNext.Visibility = Visibility.Collapsed;
            BSingIN.Visibility = Visibility.Visible;

        }

        private void BSingIN_Click(object sender, RoutedEventArgs e)
        {
            User user = DataBase.webBookEntities.User.FirstOrDefault(x => x.EmailUser == EmailReset.Text && x.SecretWordUser == AnswerReset.Text);
            if (user == null)
            {
                MessageBox.Show("Неверные слово");
                return;
            }
            HomePage.user = user;
            MainMenu.user = user;
            ProfilePage.user = user;
            TestPage.user = user;
            TaskList.user = user;
            QuestionList.user = user;
            TaskPage.user = user;
            ResultStudent.user = user;
            AddEditTestPage.user = user;
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();

            var Closes = Authorization.GetWindow(this);
            Closes.Close();
        }
    }
}
