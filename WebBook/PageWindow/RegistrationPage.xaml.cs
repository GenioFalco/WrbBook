using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
using WebBook.WindowForm;

namespace WebBook.PageWindow
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public User user = null;

        List<string> RoleTitle;
        public RegistrationPage()
        {
            InitializeComponent();

            if(ConrolerBroadCast.CheckRegUser == true)
            {
                SPRole.Visibility = Visibility.Visible;
                WhatLogIn.Visibility = Visibility.Collapsed;
            }
           

            RoleTitle = DataBase.webBookEntities.Role.Select(x => x.TitleRole).Distinct().ToList();
            foreach (var item in RoleTitle)
            {
                RoleReg.Items.Add(item);
            }
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Authorization.frame.Navigate(new AuthorizationPage());
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            PasswordMask.Visibility = Visibility.Visible;
            PasswordReg.Visibility = Visibility.Hidden;
            PasswordMask.Text = PasswordReg.Password;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            PasswordMask.Visibility = Visibility.Collapsed;
            PasswordReg.Visibility = Visibility.Visible;
            PasswordReg.Password = PasswordMask.Text;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddUser();
        }

       
        public void AddUser()
        {

            if (DataBase.webBookEntities.User.Any(x => x.EmailUser == EmailReg.Text))
            {
                MessageBox.Show("Почта уже зарегистрирована");
                return;
            }

            user = new User();

            if (!Checks.TolkBukva(NameReg.Text,"Поле Имя")) return;
            user.NameUser = NameReg.Text;


            string email = EmailReg.Text;

            // регулярное выражение для проверки формата e-mail
            string emailPattern = @"^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}$";

            // проверяем, соответствует ли введенный адрес формату
            if (System.Text.RegularExpressions.Regex.IsMatch(email, emailPattern))
            {
               
            }
            else
            {
                MessageBox.Show("Введенный e-mail адрес неправильный.");
                return;
            }
        
            user.EmailUser = EmailReg.Text;

            if (!Checks.CheckNull(PasswordReg.Password, "Поле пароль")) return;
            user.PasswordUser = PasswordReg.Password;

            if (RoleReg.SelectedValue == null) 
            {
                user.RoleUser = 2;
            }
            else 
            {
                var roleId = DataBase.webBookEntities.Role.Where(x => x.TitleRole == RoleReg.SelectedValue.ToString()).Select(id => id.IDRole).FirstOrDefault();
                user.RoleUser = Convert.ToInt32(roleId);
            }

            DataBase.webBookEntities.User.AddOrUpdate(user);
            DataBase.webBookEntities.SaveChanges();

            MessageBox.Show("Пользователь зарегистрирован");

     

            Authorization.frame.Navigate(new AuthorizationPage());







        }

        private void PasswordReg_TextChanged(object sender, TextChangedEventArgs e)
        {
            PasswordMask.Text = PasswordReg.Password;
        }

        private void PasswordMask_TextChanged(object sender, TextChangedEventArgs e)
        {
            PasswordReg.Password = PasswordMask.Text;
        }
    }
}
