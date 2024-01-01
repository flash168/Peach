using PeachPlayer.ViewModel;
using System.Windows.Controls;

namespace PeachPlayer.View
{
    /// <summary>
    /// VideoInfoView.xaml 的交互逻辑
    /// </summary>
    public partial class PlayerView : UserControl
    {
        PlayerViewVM vm;
        public PlayerView(VideoModel video)
        {
            InitializeComponent();
            this.DataContext = vm = new PlayerViewVM(video);
            contentCtrl.Content = vm.InitVLC();
        }



    }
}
