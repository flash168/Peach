using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace PeachPlayer.uc
{
    internal class ImageAsync : Image
    {
        public string ImageUrl
        {
            get { return (string)GetValue(ImageUrlProperty); }
            set { SetValue(ImageUrlProperty, value); }
        }
        public static readonly DependencyProperty ImageUrlProperty =
            DependencyProperty.Register("ImageUrl", typeof(string), typeof(ImageAsync), new PropertyMetadata("", UrlChengedAllBackd));

        private static void UrlChengedAllBackd(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ImageAsync).LoadAsync(e.NewValue?.ToString());
        }

        private CancellationTokenSource _cts;

        public async void LoadAsync(string url)
        {
            if (_cts != null)
            {
                _cts.Cancel();
                _cts = null;
            }

            _cts = new CancellationTokenSource();
            CancellationToken token = _cts.Token;

            try
            {
                // 使用 HttpClient 异步加载图片
                using (HttpClient client = new HttpClient())
                {
                    byte[] data = await client.GetByteArrayAsync(url, token);
                    BitmapImage image = new BitmapImage();

                    // 将图片数据流转换成 BitmapImage
                    using (MemoryStream stream = new MemoryStream(data))
                    {
                        image.BeginInit();
                        image.CacheOption = BitmapCacheOption.OnLoad;
                        image.StreamSource = stream;
                        image.EndInit();
                    }

                    // 在 UI 线程上显示图片
                    Dispatcher.Invoke(new Action(() => { Source = image; }));
                }
            }
            catch (TaskCanceledException ex)
            {
                // 处理任务取消异常
            }
            catch (Exception ex)
            {
                // 处理其他异常
            }
        }

    }
}
