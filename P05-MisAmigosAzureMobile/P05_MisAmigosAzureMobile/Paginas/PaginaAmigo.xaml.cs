using System;
using Xamarin.Forms;
using P05_MisAmigosAzureMobile.Clases;

namespace P05_MisAmigosAzureMobile.Paginas
{
	public partial class PaginaAmigo : ContentPage
	{
        public string ID = "";

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (ID != "")
            {
                Amigos amigo = await App.AzureService.ObtenerAmigo(ID);
                txtNombre.Text = amigo.Nombre;
                pckSexo.SelectedIndex = amigo.Sexo;
                dppCumple.Date = amigo.Cumple;
                txtCorreo.Text = amigo.Correo;
                txtTelefono.Text = amigo.Telefono;
                swtMejorAmigo.IsToggled = amigo.EsMejorAmigo;
            }
        }

        void btnGuardar_Click(object sender, EventArgs a)
        {
            string nombre = txtNombre.Text;
            int sexo = pckSexo.SelectedIndex;
            DateTime cumple = dppCumple.Date;
            string correo = txtCorreo.Text;
            string telefono = txtTelefono.Text;
            bool esMejorAmigo = swtMejorAmigo.IsToggled;

            if (ID == String.Empty)
                App.AzureService.AgregarAmigo(nombre, sexo, cumple, correo, telefono, esMejorAmigo);
            else
                App.AzureService.ModificarAmigo(ID, nombre, sexo, cumple, correo, telefono, esMejorAmigo);
            Navigation.PopAsync();
        }

        void btnEliminar_Click(object sender, EventArgs a)
        {
            if (ID != "")
            {
                App.AzureService.EliminarAmigo(ID);
                Navigation.PopAsync();
            }
        }


        public PaginaAmigo ()
		{
			InitializeComponent ();
		}
	}
}
