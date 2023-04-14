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
            //string resetCode = GenerateResetCode();
            Random random = new Random();
            int randomNumber = random.Next(100000, 999999);


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



        private string GenerateResetCode()
        {
            // генерируем случайный код из 6 символов
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());
        }


        private void BSingIN_Click(object sender, RoutedEventArgs e)
        {

            //MainMenu mainMenu = new MainMenu();
            //    mainMenu.Show();
            //    var Closes = Authorization.GetWindow(this);
            //    Closes.Close();

        }



    }
}
