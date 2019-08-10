using Okra.Models;
using Okra.Services;
using Okra.ViewModels;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Okra.ViewModel
{
    public class DetailViewModel : BaseViewModel
    {
        public bool IsVisible { get; set; }

        private readonly ISerieServiceDB serieServiceDB;

        public ICommand AddCommand { get; }


        public DetailViewModel(
            INavigationService navigationService
             , ISerieServiceDB serieServiceDB)
            : base(navigationService)
        {
            this.serieServiceDB = serieServiceDB;

            AddCommand = new DelegateCommand(async () => await AddExecute())
                .ObservesCanExecute(() => IsNotBusy);

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
  

                if (TextoFavorito == "FAVORITAR")
                {
                    await serieServiceDB.Add(Serie);
                    await ReturnItem();
                }
                else {
                    await serieServiceDB.Delete(Serie);
                    await ReturnItem();
                }              

            });
        }


        public Serie Serie { get; private set; }

        //name
        string _name;


        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }



        //overview
        string _overview;
        public string Overview
        {
            get { return _overview; }
            set { SetProperty(ref _overview, value); }
        }

        //poster
        string _poster;
        public string Poster
        {
            get { return _poster; }
            set { SetProperty(ref _poster, value); }
        }

        //backdrop
        string _backdrop;
        public string Backdrop
        {
            get { return _backdrop; }
            set { SetProperty(ref _backdrop, value); }
        }

        //votes
        double _votes;
        public double Votes
        {
            get { return _votes; }
            set { SetProperty(ref _votes, value); }
        }

        //release date
        string _releaseDate;
        private CancellationToken err;

        public DetailViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        public string ReleaseDate
        {
            get { return _releaseDate; }
            set { SetProperty(ref _releaseDate, value); }
        }

        string _textoFavorito;
        public string TextoFavorito
        {
            get { return _textoFavorito; }
            set { SetProperty(ref _textoFavorito, value); }
        }



        public override async void OnNavigatingTo(INavigationParameters parameters)
        {

            Serie = (Serie)parameters["serie"];
            Name = Serie.OriginalName;
            Overview = Serie.Overview;
            Poster = Serie.Poster;
            Backdrop = Serie.Backdrop;
            ReleaseDate = Serie.ReleaseData;
            Votes = Serie.VoteAverage;        

            await ExecuteBusyAction(async () =>
            {
                await ReturnItem();
            });
        }

        async Task ReturnItem()
        {
                    
            var collection = await serieServiceDB.GetById(Serie);
                
            if (collection != null)
            {
                TextoFavorito = "DESFAVORITAR";
            }
            else
            {
                TextoFavorito = "FAVORITAR";
            }

        }
    }


}