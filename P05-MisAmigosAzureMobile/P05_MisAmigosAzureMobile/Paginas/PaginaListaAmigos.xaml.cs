using System;
using Xamarin.Forms;
using P05_MisAmigosAzureMobile.Clases;

namespace P05_MisAmigosAzureMobile.Paginas
{
	public partial class PaginaListaAmigos : ContentPage
	{
		public PaginaListaAmigos ()
		{
			InitializeComponent ();
		}

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            lsvAmigos.ItemsSource = await App.AzureService.ObtenerAmigos();
        }

        private void lsvAmigos_Selected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                Amigos amigo = e.SelectedItem as Amigos;
                PaginaAmigo pagina = new PaginaAmigo();
                pagina.ID = amigo.Id;
                Navigation.PushAsync(pagina);
            }
        }

        void btnNuevo_Click(object sender, EventArgs a)
        {
            Navigation.PushAsync(new PaginaAmigo());
        }

    }
}
