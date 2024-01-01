using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using System;
using System.Diagnostics;

namespace PeachPlayer.uc
{

    public class SniffingService
    {
        private static SniffingWebView2 instance = null;
        public static SniffingWebView2 Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SniffingWebView2();
                }

                return instance;
            }
        }
    }


    public class SniffingWebView2
    {
        WebView2 webView;
        public event Action<string> OnResponseReceived;
        public SniffingWebView2() { }

        public void Init()
        {
            webView = new WebView2();
            InitializeAsync();
            webView.CoreWebView2InitializationCompleted += WebView_CoreWebView2InitializationCompleted;
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
                OnResponseReceived?.Invoke(url);
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

    }
}
