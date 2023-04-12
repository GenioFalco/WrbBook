using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
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
using static System.Net.Mime.MediaTypeNames;

namespace WebBook.PageWindow
{
    /// <summary>
    /// Логика взаимодействия для AddEditTopicPage.xaml
    /// </summary>
    public partial class AddEditTopicPage : Page
    {
        public static Topic topic = null;

        OpenFileDialog ofd = new OpenFileDialog();
        OpenFileDialog ofdv = new OpenFileDialog();

        bool? myResult2;
        bool? myResult3;

        public AddEditTopicPage()
        {
            InitializeComponent();
            topic = ConrolerBroadCast.topic;
            InitilizateVivod();
            CheckAdd();
        }

        public void InitilizateVivod()
        {
            if (topic != null)
            {
                Title1.Text = topic.TitleTopic;
                Description.Text = topic.DescriptionTopic;
            }
            else
            {
                topic = new Topic();
            }
        }

        private bool Check(string str, string message)
        {
            if (!string.IsNullOrEmpty(str))
            {
                return true;
            }
            MessageBox.Show("Пустая строка: " + message);
            return false;
        }

        public void CheckAdd() 
        {
            if (ConrolerBroadCast.CheckAdd == false)
            {
                Edit.Visibility = Visibility.Collapsed;
                AddT.Visibility = Visibility.Visible;
            }
            else 
            {
                Edit.Visibility = Visibility.Visible;
                AddT.Visibility = Visibility.Collapsed;

            }

        }

        public void AddEdTopic() 
        {

            if (ConrolerBroadCast.CheckAdd == false) 
            {
                if (DataBase.webBookEntities.Topic.Any(x => x.TitleTopic == Title1.Text))
                {
                    MessageBox.Show("Тема уже добавлена");
                    return;
                }

                if (!Checks.LetteLatinAndCyrillic(Title1.Text, "Поле наименование темы")) return;
                topic.TitleTopic = Title1.Text;

                if (!Checks.LetteLatinAndCyrillic(Description.Text, "Поле описание")) return;
                topic.DescriptionTopic = Description.Text;

                if (myResult2 == null)
                {
                    MessageBox.Show("Выберите документ"); return;
                }

                DataBase.webBookEntities.Topic.AddOrUpdate(topic);
                DataBase.webBookEntities.SaveChanges();

                MessageBox.Show("Тема добавлена");
            }
            else 
            {
                Edit.Visibility = Visibility.Visible;
                AddT.Visibility = Visibility.Collapsed;

                if (!Checks.LetteLatinAndCyrillic(Title1.Text, "Поле наименование темы")) return;
                topic.TitleTopic = Title1.Text;

                if (!Checks.LetteLatinAndCyrillic(Description.Text, "Поле описание")) return;
                topic.DescriptionTopic = Description.Text;

                DataBase.webBookEntities.Topic.AddOrUpdate(topic);
                DataBase.webBookEntities.SaveChanges();

                MessageBox.Show("Тема обновлена"); return;
            }
           
        }
        private void BtSave_Click(object sender, RoutedEventArgs e)
        {
            AddEdTopic();
        }


        
        private void OpenDocument_Click(object sender, RoutedEventArgs e)
        {
            ofd.Filter = "Select a rtf (*.rtf)|*.rtf|All files (*.*)|*.*";

            myResult2 = ofd.ShowDialog();
            if (myResult2 != null && myResult2 == true)
            {
                OpenDocument.Content = ofd.SafeFileName;
                string filePath = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\bin\\Debug\\LectureRtf\\" + ofd.SafeFileName;
                MessageBox.Show(filePath);
                File.Copy(ofd.FileName, filePath, true);
                topic.LectureTopic = @"LectureRtf\" + ofd.SafeFileName;

            }

        }

        private void OpenVideo_Click(object sender, RoutedEventArgs e)
        {
            ofdv.Filter = "Select a video (*.mp4)|*.mp4|All files (*.*)|*.*";
            //bool? myResult;
            myResult3 = ofdv.ShowDialog();
            if (myResult3 != null && myResult3 == true)
            {
                OpenVideo.Content = ofdv.FileName; ;
                string filePath = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\VideoMaterial\\" + ofdv.SafeFileName;
                MessageBox.Show(filePath);
                File.Copy(ofdv.FileName, filePath, true);
                topic.UrlVideoTopic = @"VideoMaterial\" + ofdv.SafeFileName;
            }
     
        }

      

     
    }
}
