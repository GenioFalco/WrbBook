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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WebBook.ClassesApp;
using WebBook.EntityFramework;
using WebBook.WindowForm;
using MessageBox = System.Windows.MessageBox;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using Path = System.IO.Path;

namespace WebBook.PageWindow
{
    /// <summary>
    /// Логика взаимодействия для AddEditTaskPage.xaml
    /// </summary>
    public partial class AddEditTaskPage : Page
    {
        public static EntityFramework.Task task = null;

        public static Topic topic = null;

        OpenFileDialog ofd = new OpenFileDialog();
        bool? myResult;

        List<string> topicTitle;

        public AddEditTaskPage()
        {
            InitializeComponent();
            task = ConrolerBroadCast.task;
            InitilizateVivod();
            CheckAdd();
            LastDateTask.DataContext = DateTime.Now;

            topicTitle = DataBase.webBookEntities.Topic.Select(x => x.TitleTopic).Distinct().ToList();
            foreach (var item in topicTitle)
            {
                TopicTask.Items.Add(item);
            }

        }

        public void CheckAdd()
        {
            if (ConrolerBroadCast.CheckAdd == false)
            {
                AddT.Text = "Добавление";
            }
            else
            {
                AddT.Text = "Редактирование";
            }

        }

        public void InitilizateVivod()
        {
            if (task != null)
            {
                TitleTaskName.Text = task.TitleTask;
                LastDateTask.Value = task.LastDateTask;
                var topicId = DataBase.webBookEntities.Topic.Where(x => x.IDTopic == task.TopicTask).Select(id => id.TitleTopic).FirstOrDefault();

                TopicTask.SelectedValue = topicId;

            }
            else
            {
                task = new EntityFramework.Task();
            }
        }

        public void AddEdTopic()
        {

            if (ConrolerBroadCast.CheckAdd == false)
            {
                if (DataBase.webBookEntities.Task.Any(x => x.TitleTask == TitleTaskName.Text))
                {
                    MessageBox.Show("Задание уже добавлено");
                    return;
                }

                if (!Checks.LetteLatinAndCyrillic(TitleTaskName.Text, "Поле наименование задания")) return;
                task.TitleTask = TitleTaskName.Text;


                if (LastDateTask.Value == null) 
                {
                    MessageBox.Show("Выберите срок сдачи");return;
                }

                if (LastDateTask.Value <= DateTime.Now)
                {
                    MessageBox.Show("Выберите срок не раньше сегодня"); ; return;
                }

                task.LastDateTask = Convert.ToDateTime(LastDateTask.Value);

                if (TopicTask.SelectedValue == null)
                {
                    MessageBox.Show("Выберите тему задания");return;
                }

                var topicId = DataBase.webBookEntities.Topic.Where(x => x.TitleTopic == TopicTask.SelectedValue.ToString()).Select(id => id.IDTopic).FirstOrDefault();

                task.TopicTask = Convert.ToInt32(topicId);

                if (myResult == null)
                {
                    MessageBox.Show("Выберите файл"); return;
                }

                DataBase.webBookEntities.Task.AddOrUpdate(task);
                DataBase.webBookEntities.SaveChanges();

                MessageBox.Show("Задание добавлено");
            }
            else
            {
                if (!Checks.LetteLatinAndCyrillic(TitleTaskName.Text, "Поле наименование задания")) return;
                task.TitleTask = TitleTaskName.Text;

                if (LastDateTask.Value == null)
                {
                    MessageBox.Show("Выберите срок сдачи");
                }

                if (LastDateTask.Value <= DateTime.Now)
                {
                    MessageBox.Show("Выберите срок не раньше сегодня"); ; return;
                }

                if (LastDateTask.Value <= DateTime.Now)
                {
                    task.LastDateTask = Convert.ToDateTime(LastDateTask.Value);
                }

                if (TopicTask.SelectedValue == null)
                {
                    MessageBox.Show("Выберите тему задания"); return;
                }
                var topicId = DataBase.webBookEntities.Topic.Where(x => x.TitleTopic == TopicTask.SelectedValue.ToString()).Select(id => id.IDTopic).FirstOrDefault();

                task.TopicTask = Convert.ToInt32(topicId);

                DataBase.webBookEntities.Task.AddOrUpdate(task);
                DataBase.webBookEntities.SaveChanges();

                MessageBox.Show("Задание обновлено"); return;
            }

        }


        public void SaveFile(string filePath)
        {
            byte[] fileData = File.ReadAllBytes(filePath);
            byte[] compressedData = ByteConverter.CompressData(fileData);

            string fileExtension = Path.GetExtension(filePath);

            task.ExtensionTask = fileExtension;

            task.PracticalTask = compressedData;
        }

        private void OpenPrTask_Click(object sender, RoutedEventArgs e)
        {
            ofd.Filter = "All files (*.*)|*.*|rtf (*.rtf)|*.rtf|doc (*.doc)|*.doc";
            myResult = ofd.ShowDialog();
            if (myResult != null && myResult == true)
            {
                SaveFile(ofd.FileName);

            }
        }

        private void BtSave_Click(object sender, RoutedEventArgs e)
        {
            AddEdTopic();
        }
    }
}
