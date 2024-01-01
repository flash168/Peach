using PeachPlayer.Models;
using PeachPlayer.View;
using System.Collections.Generic;
using System;
using System.Configuration;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Linq;

namespace PeachPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        MainWindowVM vm;
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = vm = new MainWindowVM();
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            string url = ConfigurationManager.AppSettings["ConfigUrl"];
            if (string.IsNullOrEmpty(url.Trim()))
            {
                MessageBox.Show("资源地址没有配置。");
                return;
            }
            message.Text = "加载配置文件中···";
            var isok = await vm.GetAPIConfig(url);
            message.Text = isok ? "配置文件加载成功！" : "配置文件加载失败！";
            DownloadService.Instance.InitDown(10);
        }

        private async void SitemChanged(object sender, SelectionChangedEventArgs e)
        {
            message.Text = "切换源中···";
            var sel = e.AddedItems[0] as SitesItem;
            await vm.SetSite(sel?.Key);
            message.Text = $"加载【{sel?.Key}】源中···";
            await vm.GetHome();
            message.Text = $"加载【{sel?.Key}】完成！";
        }

        private void Btn_NavigMenu(object sender, RoutedEventArgs e)
        {
            var tag = (sender as Button)?.Tag?.ToString();
            var pa = new ClassifyListView(tag);
            Frame_page.Navigate(pa);

        }

    }
}
