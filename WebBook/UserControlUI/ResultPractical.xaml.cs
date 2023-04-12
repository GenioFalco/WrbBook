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
            dlg.FileName = answerPractical.PracricalAnswer; // Имя файла по умолчанию
            dlg.DefaultExt = ""; // Расширение файла по умолчанию
            dlg.Filter = "(*.rtf)|*.rtf;|(*.doc)|*.doc;|Все файлы (*.*)|*.*"; // Фильтровать файлы по расширению

            // Показать диалоговое окно сохранения файла
            Nullable<bool> result = dlg.ShowDialog();

            // Обработка результатов диалогового окна сохранения файла
            if (result == true)
            {
                // Сохраанение документа
                string filename = dlg.FileName;
            }

            string sourcePath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\PracticalTask\\AnswerPracticalTask\\", answerPractical.PracricalAnswer);

            string targetPath = Path.Combine(dlg.FileName);

            File.Copy(sourcePath, targetPath, true);

            if (targetPath == null)
            {
                return;
            }
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            answerPractical.GradeAnswer = Convert.ToInt32(GraedPrac.Text);

            answerPractical.PracricalAnswer = answerPractical.PracricalAnswer;

            answerPractical.IdUser = answerPractical.IdUser;

            answerPractical.TaskAnswer = answerPractical.TaskAnswer;
            


            DataBase.webBookEntities.AnswerPractical.AddOrUpdate(answerPractical);
            DataBase.webBookEntities.SaveChanges();
        }

        private void Border_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            try
            {
                List<AnswerPractical> answerPracticals = DataBase.webBookEntities.AnswerPractical.Where(p => p.IdAnswer == answerPractical.IdAnswer).ToList();
                DataBase.webBookEntities.AnswerPractical.Remove(answerPracticals[0]);
                DataBase.webBookEntities.SaveChanges();
                resultStudent.VivodResult();
            }
            catch
            {
                MessageBox.Show("Удалить нельзя");
            }
        }
    }
}
