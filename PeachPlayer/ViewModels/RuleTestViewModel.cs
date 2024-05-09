using System.Collections.ObjectModel;

namespace PeachPlayer.ViewModels;

public class RuleTestViewModel : ViewModelBase
{
    public ObservableCollection<string> ListItems { get; }

    public RuleTestViewModel()
    {
        ListItems = new ObservableCollection<string>();
        for (int i = 0; i < 10; i++)
        {
            ListItems.Add($"视频源{i}");
        }
    }


}
