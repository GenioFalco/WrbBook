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
            if (DataBase.webBookEntities.User.Any(x => user.RoleUser == 1))
            {
                MessageBox.Show("Преподавателя удалить нельзя");
                return;
            }

            if (MessageBox.Show("Вы точно хотите удалить пользователя?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    if (DataBase.webBookEntities.Results.Any(x => x.IdUser == user.IDUser))
                    {
                        MessageBox.Show("Нельзя удалить");
                        return;
                    }
                    if (DataBase.webBookEntities.AnswerPractical.Any(x => x.IdUser == user.IDUser))
                    {
                        MessageBox.Show("Нельзя удалить");
                        return;
                    }

                    DataBase.webBookEntities.User.Remove(user);
                    DataBase.webBookEntities.SaveChanges();
                    listUserPage.Vivod();
                }
                catch
                {
                    MessageBox.Show("Удалить нельзя");
                }
            }
            else
            {
                return;
            }
            
        }
    }
}
