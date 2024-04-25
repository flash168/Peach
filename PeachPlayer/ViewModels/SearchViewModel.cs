using System.Collections.ObjectModel;

namespace PeachPlayer.ViewModels;

public class SearchViewModel : ViewModelBase
{
    public ObservableCollection<string> ListItems { get; }
    public SearchViewModel()
    {
        ListItems = new ObservableCollection<string>();
        for (int i = 0; i < 10; i++)
        {
            ListItems.Add($"视频源{i}");
        }
    }
    public SearchViewModel(string parameter):base()
    {
       
    }


   
}
