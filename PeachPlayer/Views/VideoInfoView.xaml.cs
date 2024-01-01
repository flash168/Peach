using PeachPlayer.ViewModel;
using PeachPlayer.Windows;
using System.Windows;
using System.Windows.Controls;

namespace PeachPlayer.View
{
    /// <summary>
    /// VideoInfoView.xaml 的交互逻辑
    /// </summary>
    public partial class VideoInfoView : UserControl
    {
        VideoInfoViewVM vm;
        public VideoInfoView(VideoModel video)
        {
            InitializeComponent();
            this.DataContext = vm = new VideoInfoViewVM(video);

        }


        private void Btn_ItemPlay(object sender, RoutedEventArgs e)
        {
            var flag = (sender as Button).Tag as PlayerData;
            var info = (sender as Button).DataContext as JiShuInfo;
            if (flag != null && info != null)
            {
                new PlayerWin(flag, info.Name).Show();
            }
        }




        private void Btn_ItemDown(object sender, RoutedEventArgs e)
        {
            var info = (sender as Button).DataContext as JiShuInfo;
            if (info != null)
            {
                //M3u8Services.Instance.AddM3u8DownTask(vm.Move, info.Purl, info.Name);
            }
        }

    }
}
