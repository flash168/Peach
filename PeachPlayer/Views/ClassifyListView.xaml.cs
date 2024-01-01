using PeachPlayer.Models;
using PeachPlayer.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace PeachPlayer.View
{
    /// <summary>
    /// ClassifyListView.xaml 的交互逻辑
    /// </summary>
    public partial class ClassifyListView : Page
    {
        ClassifyListViewVM vm;

        public ClassifyListView(string tag)
        {
            InitializeComponent();
            vm = new ClassifyListViewVM(tag);
            this.DataContext = vm;
        }

        private void list_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {

        }

        private void Btn_VideoItem(object sender, RoutedEventArgs e)
        {
            var item = (sender as FrameworkElement).DataContext as VideoModel;
            if (item != null)
            {
                NavigationService.GetNavigationService(this).Navigate(new VideoInfoView(item));
            }
        }
    }
}
