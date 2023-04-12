using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
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
using WebBook.WindowForm;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace WebBook.PageWindow
{
    /// <summary>
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
      
        public static User user  = null;
        public ProfilePage()
        {
            InitializeComponent();
            InitilizateVivod();
        }

        public void InitilizateVivod()
        {
            if (user != null)
            {
                NameEdit.Text = user.NameUser;
                LastNameEdit.Text = user.SurnameUser;
                EmailEdit.Text = user.EmailUser;
                PasswordEdit.Password = user.PasswordUser;
                WordEdit.Text = user.SecretWordUser;
                PatronymicEdit.Text = user.PasswordUser;
                if (PhotoConvert.loagPhoto(user.ImageUser) == null)
                {
                   PhotoUser.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/WebBooNoText.png"));
                }
                else
                {
                    PhotoUser.ImageSource = PhotoConvert.loagPhoto(user.ImageUser);
                }
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

            if (!Checks.CheckNull(PasswordMaskEdit.Text, "Поле пароль")) return;
            user.PasswordUser = PasswordMaskEdit.Text;

            if (!Checks.Email(EmailEdit.Text, "Поле почта")) return;
            user.EmailUser = EmailEdit.Text;

            if (!Checks.CheckNull(WordEdit.Text, "Поле сектретный код")) return;
            user.SecretWordUser = WordEdit.Text;
           
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

      
    }
}
