using Avalonia;
using Peach.Application.Interfaces;
using Peach.Model.Models;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

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

        private Vector offset;
        public Vector Offset
        {
            get { return offset; }
            set { this.RaiseAndSetIfChanged(ref offset, value); Debug.WriteLine($"offset:{offset}"); }
        }

        private Size viewport;
        public Size Viewport
        {
            get { return viewport; }
            set { this.RaiseAndSetIfChanged(ref viewport, value);Debug.WriteLine($"verticalViewport:{viewport}"); }
        }


        public ClassifyListViewModel(ClassModel _class, List<FilterModel> _filters = null)
        {
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

        public async void NextPage()
        {
            PgIndex++;
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



    }
}
