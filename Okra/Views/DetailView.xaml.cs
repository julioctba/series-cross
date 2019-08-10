using Okra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Okra.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailView : ContentPage
    {

        public DetailView()
        {            
            InitializeComponent();


            
           /* groupPage.ToolbarItems.Clear();           

            groupPage.ToolbarItems.Add(new ToolbarItem
            {
                Name = "OK",
                Icon = "icon.png",
                Order = ToolbarItemOrder.Primary,
                
            });*/
            

        }

    }
}