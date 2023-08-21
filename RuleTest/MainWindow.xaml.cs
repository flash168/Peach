using Peach.Application.VodInfos;
using Peach.Drpy;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RuleTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            testService.WriterLog += Service_WriterLog;
        }

        private void Service_WriterLog(string obj)
        {
            this.Dispatcher.Invoke(() =>
            {
                txt_Meg1.Text += "\n" + obj;
                txt_Meg1.ScrollToEnd();
            });
        }

        RuleTestService testService = new RuleTestService();

        private void AddMeg(string mes)
        {
            txt_Meg.Text += $"\n---------------------------------【{DateTime.Now}】---------------------------------";
            txt_Meg.Text += "\n" + mes;
            txt_Meg.ScrollToEnd();
        }
        private void Btn_Clear(object sender, RoutedEventArgs e)
        {
            txt_Meg.Text = "";
        }
        private void Btn_Clear1(object sender, RoutedEventArgs e)
        {
            txt_Meg1.Text = "";
        }
        private void Btn_Copy1(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(txt_Meg1.SelectedText);
        }
        private void Bnt_Init(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_rule.Text.Trim()))
                AddMeg("Rule规则不能为空！");
            var isok = testService.InitSite(txt_rule.Text.Trim());
            AddMeg($"初始化DRPY2 {(isok ? "成功" : "失败")}。");
        }

        private async void Bnt_Home(object sender, RoutedEventArgs e)
        {
            var req = await testService.HomeAsync(string.Empty);
            AddMeg(req);
        }

        private async void Bnt_ClassA(object sender, RoutedEventArgs e)
        {
            var req = await testService.ClassifyAsync(string.Empty, txt_tid.Text.Trim(), "", "", "");
            AddMeg(req);
        }

        private async void Bnt_Detail(object sender, RoutedEventArgs e)
        {
            var req = await testService.DetailsAsync(string.Empty, txt_vid.Text.Trim());
            AddMeg(req);
        }

        private async void Bnt_Search(object sender, RoutedEventArgs e)
        {
            var req = await testService.SearchAsync(string.Empty, txt_qe.Text.Trim());
            AddMeg(req);
        }

        private async void Bnt_Play(object sender, RoutedEventArgs e)
        {
            var req = await testService.SniffingAsync(txt_line.Text.Trim(), txt_pl.Text.Trim());
            AddMeg(req);
        }
    }
}
