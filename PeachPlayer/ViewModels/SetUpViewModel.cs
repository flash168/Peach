using Peach.Application.Services;
using PeachPlayer.Foundation;
using ReactiveUI;
using System;
using System.Reactive;

namespace PeachPlayer.ViewModels
{
    public class SetUpViewModel : ViewModelBase
    {

        public ReactiveCommand<Unit, Unit> SaveConfigCommand { get; }

        private string sourceUrl;
        public string SourceUrl
        {
            get => sourceUrl;
            set => this.RaiseAndSetIfChanged(ref sourceUrl, value);
        }

        private string hipySnifferUrl;
        public string HipySnifferUrl
        {
            get => hipySnifferUrl;
            set => this.RaiseAndSetIfChanged(ref hipySnifferUrl, value);
        }



        ConfigStorage configStorage = ConfigStorage.Instance;
        public SetUpViewModel()
        {
            IObservable<bool> isInputValid = this.WhenAnyValue(x => x.SourceUrl,
                x => !string.IsNullOrWhiteSpace(x) && x.ToLower().StartsWith("http"));

            SaveConfigCommand = ReactiveCommand.Create(SaveConfig, isInputValid);

            HipySnifferUrl = configStorage.AppConfig.HipySnifferUrl;
            SourceUrl = configStorage.AppConfig.SourceUrl;
        }


        public void SaveConfig()
        {
            configStorage.AppConfig.SourceUrl = this.SourceUrl;
            configStorage.AppConfig.HipySnifferUrl = this.HipySnifferUrl;
            configStorage.Save();
            var aa = Interactions.ShowNote.Handle("配置文件保存成功了！");
        }


    }
}
