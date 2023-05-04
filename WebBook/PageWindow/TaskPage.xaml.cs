using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Remoting.Contexts;
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
using WebBook.UserControlUI;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using MessageBox = System.Windows.MessageBox;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using Path = System.IO.Path;


namespace WebBook.PageWindow
{
    /// <summary>
    /// Логика взаимодействия для TaskPage.xaml
    /// </summary>
    public partial class TaskPage : Page
    {
        public static EntityFramework.Task task = null;

        public static User user = null;

        public static AnswerPractical answerPractical = null;

        OpenFileDialog ofd = new OpenFileDialog();
        bool? myResult;

        public byte[] compressedData;

        string fileExtension;

        public TaskPage()
        {
            InitializeComponent();
            InitilizateVivod();
        }

        public void InitilizateVivod()
        {
            if (task != null)
            {
                TitleTask.Text = task.TitleTask;

                LastDate.Text = "Последний срок сдачи:" + task.LastDateTask.ToString();

                var graedId = DataBase.webBookEntities.AnswerPractical.Where(id => id.IdTask == task.IDTask && id.IdUser == user.IDUser).Select(x => x.GradeAnswer).FirstOrDefault();
                Grade.Text = graedId.ToString();

               


                TimeSpan TimeRemaining = task.LastDateTask - DateTime.Now;
                LeftDate.Text = "Оставшееся время:" + TimeRemaining.Days + " : " + TimeRemaining.Hours + " : " + TimeRemaining.Minutes;

                if (DateTime.Now >= task.LastDateTask)
                {
                    LeftDate.Foreground = new SolidColorBrush(Colors.Red);
                }

            }

            else
            {
                task = new EntityFramework.Task();
            }
        }



     

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = task.TitleTask; // Имя файла по умолчанию
            dlg.DefaultExt = task.ExtensionTask; // Расширение файла по умолчанию
           
            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                
                string filePath = dlg.FileName;
              
                byte[] fileContent = ByteConverter.DecompressData(task.PracticalTask);
                using (var fs = new FileStream(filePath, FileMode.Create))

                {
                    fs.Write(fileContent, 0, fileContent.Length);
                }
                
            }

        }


        public void SaveFile(string filePath)
        {
            byte[] fileData = File.ReadAllBytes(filePath);

            compressedData = ByteConverter.CompressData(fileData);

            fileExtension = Path.GetExtension(filePath);

           

            
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ofd.Filter = "Select a rtf (*.rtf)|*.rtf|doc (*.doc)|*.doc|All files (*.*)|*.*";
            myResult = ofd.ShowDialog();
            if (myResult != null && myResult == true)
            {
                SaveFile(ofd.FileName);

            }


        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            answerPractical = new AnswerPractical();

            answerPractical.IdUser = user.IDUser;

            answerPractical.IdTask = task.IDTask;

            answerPractical.PracricalAnswer = compressedData;

            answerPractical.DeliveryDate = DateTime.Now;

            answerPractical.ExtensionAnsw = fileExtension;
            DataBase.webBookEntities.AnswerPractical.AddOrUpdate(answerPractical);
            DataBase.webBookEntities.SaveChanges();
        }
    }
}
            

       
