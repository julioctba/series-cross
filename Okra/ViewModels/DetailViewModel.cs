using Okra.Models;
using Okra.Services;
using Okra.ViewModels;
using Prism.Commands;
using Prism.Navigation;
using System.Threading.Tasks;
using System.Windows.Input;

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



        private async Task AddExecute()
        {
            await ExecuteBusyAction(async () =>
            {                
                await serieServiceDB.Add(Serie);

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

        public DetailViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        public string ReleaseDate
        {
            get { return _releaseDate; }
            set { SetProperty(ref _releaseDate, value); }
        }

               

        public override void OnNavigatingTo(INavigationParameters parameters)
        {

            Serie = (Serie) parameters["serie"];
            Name = Serie.OriginalName;
            Overview = Serie.Overview;
            Poster = Serie.Poster;
            Backdrop = Serie.Backdrop;
            ReleaseDate = Serie.ReleaseData;
            Votes = Serie.VoteAverage;
        }


    }

}
