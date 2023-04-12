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

namespace WebBook.UserControlUI
{
    /// <summary>
    /// Логика взаимодействия для UserList.xaml
    /// </summary>
    public partial class UserList : UserControl
    {
        public listUserPage listUserPage;
        User user = null;
        public UserList(User user)
        {
            InitializeComponent();
            this.user = user;  
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (DataBase.webBookEntities.User.Any(x => user.RoleUser == 1))
                {
                    MessageBox.Show("Администратора удалить нельзя");
                    return;
                }
                List<User> users = DataBase.webBookEntities.User.Where(p => p.IDUser == user.IDUser).ToList();
                DataBase.webBookEntities.User.Remove(users[0]);
                DataBase.webBookEntities.SaveChanges();
                listUserPage.Vivod();
            }
            catch
            {
                MessageBox.Show("Удалить нельзя");
            }
        }
    }
}
