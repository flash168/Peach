using PeachPlayer.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace PeachPlayer.uc
{
    /// <summary>
    /// VideoItemUc.xaml 的交互逻辑
    /// </summary>
    public partial class VideoItemUc : UserControl
    {
        public VideoItemUc()
        {
            InitializeComponent();
        }


        public VideoModel ItemData
        {
            get { return (VideoModel)GetValue(ItemDataProperty); }
            set { SetValue(ItemDataProperty, value); }
        }
        public static readonly DependencyProperty ItemDataProperty = DependencyProperty.Register("ItemData", typeof(VideoModel), typeof(VideoItemUc), new PropertyMetadata(null, OnItemDataChanged));

        private static void OnItemDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (VideoItemUc)d;
            var item = (VideoModel)e.NewValue;
            if (item != null)
            {
                control.title.Content = item.Vod_name;
                control.score.Content = item.Vod_douban_score;
                control.mark.Content = item.Vod_remarks;
                control.image.Source = new BitmapImage(new System.Uri(item.Vod_pic, System.UriKind.RelativeOrAbsolute));
            }
        }

    }
}
