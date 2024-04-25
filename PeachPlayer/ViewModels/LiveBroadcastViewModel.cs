using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PeachPlayer.ViewModels
{
    public class LiveBroadcastViewModel : ViewModelBase
    {
        public ObservableCollection<string> ListItems { get; }

        public LiveBroadcastViewModel()
        {
            ListItems = new ObservableCollection<string>();
            for (int i = 0; i < 10; i++)
            {
                ListItems.Add($"视频源{i}");
            }
        }

    }
}
