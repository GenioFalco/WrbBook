using System;
using System.Collections.Generic;
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

namespace WebBook.UserControlUI
{
    /// <summary>
    /// Логика взаимодействия для ResultTestList.xaml
    /// </summary>
    public partial class ResultTestList : UserControl
    {
        public ResultStudent resultStudent;
        Results results = null;
        public ResultTestList(Results results)
        {
            InitializeComponent();
            this.results = results;
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                List<Results> result = DataBase.webBookEntities.Results.Where(p => p.IdResult == results.IdResult).ToList();
                DataBase.webBookEntities.Results.Remove(result[0]);
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
