using Avalonia.Controls;
using Avalonia.Input;
using Peach.Application.Interfaces;
using Peach.Model.Models;
using PeachPlayer.Controls;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;

namespace PeachPlayer.ViewModels
{
    public class ClassifyListViewModel : ViewModelBase
    {
        private ICmsService vod;
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

        [Reactive]
        public bool IsLoading { get; set; }
        public ClassifyListViewModel(ClassModel _class, List<FilterModel> _filters = null)
        {
            ScrollCommand = ReactiveCommand.Create<object>(Scroll);
            vod = Locator.Current.GetService<ICmsService>();
            Class = _class;
            filters = _filters;
        }

        public async void LoadVodList()
        {
            IsLoading = true;
            MessageBus.Current.SendMessage(IsLoading, "IsLoading");
            Videos.Clear();
            var data = await vod?.CategoryAsync(Class.Type_Id, PgIndex, "", "");
            if (data?.List?.Count > 0)
            {
                var vods = data.List.Select(x => new VideoViewModel(x));
                foreach (var v in vods)
                {
                    Videos.Add(v);
                }
            }
            IsLoading = false;
            MessageBus.Current.SendMessage(IsLoading, "IsLoading");
        }

        public void Scroll(object o)
        {
            var t = (ScrollViewer)o;
            if (t.Offset.Length >= t.Extent.Height - (t.DesiredSize.Height * 1.2))
            {
                NextPage();
            }
        }

        public async void NextPage()
        {
            if (IsLoading) return;

            IsLoading = true;
            MessageBus.Current.SendMessage(IsLoading, "IsLoading");
            PgIndex++;
            var data = await vod?.CategoryAsync(Class.Type_Id, PgIndex, "", "");
            if (data?.List?.Count > 0)
            {
                PgIndex = data.page;
                var vods = data.List.Select(x => new VideoViewModel(x));
                foreach (var v in vods)
                {
                    Videos.Add(v);
                }
            }
            IsLoading = false;
            MessageBus.Current.SendMessage(IsLoading, "IsLoading");
        }



    }
}
