using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Planificador.Repositorios;

namespace Planificador
{
    public partial class App : Application
    {
        string dbPath => FileAccessHelper.GetLocalFilePath("planificador.db3");
        public App()
        {
            InitializeComponent();

            new ConnectionManager(dbPath);

            MainPage = new Planificador.MainPage();            
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
