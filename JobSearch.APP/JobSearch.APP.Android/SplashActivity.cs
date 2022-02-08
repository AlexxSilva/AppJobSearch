using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSearch.APP.Droid
{
    [Activity(Label = "App - JobSearch", Theme = "@style/MainTheme.Splash", Icon = "@mipmap/ic_launcher", MainLauncher = true, NoHistory = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class SplashActivity : Activity
    {
        static readonly string TAG = "X:" + typeof(SplashActivity).Name;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Log.Debug(TAG, "SplashActivity.OnCreate");
            //StartActivity(typeof(MainActivity));
            //Finish();


            OverridePendingTransition(0, 0);

        }


        protected override void OnResume()
        {
            base.OnResume();
            Task startupWork = new Task(() => { SimulateStartup(); });
            startupWork.Start();
        }

        async void SimulateStartup()
        {
            Log.Debug(TAG, "Executar inicialização");
            await Task.Delay(1000); // 
            Log.Debug(TAG, "Iniciar Activity");
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }

        public override void OnBackPressed()
        {
            //base.OnBackPressed(); // precisa para ao voltar não cancelar a inicialização do splashScreen
            //ao abrir o app novamente
        }

    }
}