using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WebBook.ClassesApp;
using WebBook.EntityFramework;
using Xceed.Wpf.Toolkit;
using static System.Net.Mime.MediaTypeNames;
using MessageBox = System.Windows.MessageBox;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using Path = System.IO.Path;

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
                string relativePath = @"..\..\LectureRtf\"; // две точки - это подняться на уровень выше, затем перейти в папку LectureRtf

                string fileName = Path.GetFileName(ofd.FileName);
                string filePath = Path.Combine(relativePath, fileName);
                File.Copy(ofd.FileName, filePath);


                //string path = @"LectureRtf";
                //Directory.CreateDirectory(path);

                //string sourceFile = ofd.FileName;
                //string destinationPath = @"LectureRtf\" + System.IO.Path.GetFileName(sourceFile);
                //string absolutePath = System.IO.Path.Combine(Environment.CurrentDirectory, destinationPath);
                //File.Copy(sourceFile, absolutePath, true);


                //string sourceFile = ofd.FileName;
                //string destinationFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LectureRtf");
                //if (!Directory.Exists(destinationFolder))
                //{
                //    Directory.CreateDirectory(destinationFolder);
                //}
                //string destinationFile = System.IO.Path.Combine(destinationFolder, System.IO.Path.GetFileName(sourceFile));
                //File.Copy(sourceFile, destinationFile, true);


                //OpenDocument.Content = ofd.SafeFileName;
                //string filePath = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\LectureRtf\\" + ofd.SafeFileName;
                //MessageBox.Show(filePath);
                //File.Copy(ofd.FileName, filePath, true);
                //topic.LectureTopic = @"LectureRtf\" + ofd.SafeFileName;
                topic.LectureTopic = filePath;

            }

        }

        private void OpenVideo_Click(object sender, RoutedEventArgs e)
        {
           
     
        }

      

     
    }
}
