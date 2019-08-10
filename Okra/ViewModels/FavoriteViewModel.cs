using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Okra.Models;
using Okra.Services;
using Okra.Views;
using Prism;
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
            base.OnNavigatingTo(parameters);
            await LoadSerie();               
        }

        private async Task LoadSerie()
        {
            await ExecuteBusyAction(async () => {

                var collection = await serieServiceDB.All();

                Series.Clear();

                foreach (var item in collection)
                {
                    Series.Add(item);
                }
            });
        }

        private bool _isRefreshing = false;

        [Obsolete]
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        [Obsolete]
        public ICommand RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsRefreshing = true;

                    await LoadSerie();

                    IsRefreshing = false;
                });
            }
        }

        async Task ItemClickCommandExecute(Serie serie)
        {
            var param = new NavigationParameters();
            param.Add("serie", serie);
            await NavigationService.NavigateAsync($"{nameof(DetailView)}", param);
        }
    }
}
