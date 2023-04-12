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
using Xceed.Wpf.Toolkit;

namespace WebBook
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void PassMask(object sender, RoutedEventArgs e)
        {
            PasswordMask.Visibility = Visibility.Visible;
            Password.Visibility = Visibility.Hidden;
            PasswordMask.Text = Password.Password;
        }

        private void PassUnMask(object sender, RoutedEventArgs e)
        {
            PasswordMask.Visibility = Visibility.Hidden;
            Password.Visibility = Visibility.Visible;
            Password.Password = PasswordMask.Text;
        }
    }
}
