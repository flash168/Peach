using Avalonia.Controls;
using Avalonia.Extensions.Media;

namespace PeachPlayer.Views;

public partial class VideoPlayView : Window
{
    public VideoPlayView()
    {
        InitializeComponent();
        VideoView = this.FindControl<PlayerView>("playerView");
    }


    PlayerView VideoView;
    public void Play(string url)
    {
        VideoView.Play(url);
    }
    public void Stop()
    {
        VideoView.Stop();
    }

}