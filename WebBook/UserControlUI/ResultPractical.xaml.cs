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
using WebBook.PageWindow;
using Path = System.IO.Path;

namespace WebBook.UserControlUI
{
    /// <summary>
    /// Логика взаимодействия для ResultPractical.xaml
    /// </summary>
    public partial class ResultPractical : UserControl
    {
        public ResultStudent resultStudent;
        AnswerPractical answerPractical = null;
        public ResultPractical(AnswerPractical answerPractical)
        {
            InitializeComponent();
            this.answerPractical = answerPractical;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();

            var taskId = DataBase.webBookEntities.Task.Where(x => x.IDTask == answerPractical.IdTask).Select(id => id.TitleTask).FirstOrDefault();

            var userId = DataBase.webBookEntities.User.Where(x => x.IDUser == answerPractical.IdUser).Select(id => id.SurnameUser + id.GroupUser).FirstOrDefault();

            dlg.FileName = taskId + " " + userId; // Имя файла по умолчанию
            dlg.DefaultExt = answerPractical.ExtensionAnsw; // Расширение файла по умолчанию

            bool? result = dlg.ShowDialog();

           
            if (result == true)
            {
                string filePath = dlg.FileName;

                byte[] fileContent = ByteConverter.DecompressData(answerPractical.PracricalAnswer);
                using (var fs = new FileStream(filePath, FileMode.Create))

                {
                    fs.Write(fileContent, 0, fileContent.Length);
                }

            }


        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!Checks.Number(GraedPrac.Text, "Поле оценка")) return;

            if (Convert.ToInt32(GraedPrac.Text) < 2 || Convert.ToInt32(GraedPrac.Text) > 5)
            {
                MessageBox.Show("Введите оценку от 2 до 5");return;
            }

            answerPractical.GradeAnswer = Convert.ToInt32(GraedPrac.Text);

            answerPractical.PracricalAnswer = answerPractical.PracricalAnswer;

            answerPractical.IdUser = answerPractical.IdUser;

            answerPractical.IdTask = answerPractical.IdTask;
            

            DataBase.webBookEntities.AnswerPractical.AddOrUpdate(answerPractical);
            DataBase.webBookEntities.SaveChanges();
        }

        private void Border_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
 

            if (MessageBox.Show("Вы точно хотите удалить?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    DataBase.webBookEntities.AnswerPractical.Remove(answerPractical);
                    DataBase.webBookEntities.SaveChanges();
                    resultStudent.VivodResult();
                }
                catch
                {
                    MessageBox.Show("Нельзя удалить");
                }
            }
            else
            {
                return;
            }
        }
    }
}
