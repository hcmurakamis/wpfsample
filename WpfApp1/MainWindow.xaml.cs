﻿using System.Collections.ObjectModel;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private ObservableCollection<string> FileList;


        public MainWindow()
        {
            InitializeComponent();
            //FileList = new ObservableCollection<string>();
            //GarbageBucket.ItemsSource = FileList;
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
            Image image = e.Source as Image;
            DataObject data = new DataObject(typeof(ImageSource), image.Source);
            DragDrop.DoDragDrop(image, data, DragDropEffects.All);
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
                GarbageBucket.Items.Add(e.Data.GetData(DataFormats.Bitmap));
            }
        }
    }
}