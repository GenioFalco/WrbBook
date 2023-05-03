using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Net;
using Authorization = WebBook.WindowForm.Authorization;
using System.Web;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using WebBook.EntityFramework;
using WebBook.ClassesApp;
using MessageBox = System.Windows.MessageBox;
using System.Data.Entity.Migrations;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace WebBook.PageWindow
{
    /// <summary>
    /// Логика взаимодействия для ResetPage.xaml
    /// </summary>
    public partial class ResetPage : Page
    {
        User user;
        private int randomNumber;

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
            string email = EmailReset.Text;

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

            user = DataBase.webBookEntities.User.FirstOrDefault(x => x.EmailUser == EmailReset.Text);
            if (user == null)
            {
                MessageBox.Show("Учётная запись не существует");
                return;
            }
            else
            {
                SCodeReset.Visibility = Visibility.Visible;
                BNextPass.Visibility = Visibility.Visible;

                SPMail.Visibility = Visibility.Collapsed;
                BNext.Visibility = Visibility.Collapsed;
            }

            //string resetCode = GenerateResetCode();
            Random random = new Random();
            randomNumber = random.Next(100000, 999999);


            SmtpClient Smtp = new SmtpClient("smtp.mail.ru", 25);
            Smtp.Credentials = new NetworkCredential("shoping_re_airpods@mail.ru", "u5cKGbsVmrEnt9ktBVuW");
            //Smtp.EnableSsl = false;
            Smtp.EnableSsl = true;
            //Формирование письма
            MailMessage Message = new MailMessage();
            Message.From = new MailAddress("shoping_re_airpods@mail.ru");
            Message.To.Add(new MailAddress(EmailReset.Text));
            Message.Subject = "Сброс пароля";

            string body = "<html><head><title>Сброс пароля</title></head><body>";
            body += "<div style='text-align:center;'><img src='\"/WebBook;component/Resources/WebBooNoText.png\"' alt='Logo'></div>";
            body += "<p>Уважаемый пользователь,</p><p>Вы запросили сброс пароля в нашем приложении. Используйте следующий код для сброса пароля:</p>";
            body += $"<p style='font-size:18px; color:#003C61; font-weight:bold;'>{randomNumber}</p><p>Если вы не запрашивали сброс пароля, то просто проигнорируйте это сообщение.</p><p>С уважением,<br>Команда WebBook</p>";
            body += "</body></html>";
            Message.Body = body;

            Message.IsBodyHtml = true;

            Smtp.Send(Message);//отправка

        }
        private void BSingIN_Click(object sender, RoutedEventArgs e)
        {

            if (CodeReset.Text != randomNumber.ToString())
            {
                MessageBox.Show("Введён не верный код");
                return;
            }


            if (CodeReset.Text ==  null)
            {
                MessageBox.Show("Введbnt код");
                return;
            }
            SCodeReset.Visibility = Visibility.Collapsed;
            BNextPass.Visibility = Visibility.Collapsed;

            SPNewPass.Visibility = Visibility.Visible;
            BSavesPass.Visibility = Visibility.Visible;


        }

        private void Password_TextChanged(object sender, TextChangedEventArgs e)
        {
            PasswordMask.Text = NewPassword.Password;
        }

        private void PasswordMask_TextChanged(object sender, TextChangedEventArgs e)
        {
            NewPassword.Password = PasswordMask.Text;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            PasswordMask.Visibility = Visibility.Collapsed;
            NewPassword.Visibility = Visibility.Visible;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            PasswordMask.Visibility = Visibility.Visible;
            NewPassword.Visibility = Visibility.Hidden;
        }

        private void BSavesPass_Click(object sender, RoutedEventArgs e)
        {
            if (NewPassword.Password == null)
            {
                MessageBox.Show("Введите новый пароль");
                return;
            }

            user.PasswordUser = NewPassword.Password;

            DataBase.webBookEntities.User.AddOrUpdate(user);
            DataBase.webBookEntities.SaveChanges();

            MessageBox.Show("Пароль обновлён. Не зыбывай больше )");

            Authorization.frame.Navigate(new AuthorizationPage());
        }
    }
}
