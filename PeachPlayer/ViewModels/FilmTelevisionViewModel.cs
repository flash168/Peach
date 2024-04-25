using System.Collections.ObjectModel;

namespace PeachPlayer.ViewModels;

public class FilmTelevisionViewModel : ViewModelBase
{
    public ObservableCollection<string> ListItems { get; }

    public FilmTelevisionViewModel()
    {
        ListItems = new ObservableCollection<string>();
        for (int i = 0; i < 10; i++)
        {
            ListItems.Add($"视频源{i}");
        }
    }


}
