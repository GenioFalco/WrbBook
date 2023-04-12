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
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        public TaskList taskList;
        public ProfilePage profilePage;
        public MainMenu mainMenu;
        public HomePage homePage;
        public listUserPage userPage;
        public TestPage testPage;
        public AnswerPractical answerPractical;
        public AuthorizationPage()
        {
            InitializeComponent();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            PasswordMask.Visibility = Visibility.Visible;
            Password.Visibility = Visibility.Hidden;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            PasswordMask.Visibility = Visibility.Collapsed;
            Password.Visibility = Visibility.Visible;
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Authorization.frame.Navigate(new RegistrationPage());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            User user = DataBase.webBookEntities.User.FirstOrDefault(x => x.EmailUser == Email.Text && x.PasswordUser == Password.Password && x.PasswordUser == PasswordMask.Text);
            if (user == null)
            {
                MessageBox.Show("Неверные данные");
                return;
            }

            MainMenu.user = user;
            ProfilePage.user = user;
            HomePage.user = user;
            listUserPage.user = user;
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


        //Для передачи данных между двумя поялми Password и PasswordMask
        private void Password_TextChanged(object sender, TextChangedEventArgs e)
        {
            PasswordMask.Text = Password.Password;
        }

        private void PasswordMask_TextChanged(object sender, TextChangedEventArgs e)
        {
            Password.Password = PasswordMask.Text;
        }

        private void TextBlock_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            Authorization.frame.Navigate(new ResetPage());
        }

        private void TextBlock_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            if (Password.Password == "0000")
            {
                MainMenu mainMenu = new MainMenu();
                mainMenu.Show();
                var Closes = Authorization.GetWindow(this);
                Closes.Close();
            }
            else
            {
                MessageBox.Show("Неверный код!");
            }
        }
    }
}
