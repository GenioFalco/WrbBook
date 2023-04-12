using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Логика взаимодействия для listUserPage.xaml
    /// </summary>
    public partial class listUserPage : Page
    {
        public static User user = null;
        string like = "";
        public listUserPage()
        {
            InitializeComponent();
            Vivod();
            ProfileVivod();
        }

        public void ProfileVivod()
        {
            if (user != null)
            {
                UserName.Text = user.NameUser;
                if (PhotoConvert.loagPhoto(user.ImageUser) == null)
                {
                    UserPhoto.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/WebBooNoText.png"));
                }
                else
                {
                    UserPhoto.ImageSource = PhotoConvert.loagPhoto(user.ImageUser);
                }
            }
            else
            {
                user = new User();
            }

        }


        public void Vivod()
        {
            UserOutput.Children.Clear();
            List<User> users = DataBase.webBookEntities.User.Where(obj => DbFunctions.Like(obj.NameUser, "%" + like + "%")).ToList();
            foreach (var item in users)
            {
                UserList userList = new UserList(item);
                userList.listUserPage = this;
                userList.NameUserList.Text = item.NameUser;
                userList.LastNameUserList.Text = item.SurnameUser;
                userList.EmailUserList.Text = item.EmailUser;
                userList.PatronymicUserList.Text = item.PatronymicUser;
                var RoleId = DataBase.webBookEntities.Role.Where(x => x.IDRole == item.RoleUser).Select(id => id.TitleRole).FirstOrDefault();
                userList.RolelUserList.Text = RoleId;
                if (PhotoConvert.loagPhoto(item.ImageUser) == null)
                {

                    userList.UserPhotoList.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/WebBooNoText.png"));

                }
                else
                {
                    userList.UserPhotoList.ImageSource = PhotoConvert.loagPhoto(item.ImageUser);
                }

                UserOutput.Children.Add(userList);
            }

        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            like = Search.Text;
            Vivod();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ConrolerBroadCast.CheckRegUser = true;
            MainMenu.frame.Navigate(new RegistrationPage());
        }
    }
}
