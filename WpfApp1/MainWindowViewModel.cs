using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using WpfLibrary1;

namespace WpfApp1
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            VideoItems = new ObservableCollection<MediaPlayerViewModel>
            {
                new MediaPlayerViewModel{Source2=new Uri("test1.mp4", UriKind.Relative), Thumb=new Uri("Icons/file.png", UriKind.Relative) },
                new MediaPlayerViewModel{Source2=new Uri("test2.mp4", UriKind.Relative), Thumb=new Uri("Icons/folder.png", UriKind.Relative) },

            };


            // データモデルを作成
            TreeItems = new ObservableCollection<TreeNode>
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
        }

        private ObservableCollection<MediaPlayerViewModel> _videoItems;

        public ObservableCollection<MediaPlayerViewModel> VideoItems
        {

            get { return _videoItems; }
            set
            {
                if (_videoItems != value)
                {
                    _videoItems = value;
                    OnPropertyChanged(nameof(ObservableCollection<MediaPlayerViewModel>));
                }
            }
        }

        public ObservableCollection<TreeNode> TreeItems
        {

            get; set;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
