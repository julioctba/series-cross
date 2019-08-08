using Okra.Infra;
using Okra.Infra.Api;
using Okra.Repositories;
using Okra.Services;
using Okra.ViewModel;
using Okra.ViewModels;
using Okra.Views;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using Refit;
using Xamarin.Forms;

namespace Okra
{
    public partial class App : PrismApplication
    {
        public App()
        {
        }

        public App(IPlatformInitializer platformInitializer)
            : base(platformInitializer)
        {
        }

        public App(IPlatformInitializer platformInitializer, bool setFormsDependencyResolver)
            : base(platformInitializer, setFormsDependencyResolver)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            await NavigationService.NavigateAsync($"NavigationPage/{nameof(HomePage)}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<HomePage>();
            containerRegistry.RegisterForNavigation<MainPage, MainViewModel>(nameof(MainPage));
            containerRegistry.RegisterForNavigation<FavoritePage, FavoriteViewModel>(nameof(FavoritePage));
            containerRegistry.RegisterForNavigation<DetailView, DetailViewModel>();

    


            var api = RestService.For<ITmdbApi>(new System.Net.Http.HttpClient() {
                BaseAddress = new System.Uri(AppSetting.ApiUrl)

            });

            containerRegistry.RegisterInstance<ITmdbApi>(api);

            containerRegistry.Register<ISerieService, SerieService>();
            containerRegistry.Register<ISerieServiceDB, SerieServiceDB>();


            containerRegistry.Register<ILocalDataBaseRepository, LocalDataBaseRepository>();
        }
    }
}
