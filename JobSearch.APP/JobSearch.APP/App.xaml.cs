using JobSearch.APP.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JobSearch.APP
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //App.Current.Properties.Remove("User");

            if (App.Current.Properties.ContainsKey("User"))
            {
                MainPage = new NavigationPage(new Start());
            }
            else
            {
                MainPage = new NavigationPage(new Login());
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
