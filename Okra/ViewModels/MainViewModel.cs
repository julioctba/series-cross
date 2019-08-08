using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
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
    public class MainViewModel : BaseViewModel
    {
        readonly ISerieService serieService;

        public ICommand AddCommand { get; }

        public ICommand ItemClickCommand { get; }


        public MainViewModel(
            Prism.Navigation.INavigationService navigationService
             , ISerieService serieService)
            : base(navigationService)
        {
            this.serieService = serieService;

            ItemClickCommand = new Command<Serie>(async (item)
               => await ItemClickCommandExecute(item));
        }

        private ObservableCollection<Serie> items = new ObservableCollection<Serie>();
        public ObservableCollection<Serie> Items
        {
            get => items;
            set => SetProperty(ref items, value);
        }

     

        public override async void OnNavigatingTo(INavigationParameters parameters)
        {
            await ExecuteBusyAction(async () =>
            {
                await LoadDataAsync();
            });
        }
   

        async Task LoadDataAsync()
        {
            var result = await serieService.GetSeriesAsync();
            Items = new ObservableCollection<Serie>(result.Series);           
        }

        async Task ItemClickCommandExecute(Serie serie)
        {          
                var param = new NavigationParameters();
                param.Add("serie", serie);                
                await NavigationService.NavigateAsync($"{nameof(DetailView)}",param);
        }

    }
}
