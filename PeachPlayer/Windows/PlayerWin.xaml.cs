using Microsoft.Web.WebView2.Core;
using PeachPlayer.Models;
using PeachPlayer.ViewModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using Vlc.DotNet.Core;
using Vlc.DotNet.Wpf;

namespace PeachPlayer.Windows
{
    /// <summary>
    /// PlayerWin.xaml 的交互逻辑
    /// </summary>
    public partial class PlayerWin : Window
    {
        PlayerWinVM vm;
        public PlayerWin(PlayerData data, string ji)
        {
            InitializeComponent();
            InitializeAsync();
            webView.CoreWebView2InitializationCompleted += WebView_CoreWebView2InitializationCompleted;

            InitVLC();
            this.DataContext = vm = new PlayerWinVM(data, ji, vlcVideo, GoUrl);
        }


        private void WebView_CoreWebView2InitializationCompleted(object? sender, Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs e)
        {
            webView.CoreWebView2.WebResourceResponseReceived += CoreWebView2_WebResourceResponseReceivedAsync;
        }

        private void CoreWebView2_WebResourceResponseReceivedAsync(object? sender, CoreWebView2WebResourceResponseReceivedEventArgs e)
        {
            var url = e.Request.Uri;
            Debug.WriteLine(url);
            if (url.Contains(".m3u8"))
            {
                vlcVideo?.SourceProvider?.MediaPlayer?.Play(url);
                // OnResponseReceived?.Invoke(url);
            }
        }

        async void InitializeAsync()
        {
            var webView2Environment = await CoreWebView2Environment.CreateAsync();
            await webView.EnsureCoreWebView2Async(webView2Environment);
            Debug.WriteLine("----EnsureCoreWebView2Async end");
            if (!string.IsNullOrEmpty(purl))
            {
                GoUrl(purl);
                purl = "";
            }
        }
        //懒得处理异步，初始化好后才能跳转网页
        static string purl = "";
        public void GoUrl(string url)
        {
            if (webView != null && webView.CoreWebView2 != null)
            {
                webView.CoreWebView2.Navigate(url);
            }
            else
                purl = url;
        }


        VlcControl vlcVideo;
        public void InitVLC()
        {
            if (this.vlcVideo?.SourceProvider?.MediaPlayer != null)
            {
                this.vlcVideo.SourceProvider.MediaPlayer.PositionChanged -= MediaPlayer_PositionChanged;
                this.vlcVideo.SourceProvider.MediaPlayer.LengthChanged -= MediaPlayer_LengthChanged;
            }
            this.vlcVideo = new VlcControl();
            contentCtrl.Content = this.vlcVideo;
            var libDirectory = new DirectoryInfo(Directory.GetCurrentDirectory() + "\\LibVlc");
            this.vlcVideo.SourceProvider.CreatePlayer(libDirectory);//创建视频播放器
            this.vlcVideo.SourceProvider.MediaPlayer.PositionChanged += MediaPlayer_PositionChanged;//视频的定位移动事件
            this.vlcVideo.SourceProvider.MediaPlayer.LengthChanged += MediaPlayer_LengthChanged;//播放视频源的视频长度

        }

        private void MediaPlayer_LengthChanged(object? sender, VlcMediaPlayerLengthChangedEventArgs e)
        {
        }

        private void MediaPlayer_PositionChanged(object? sender, VlcMediaPlayerPositionChangedEventArgs e)
        {
        }



    }
}
