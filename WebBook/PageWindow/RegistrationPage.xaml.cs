using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WebBook.ClassesApp;
using WebBook.EntityFramework;
using WebBook.WindowForm;
using System.Windows.Media;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Net.NetworkInformation;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using System.Net.Http;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;

namespace WebBook.PageWindow
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public User user = null;

       
        public RegistrationPage()
        {
            InitializeComponent();

        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowForm.Authorization.frame.Navigate(new AuthorizationPage());
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

            if (!Checks.TolkBukva(NameReg.Text, "Поле Имя")) return;
            user.NameUser = NameReg.Text;


            string email = EmailReg.Text;

            // регулярное выражение для проверки формата e-mail
            string emailPattern = @"^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}$";

            // проверяем, соответствует ли введенный адрес формату
            if (!Regex.IsMatch(email, emailPattern))
            {
                MessageBox.Show("Введенный e-mail адрес неправильный.");
                return;
            }


            if (!IsValidEmail(EmailReg.Text))
            {
                MessageBox.Show("Адрес электронной почты недействителен.");return;
            }


            user.EmailUser = EmailReg.Text;

            if (!Checks.CheckNull(PasswordReg.Password, "Поле пароль")) return;
            user.PasswordUser = PasswordReg.Password;

            user.RoleUser = 2;

            if (!Checks.WordAndNumber(GroupReg.Text, "Поле группа")) return;
            user.GroupUser = GroupReg.Text;


            DataBase.webBookEntities.User.AddOrUpdate(user);
            DataBase.webBookEntities.SaveChanges();

            MessageBox.Show("Пользователь зарегистрирован");

            WindowForm.Authorization.frame.Navigate(new AuthorizationPage());
        }
      
        private void PasswordReg_TextChanged(object sender, TextChangedEventArgs e)
        {
            PasswordMask.Text = PasswordReg.Password;
        }

        private void PasswordMask_TextChanged(object sender, TextChangedEventArgs e)
        {
            PasswordReg.Password = PasswordMask.Text;
        }

        public static bool IsValidEmail(string email)
        {
            try
            {
                SmtpClient Smtp = new SmtpClient("smtp.mail.ru", 25);
                Smtp.Credentials = new NetworkCredential("shoping_re_airpods@mail.ru", "u5cKGbsVmrEnt9ktBVuW");
                Smtp.EnableSsl = true;
                //Формирование письма
                MailMessage Message = new MailMessage();
                Message.From = new MailAddress("shoping_re_airpods@mail.ru");
                Message.To.Add(new MailAddress(email));
                Message.Subject = "Проверка действительности почты";
                string body = "<html><head><title>Проверка действительности почты</title></head><body>";
                body += "<div style='text-align:center;'><img src='\"/Веб Бук;component/Resources/WebBooNoText.png\"' alt='Logo'></div>";
                body += "<p>Уважаемый пользователь,</p><p>Данное сообщение было отправлено для проверки действительности данной почты</p>";
                body += "</p><p>Если вы не регистрировались в Веб Бук, то просто проигнорируйте это сообщение.</p><p>С уважением,<br>Команда Веб Бук</p>";
                body += "</body></html>";
                Message.Body = body;
                Message.IsBodyHtml = true;

                Message.DeliveryNotificationOptions = DeliveryNotificationOptions.Never;
                Smtp.Send(Message);

                // Если адрес электронной почты существует, возвращаем true
                return true;
            }
            catch (SmtpFailedRecipientException)
            {
                // Если адрес электронной почты не существует, возвращаем false
                return false;
            }
            catch (SmtpException)
            {
                // Если произошла ошибка при проверке адреса электронной почты, возвращаем false
                return false;
            }
        }



    }
}
