using OpenCvSharp;
using OpenCvSharp.WpfExtensions;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace WpfApp2
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            VideoCapture capture = new VideoCapture("video/output2.m3u8");
            //VideoCapture capture = new VideoCapture("video/test1.mp4");
            var img = new Mat();

            // 101番目のフレームから再生 MAXFRAME が100の場合は99が最大
            //capture.PosFrames=101;

            // フレームカウンタ
            int cnt = 1;
            while (capture.Read(img))
            {

                Title = capture.Fps.ToString() + "Fps,MAXFrame" + capture.FrameCount.ToString() + "/Frame" + cnt.ToString();
                cnt++;

                var x = BitmapSourceConverter.ToBitmapSource(img);

                // 開始5秒後を表示
                // capture.PosMsec=5000;

                // 保存 
                //using (Stream stream = new FileStream("c:\\test" + cnt + ".jpg", FileMode.Create))
                //{
                //    PngBitmapEncoder encoder = new PngBitmapEncoder();
                //    encoder.Frames.Add(BitmapFrame.Create(x));
                //    encoder.Save(stream);
                //}
                image.Source = x;
                await Task.Delay(10);
            }
            image.Source = null;
        }
    }
}
