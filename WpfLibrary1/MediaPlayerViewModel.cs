using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace WpfLibrary1
{
    public class MediaPlayerViewModel : INotifyPropertyChanged
    {
        private Uri? _thumb;

        public Uri Thumb
        {
            get => _thumb;
            set
            {
                if (_thumb != value)
                {
                    _thumb = value;
                    OnPropertyChanged(nameof(Thumb));
                }
            }
        }


        private Uri? _source;

        public Uri Source
        {
            get => _source;
            set
            {
                if (_source != value)
                {
                    _source = value;
                    OnPropertyChanged(nameof(Source));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
