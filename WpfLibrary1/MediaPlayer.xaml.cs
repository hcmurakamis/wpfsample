using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace WpfLibrary1
{
    /// <summary>
    /// MediaPlayer.xaml の相互作用ロジック
    /// https://qiita.com/jakucho0926/items/d2d71080bd1f5c39495a
    /// </summary>
    public partial class MediaPlayer : UserControl
    {
        public static readonly DependencyProperty Thumb2Property =
        DependencyProperty.Register(
            nameof(Thumb2),         // プロパティ名
            typeof(String), // バインドするデータの型
            typeof(MediaPlayer),  // 自分自身の型
            new PropertyMetadata(  // 初期値をPropertyMetadata経由でつっこむ
                new String(""))
        );

        public String Thumb2
        {
            get => (String)GetValue(Thumb2Property);
            set => SetValue(Thumb2Property, value);
        }





        public static readonly DependencyProperty ThumbProperty = 
            DependencyProperty.Register("Thumb", typeof(Uri), 
                typeof(MediaPlayer), new PropertyMetadata(null));
        public Uri Thumb
        {
            get { return (Uri)GetValue(ThumbProperty); }
            set { SetValue(ThumbProperty, value); }
        }

        #region Dependency Properties

        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(Uri), typeof(MediaPlayer), new PropertyMetadata(null));
        public Uri Source
        {
            get { return (Uri)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        #endregion

        #region Properties

        protected Storyboard TimelineStory
        {
            get { return (Storyboard)this.FindResource(nameof(TimelineStory)); }
        }

        protected bool IsPlaying
        {
            get { return this.Media.Clock != null && !this.IsPaused && !this.IsStopped; }
        }

        protected bool IsPaused
        {
            get { return this.Media.Clock != null && this.Media.Clock.IsPaused; }
        }

        protected bool IsStopped
        {
            get { return this.Media.Clock == null || this.Media.Clock.CurrentState.HasFlag(ClockState.Stopped); }
        }

        #endregion

        #region Constructors

        public MediaPlayer()
        {
            InitializeComponent();
            DataContext = this;
        }

        #endregion

        #region Container Events

        private void UserControl_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.MediaPlayPause:
                    this.Play();
                    break;

                case Key.Space:
                    if (this.IsPlaying)
                    {
                        this.Pause();
                    }
                    else
                    {
                        this.Play();
                    }
                    break;

                case Key.Left:
                    this.Rewind(TimeSpan.FromSeconds(10));
                    break;

                case Key.Right:
                    this.Forward(TimeSpan.FromSeconds(10));
                    break;
            }
        }

        #endregion

        #region Control Events

        private void Media_Loaded(object sender, RoutedEventArgs e)
        {
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }
            this.Play();
            this.Stop();
        }

        private void Media_MediaOpened(object sender, EventArgs e)
        {
            this.SeekSlider.Maximum = this.Media.NaturalDuration.TimeSpan.TotalMilliseconds;
        }

        private void Media_MediaEnded(object sender, RoutedEventArgs e)
        {
            this.Stop();
        }

        private void Media_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.IsPlaying)
            {
                this.Pause();
            }
            if (this.IsStopped)
            {
                this.Play();
            }
            if (this.IsPaused)
            {
                this.Play(TimeSpan.FromMilliseconds(this.SeekSlider.Value));
            }
        }

        private void MediaTimeline_CurrentTimeInvalidated(object sender, EventArgs e)
        {
            this.SeekSlider.Value = this.Media.Position.TotalMilliseconds;
        }

        private void SeekSlider_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.SeekSlider.IsMoveToPointEnabled = true;
            if (this.IsPlaying)
            {
                this.Pause();
            }
            if (this.IsStopped)
            {
                this.Play(TimeSpan.FromMilliseconds(this.SeekSlider.Value));
                this.Pause();
            }
        }

        private void SeekSlider_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (this.IsPaused)
            {
                this.Play(TimeSpan.FromMilliseconds(this.SeekSlider.Value));
            }
            this.SeekSlider.IsMoveToPointEnabled = false;
        }

        private void SeekSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.SeekSlider.IsMoveToPointEnabled)
            {
                this.TimelineStory.Seek(TimeSpan.FromMilliseconds(this.SeekSlider.Value));
            }
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            this.Play();
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Pause();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            this.Stop();
        }

        #endregion

        #region Methods

        public void Play()
        {
            if (this.IsStopped)
            {
                this.TimelineStory.Begin();
            }
            if (this.IsPaused)
            {
                this.TimelineStory.Resume();
            }
        }

        public void Play(TimeSpan position)
        {
            this.Seek(position);
            this.Play();
        }

        public void Seek(TimeSpan timeSpan)
        {
            var value = timeSpan;
            if (value.TotalMilliseconds < 0)
            {
                value = new TimeSpan();
            }
            this.TimelineStory.Seek(value);
        }

        public void Rewind(TimeSpan timeSpan)
        {
            this.Seek(TimeSpan.FromMilliseconds(this.SeekSlider.Value).Subtract(timeSpan));
        }

        public void Forward(TimeSpan timeSpan)
        {
            this.Seek(TimeSpan.FromMilliseconds(this.SeekSlider.Value).Add(timeSpan));
        }

        public void Pause()
        {
            this.TimelineStory.Pause();
        }

        public void Stop()
        {
            this.TimelineStory.Stop();
        }

        #endregion

    }
}