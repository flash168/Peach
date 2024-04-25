using System.Collections.ObjectModel;

namespace PeachPlayer.ViewModels;

public class PlayHistoryViewModel : ViewModelBase
{
    public ObservableCollection<string> ListItems { get; }

    public PlayHistoryViewModel()
    {
        ListItems = new ObservableCollection<string>();
        for (int i = 0; i < 10; i++)
        {
            ListItems.Add($"视频源{i}");
        }
    }
}
