using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Windows;
using WebBook.ClassesApp;
using WebBook.EntityFramework;
using MessageBox = System.Windows.MessageBox;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using Page = System.Windows.Controls.Page;


namespace WebBook.PageWindow
{
    /// <summary>
    /// Логика взаимодействия для AddEditTopicPage.xaml
    /// </summary>
    public partial class AddEditTopicPage : Page
    {

        public static Topic topic = new Topic();

        OpenFileDialog ofd = new OpenFileDialog();

        bool? myResult2;

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

                if (myResult2 == null || myResult2 == false)
                {
                    MessageBox.Show("Выберите документ"); return;
                }

                DataBase.webBookEntities.Topic.AddOrUpdate(topic);
                DataBase.webBookEntities.SaveChanges();

                MessageBox.Show("Тема добавлена");

                Title1.Text = "";
                Description.Text = "";
                myResult2 = null;
                OpenDocument.Content = "Выберите документ";
                topic = new Topic();

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


        public void SaveFile(string filePath)
        {
            byte[] fileData = File.ReadAllBytes(filePath);
            byte[] compressedData = ByteConverter.CompressData(fileData);
            topic.LectureTopic = compressedData;
        }

        private void OpenDocument_Click(object sender, RoutedEventArgs e)
        {
            ofd.Filter = "Select a rtf (*.rtf)|*.rtf|All files (*.*)|*.*";

            myResult2 = ofd.ShowDialog();
            if (myResult2 != null && myResult2 == true)
            {
                SaveFile(ofd.FileName);
            }
            OpenDocument.Content = ofd.SafeFileName;
        }
    }
}
