using Avalonia.Controls;
using Peach.Application.Interfaces;
using Peach.Model.Models;
using PeachPlayer.Views;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;


namespace PeachPlayer.ViewModels
{
    public class VideoPlayViewModel : ViewModelBase
    {

        [Reactive]
        public string Vod_name { get; set; }
        [Reactive]
        public string Vod_content { get; set; }
        [Reactive]
        public string Vod_actor { get; set; }
        [Reactive]
        public string Vod_area { get; set; }
        [Reactive]
        public string Vod_year { get; set; }
        [Reactive]
        public string Vod_remarks { get; set; }


        [Reactive]
        public VodModel Vod { get; set; } = new();

        [Reactive]
        public ObservableCollection<LineModel> Lines { get; set; } = new();

        [Reactive]
        public int SelectedLine { get; set; }

        [Reactive]
        public ObservableCollection<JIShuModel> Selectedjishus { get; set; } = new();

        public ReactiveCommand<string, Unit> SwitchJiShuCommand { get; }

        private ICmsService infoService;
        public VideoPlayViewModel()
        {
            infoService = Locator.Current.GetService<ICmsService>();
            SwitchJiShuCommand = ReactiveCommand.Create<string>(SwitchJiShu);
            this.WhenAnyValue(x => x.Vod).Where(v => v != null).Subscribe(v =>
            {
                Vod_name = v.vod_name;
                Vod_content = v.vod_content;
                Vod_actor = v.vod_actor;
                Vod_area = v.vod_area;
                Vod_year = v.vod_year;
                Vod_remarks = v.vod_remarks;
            });
            this.WhenAnyValue(x => x.SelectedLine).Where(i => i >= 0 && Lines.Count > i).Subscribe(s =>
            {
                Selectedjishus = new ObservableCollection<JIShuModel>(Lines[s].Episodes);
            });
        }

        public void Show(VodModel svod, List<LineModel> lines)
        {
            Vod = svod;
            if (lines != null)
            {
                Lines = new ObservableCollection<LineModel>(lines);
                SelectedLine = 0;
            }
        }

        public void SwitchJiShu(string ji)
        {
            MessageBus.Current.SendMessage(ji, "SelectedJi");
        }

    }
}