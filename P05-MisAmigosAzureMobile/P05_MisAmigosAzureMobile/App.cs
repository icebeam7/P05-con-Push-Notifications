using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using P05_MisAmigosAzureMobile.Datos;
using P05_MisAmigosAzureMobile.Paginas;

namespace P05_MisAmigosAzureMobile
{
	public class App : Application
	{
        public static AzureDataService AzureService = new AzureDataService();

        public App ()
		{
            MainPage = new NavigationPage(new PaginaListaAmigos());
        }

        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
