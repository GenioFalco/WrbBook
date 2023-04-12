using System;
using System.Collections.Generic;
using System.Linq;
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
using WebBook.EntityFramework;
using Xceed.Wpf.Toolkit;
using WebBook.ClassesApp;
using MessageBox = System.Windows.MessageBox;
using Xceed.Wpf.AvalonDock.Themes;
using System.Net;
using WebBook.PageWindow;

namespace WebBook.WindowForm
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public static Frame frame;
        public Authorization()
        {
            InitializeComponent();
            frame = FLogIn;
            
            FLogIn.Navigate(new AuthorizationPage());
        }


      

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left)
            {
              DragMove();
            }
        }

     

       
    }
}
