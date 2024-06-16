using LibVLCSharp.Shared;
using System.Collections.ObjectModel;
using System.IO;
using System.Numerics;
using System.Reflection.Emit;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfLibrary1;
using static System.Net.Mime.MediaTypeNames;

namespace WpfApp1
{
    public class TreeNode
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public ObservableCollection<TreeNode> Children { get; set; }
    }

    public class VideoInfo
    {
        public Uri Src { get; set; }
        public Uri Thu { get; set; }

    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Media media1;

        // HLS Create by ffmpeg
        // ffmpeg -i s2.mp4 -codec: copy -start_number 0 -hls_time 10 -hls_list_size 0 -f hls output2.m3u8
        // VIdeo Downloaded by https://github.com/intel-iot-devkit/sample-videos/tree/master

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            vlcControl.MediaPlayer = new LibVLCSharp.Shared.MediaPlayer(new LibVLC());
            var mediaOptions = new string[]
            {
                ":network-caching=1000",
                ":clock-jitter=0",
                ":clock-synchro=0"
            };

            string FileName = "video/output2.m3u8";
            FileInfo MediaSource1 = new FileInfo(System.IO.Path.Combine(new FileInfo(System.Reflection.Assembly.GetEntryAssembly().Location).DirectoryName, FileName));
            media1 = new Media(new LibVLC(), MediaSource1.FullName);
            vlcControl.MediaPlayer.Play(media1);

        }

        public MainWindow()
        {
            InitializeComponent();

            // データモデルを作成
            var rootItems = new ObservableCollection<TreeNode>
            {
                new TreeNode
                {
                    Name = "Root 1",
                    Icon = "Icons/folder.png",
                    Children = new ObservableCollection<TreeNode>
                    {
                        new TreeNode { Name = "Child 1.1", Icon = "Icons/file.png" },
                        new TreeNode { Name = "Child 1.2", Icon = "Icons/file.png" }
                    }
                },
                new TreeNode
                {
                    Name = "Root 2",
                    Icon = "Icons/folder.png",
                    Children = new ObservableCollection<TreeNode>
                    {
                        new TreeNode { Name = "Child 2.1", Icon = "Icons/file.png" }
                    }
                }
            };

            // TreeViewにデータをバインド
            TreeViewControl.ItemsSource = rootItems;

            var videoItems = new ObservableCollection<MediaPlayerViewModel>
            {
                new MediaPlayerViewModel{Source2=new Uri("test1.mp4", UriKind.Relative), Thumb=new Uri("Icons/file.png", UriKind.Relative) },
                new MediaPlayerViewModel{Source2=new Uri("test2.mp4", UriKind.Relative), Thumb=new Uri("Icons/folder.png", UriKind.Relative) },

            };


            this.DataContext = videoItems;

        }

        public void vlcclick(object sender, EventArgs e)
        {
            vlcControl.MediaPlayer.Play(media1);
        }

        private void FileListBox_Drop(object sender, DragEventArgs e)
        {
            //if (e.Data.GetDataPresent(DataFormats.FileDrop))
            //{
            //    var fileNames = (string[])e.Data.GetData(DataFormats.FileDrop);
            //    foreach (var name in fileNames)
            //    {
            //        FileList.Add(name);
            //    }
            //}
        }

        private void FileListBox_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.All;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
            e.Handled = true;
        }

        private void img2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //Image image = e.Source as Image;
            //DataObject data = new DataObject(typeof(ImageSource), image.Source);
            //DragDrop.DoDragDrop(image, data, DragDropEffects.All);
        }

        private void SourceImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                // データオブジェクトの作成
                DataObject data = new DataObject(DataFormats.Bitmap, SourceImage.Source);
                // ドラッグの開始
                DragDrop.DoDragDrop(SourceImage, data, DragDropEffects.Copy);
            }
        }

        private void TargetImage_DragOver(object sender, DragEventArgs e)
        {
            Console.WriteLine("TargetImage_DragOver");
            Console.WriteLine(e);

            // ドラッグされているデータが画像かどうかをチェック
            //if (e.Data.GetDataPresent(DataFormats.FileDrop))
            //{
            //    e.Effects = DragDropEffects.Copy;
            //}
            //else
            //{
            //    e.Effects = DragDropEffects.None;
            //}
            e.Handled = true;
        }

        private void TargetImage_Drop(object sender, DragEventArgs e)
        {
            Console.WriteLine("TargetImage_Drop");
            Console.WriteLine(e.Data.GetData(DataFormats.FileDrop)); 

            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                // ドロップされた画像を取得
                // TargetImageのSourceプロパティに設定
                var items = ImageList.SelectedItems;

                //foreach (var item in items)
                //{
                //    // object to Image
                //    Image image = (Image)item;

                //    //GarbageBucket.Items.Add(image.Source);
                //}


            }
        }

        private void MediaPlayer_SourceUpdated(object sender, DataTransferEventArgs e)
        {

        }
    }
}