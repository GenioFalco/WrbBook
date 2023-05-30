using Microsoft.Win32;
using System;
using System.Data.Entity;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Threading;
using WebBook.ClassesApp;
using WebBook.EntityFramework;
using WebBook.WindowForm;
using Path = System.IO.Path;

namespace WebBook.PageWindow
{
    /// <summary>
    /// Логика взаимодействия для TopicsPage.xaml
    /// </summary>
    public partial class TopicsPage : Page
    {
        public string fileName { get; set; }

        public static Topic topic = null;
        public TopicsPage()
        {
            InitializeComponent();
            InitilizateVivod();
        }

     

        public void InitilizateVivod()
        {
            if (topic != null)
            {
                TitleTopic.Text = topic.TitleTopic;

                Description.Text = topic.DescriptionTopic;

                byte[] decompressedData = ByteConverter.DecompressData(topic.LectureTopic);

                MemoryStream stream = new MemoryStream(decompressedData);
                TextRange range = new TextRange(TopicsText.Document.ContentStart, TopicsText.Document.ContentEnd);
                range.Load(stream, DataFormats.Rtf);

            }
            else
            {
                topic = new Topic();
            }

        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            TopicsText.Foreground = new SolidColorBrush(Colors.Black);
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "RichText Files (*.rtf)|*.rtf|All files (*.*)|*.*";
            if (sfd.ShowDialog() == true)
            {
                TextRange doc = new TextRange(TopicsText.Document.ContentStart, TopicsText.Document.ContentEnd);
                using (FileStream fs = File.Create(sfd.FileName))
                {
                    if (Path.GetExtension(sfd.FileName).ToLower() == ".rtf")
                        doc.Save(fs, DataFormats.Rtf); 
                }
            }
         

        }

       

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MainMenu.frame.Navigate(new HomePage());
        }

   
   
    }
}
