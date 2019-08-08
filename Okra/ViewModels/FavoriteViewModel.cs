using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Okra.Models;
using Okra.Services;
using Okra.Views;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace Okra.ViewModels
{
    public sealed class FavoriteViewModel : BaseViewModel
    {
        private readonly ISerieServiceDB serieServiceDB;

        public ICommand ItemClickCommand { get; }

        public FavoriteViewModel(
            INavigationService navigationService
             , ISerieServiceDB serieServiceDB)
            : base(navigationService)
        {
            this.serieServiceDB = serieServiceDB;
       

            ItemClickCommand = new Command<Serie>(async (item)
               => await ItemClickCommandExecute(item));
        }

        private ObservableCollection<Serie> series = new ObservableCollection<Serie>();
        public ObservableCollection<Serie> Series
        {
            get => series;
            set => SetProperty(ref series, value);
        }

        

        private async Task AddExecute()
        {
            await ExecuteBusyAction(async () =>
            {              
                await LoadSerie();
            });
        }

        public override async void OnNavigatingTo(INavigationParameters parameters)
        {
            await ExecuteBusyAction(async () =>
            {
                await LoadSerie();                
            });
        }

        private async Task LoadSerie()
        {
            var collection = await serieServiceDB.All();
            Series = new ObservableCollection<Serie>(collection);
        }

        async Task ItemClickCommandExecute(Serie serie)
        {
            var param = new NavigationParameters();
            param.Add("serie", serie);
            await NavigationService.NavigateAsync($"{nameof(DetailView)}", param);
        }
    }
}
