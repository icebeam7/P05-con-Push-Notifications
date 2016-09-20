using System;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using P05_MisAmigosAzureMobile.Clases;
using System.Linq;

namespace P05_MisAmigosAzureMobile.Datos
{
    public class AzureDataService
    {
        public MobileServiceClient MobileService { get; set; }
        IMobileServiceSyncTable<Amigos> tablaAmigos;

        bool isInitialized;

        public AzureDataService()
        {
            Initialize();
        }

        public async Task Initialize()
        {
            if (isInitialized)
                return;

            MobileService = new MobileServiceClient("http://mis-amigos-app-luis.azurewebsites.net");

            const string path = "syncstore-amigos.db";

            var store = new MobileServiceSQLiteStore(path);
            store.DefineTable<Amigos>();
            
            await MobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());

            tablaAmigos = MobileService.GetSyncTable<Amigos>();
            isInitialized = true;
        }

        public async Task<IEnumerable<Amigos>> ObtenerAmigos()
        {
            await Initialize();
            await SyncAmigos();
            return await tablaAmigos.OrderBy(a => a.Nombre).ToEnumerableAsync();
        }

        public async Task<Amigos> ObtenerAmigo(string id)
        {
            await Initialize();
            await SyncAmigos();
            return (await tablaAmigos.Where(a => a.Id == id).Take(1).ToEnumerableAsync()).FirstOrDefault();
        }

        public async Task<Amigos> AgregarAmigo(string nombre, int sexo, DateTime cumple, string correo, string telefono, bool esMejorAmigo)
        {
            await Initialize();

            var item = new Amigos
            {
                Nombre = nombre,
                Sexo = sexo,
                Cumple = cumple,
                Correo = correo,
                Telefono = telefono,
                EsMejorAmigo = esMejorAmigo
            };

            await tablaAmigos.InsertAsync(item);

            await SyncAmigos();
            return item;
        }

        public async Task<Amigos> ModificarAmigo(string id, string nombre, int sexo, DateTime cumple, string correo, string telefono, bool esMejorAmigo)
        {
            await Initialize();

            var item = await ObtenerAmigo(id);
            item.Nombre = nombre;
            item.Sexo = sexo;
            item.Cumple = cumple;
            item.Correo = correo;
            item.Telefono = telefono;
            item.EsMejorAmigo = esMejorAmigo;

            await tablaAmigos.UpdateAsync(item);

            await SyncAmigos();
            return item;
        }

        public async Task EliminarAmigo(string id)
        {
            await Initialize();

            var item = await ObtenerAmigo(id);

            await tablaAmigos.DeleteAsync(item);

            await SyncAmigos();
        }

        public async Task SyncAmigos()
        {
            await tablaAmigos.PullAsync("Amigos", tablaAmigos.CreateQuery());
            await MobileService.SyncContext.PushAsync();
        }
    }
}
