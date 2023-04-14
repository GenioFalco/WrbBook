using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Threading;
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
        
        private bool userIsDraggingSlider = false;
        public string fileName { get; set; }

        

        public static Topic topic = null;
        public TopicsPage()
        {
            InitializeComponent();

            www.Pause();
            InitilizateVivod();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
            
        }

        public void InitilizateVivod()
        {
            if (topic != null)
            {
                TitleTopic.Text = topic.TitleTopic;


                if (topic.UrlVideoTopic != null)
                {
                    www.Source = new Uri(topic.UrlVideoTopic, UriKind.Relative);
                }
                else
                {
                    www.Source = null;
                }

                Description.Text = topic.DescriptionTopic;

                TextRange range;
                FileStream fStream;

                fileName = topic.LectureTopic;

                if (File.Exists(fileName))
                {
                    range = new TextRange(TopicsText.Document.ContentStart, TopicsText.Document.ContentEnd);
                    fStream = new FileStream(fileName, FileMode.OpenOrCreate);
                    range.Load(fStream, DataFormats.Rtf);
                    fStream.Close();
                }

            }
            else
            {
                topic = new Topic();
            }

        }

    
        

        void timer_Tick(object sender, EventArgs e)
        {
            if ((www.Source != null) && (www.NaturalDuration.HasTimeSpan) && (!userIsDraggingSlider))
            {
                pbVolume.Minimum = 0;
                pbVolume.Maximum = www.NaturalDuration.TimeSpan.TotalSeconds;
                pbVolume.Value = www.Position.TotalSeconds;

                sliProgress.Minimum = 0;
                sliProgress.Maximum = www.NaturalDuration.TimeSpan.TotalSeconds;
                sliProgress.Value = www.Position.TotalSeconds;
            }

        }

        private void sliProgress_DragStarted(object sender, DragStartedEventArgs e)
        {
            userIsDraggingSlider = true;
        }

        private void sliProgress_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            userIsDraggingSlider = false;
            www.Position = TimeSpan.FromSeconds(pbVolume.Value);
            www.Position = TimeSpan.FromSeconds(sliProgress.Value);
        }

        private void sliProgress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (www.Source != null)
            {
                if (www.NaturalDuration.HasTimeSpan)
                    time.Text = String.Format("{0} / {1}", www.Position.ToString(@"mm\:ss"), www.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
            }
            else
                time.Text = "No file selected...";

 
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Save.Foreground = new SolidColorBrush(Colors.Black);
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

   
    

        private void RadioButton_Unchecked(object sender, RoutedEventArgs e)
        {
            www.Pause();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            www.Play();
        }
    }
}
