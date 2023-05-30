using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Text.RegularExpressions;
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
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace WebBook.PageWindow
{
    /// <summary>
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        int CountTest = DataBase.webBookEntities.Test.ToList().Count;

        int CountTask = DataBase.webBookEntities.Task.ToList().Count;

        public static User user  = null;
        public ProfilePage()
        {
            InitializeComponent();
            InitilizateVivod();

            if(user.RoleUser == 1) 
            {
                spgr.Visibility = Visibility.Collapsed;
            }
        }

        public void InitilizateVivod()
        {
            if (user != null)
            {
                NameEdit.Text = user.NameUser;
                LastNameEdit.Text = user.SurnameUser;
                EmailEdit.Text = user.EmailUser;
                PasswordEdit.Password = user.PasswordUser;
                PatronymicEdit.Text = user.PatronymicUser;
                GroupEdit.Text = user.GroupUser;
                if (PhotoConvert.loagPhoto(user.ImageUser) == null)
                {
                    PhotoUser.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/WebBooNoText.png"));
                }
                else
                {

                    PhotoUser.ImageSource = PhotoConvert.loagPhoto(user.ImageUser);
                }

                PercentTest.Maximum = CountTest;

                var resultstest = DataBase.webBookEntities.Results.Where(r => r.IdUser == user.IDUser).ToList();

                PercentTest.Value = resultstest.Count();

                PercentTask.Maximum = CountTask;

                var resultstask = DataBase.webBookEntities.AnswerPractical.Where(r => r.IdUser == user.IDUser).ToList();

                PercentTask.Value = resultstask.Count();
            }
            else
            {
                user = new User();
            }


        }

        public void Save()
        {
            if (!Checks.TolkBukva(NameEdit.Text, "Поле имя")) return;
            user.NameUser = NameEdit.Text;

            if (!Checks.TolkBukva(LastNameEdit.Text, "Поле фамилия")) return;
            user.SurnameUser = LastNameEdit.Text;

            if (!Checks.TolkBukva(PatronymicEdit.Text, "Поле отчество")) return;
            user.PatronymicUser = PatronymicEdit.Text;

            if (!Checks.CheckNull(PasswordMaskEdit.Text, "Поле пароль")) return;
            user.PasswordUser = PasswordMaskEdit.Text;

            if (!Checks.WordAndNumber(GroupEdit.Text, "Поле группа")) return;
            user.GroupUser = GroupEdit.Text;


            string email = EmailEdit.Text;

            // регулярное выражение для проверки формата e-mail
            string emailPattern = @"^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}$";

            // проверяем, соответствует ли введенный адрес формату
            if (!Regex.IsMatch(email, emailPattern))
            {
                MessageBox.Show("Введенный e-mail адрес неправильный.");
                return;
            }


            if (!IsValidEmail(EmailEdit.Text))
            {
                MessageBox.Show("Адрес электронной почты недействителен."); return;
            }


            user.EmailUser = EmailEdit.Text;

            DataBase.webBookEntities.User.AddOrUpdate(user);
            DataBase.webBookEntities.SaveChanges();
            
        }

       

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select a picture";
            openFileDialog.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                "JPEG (*.jpg;*jpeg)|*.jpg;*.jpeg|" +
                "Portable Network Graphic(*.png)|*.png";
            bool? myResult;
            myResult = openFileDialog.ShowDialog();
            if (myResult != null && myResult == true)
            {
                PhotoVivod.Text = openFileDialog.FileName;
                if (PhotoVivod.Text.ToString() != "Выбрать" && PhotoVivod.Text.ToString() != "")
                {
                    byte[] bytesPhoto = File.ReadAllBytes(PhotoVivod.Text.ToString());
                    user.ImageUser = bytesPhoto;
                }
                InitilizateVivod();
            }
            else
            {
                MessageBox.Show("Выберите изображение");
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            PasswordMaskEdit.Visibility = Visibility.Visible;
            PasswordEdit.Visibility = Visibility.Hidden;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            PasswordMaskEdit.Visibility = Visibility.Collapsed;
            PasswordEdit.Visibility = Visibility.Visible;
        }

        private void PasswordEdit_TextChanged(object sender, TextChangedEventArgs e)
        {
            PasswordMaskEdit.Text = PasswordEdit.Password;
        }

        private void PasswordMaskEdit_TextChanged(object sender, TextChangedEventArgs e)
        {
            PasswordEdit.Password = PasswordMaskEdit.Text;
        }

        private void BtSave_Click(object sender, RoutedEventArgs e)
        {
            Save();

        }

        private void BtSadve_Click(object sender, RoutedEventArgs e)
        {
     
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
