using Avalonia.Controls;
using Peach.Application.Interfaces;
using Peach.Model.Models;
using ReactiveUI;
using Splat;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;

namespace PeachPlayer.ViewModels
{
    public class ClassifyListViewModel : ViewModelBase
    {
        private IVodInfoService vod;
        private readonly ClassModel Class;
        private readonly List<FilterModel> filters;
        private int PgIndex = 1;

        public string Type_Name => Class.Type_Name;

        private VideoViewModel selectedVod;
        public VideoViewModel SelectedVod
        {
            get { return selectedVod; }
            set { this.RaiseAndSetIfChanged(ref selectedVod, value); }
        }
        public ObservableCollection<VideoViewModel> Videos { get; } = new();

        public ReactiveCommand<object, Unit> ScrollCommand { get; }

        public ClassifyListViewModel(ClassModel _class, List<FilterModel> _filters = null)
        {
            ScrollCommand = ReactiveCommand.Create<object>(Scroll);
            vod = Locator.Current.GetService<IVodInfoService>();
            Class = _class;
            filters = _filters;
        }

        public async void LoadVodList()
        {
            Videos.Clear();
            var data = await vod?.ClassifyAsync(Class.Type_Id, PgIndex, "", "");
            if (data?.List?.Count > 0)
            {
                var vods = data.List.Select(x => new VideoViewModel(x));
                foreach (var v in vods)
                {
                    Videos.Add(v);
                }
            }
        }

        public void Scroll(object o)
        {
            var t = (ScrollViewer)o;
            if (t.Offset.Length >= t.Extent.Height - (t.DesiredSize.Height * 1.2))
            {
                NextPage();
            }
        }

        private bool IsLoadNext = false;
        public async void NextPage()
        {
            if (IsLoadNext) return;
            IsLoadNext = true;
            PgIndex++;
            var data = await vod?.ClassifyAsync(Class.Type_Id, PgIndex, "", "");
            if (data?.List?.Count > 0)
            {
                PgIndex = data.page;
                var vods = data.List.Select(x => new VideoViewModel(x));
                foreach (var v in vods)
                {
                    Videos.Add(v);
                }
            }
            IsLoadNext = false;
        }



    }
}
