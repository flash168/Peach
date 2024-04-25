using System.Collections.ObjectModel;

namespace PeachPlayer.ViewModels;

public class NetdiskViewModel : ViewModelBase
{
    public ObservableCollection<string> ListItems { get; }

    public NetdiskViewModel()
    {
        ListItems = new ObservableCollection<string>();
        for (int i = 0; i < 10; i++)
        {
            ListItems.Add($"视频源{i}");
        }
    }
}
