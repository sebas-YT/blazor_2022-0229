using System.Text.Json;

namespace ListaVehiculos.web.Infraestructure.Services
{
    /// <summary>
    /// Interfaz para el servicio del archivo Json
    /// </summary>
    public interface IJsonFileService<T> where T : class
    {

        /// <summary>
        /// Escribe en un archivo
        /// </summary>
        /// <param name="archivo">
        /// Es el nombre del archivo donde se escribirá el contenido
        /// </param>
        /// <param name="datos">
        /// Representa los datos a escribir
        /// </param>
        /// <returns>Nada</returns>
        Task Escribir(string archivo, List<T> datos);

        /// <summary>
        /// Lee un archivo
        /// </summary>
        /// <param name="archivo">
        /// Es el nombre del archivo a leer
        /// </param>
        /// <returns>
        /// Una lista de datos
        /// </returns>
        Task<List<T>> Leer(string archivo);
    }

    public class JsonFileService<T> : IJsonFileService<T> where T : class
    {
        private string FicheroBase
        {
            get
            {
                var patch = $"{Environment.GetFolderPath(
                    Environment.SpecialFolder.MyDocuments
                    )}\\ListaVehiculkos";
                if (!Directory.Exists(patch))
                    Directory.CreateDirectory(patch);

                return patch;
            }
        }

        public async Task<List<T>> Leer(string archivo)
        {
            /// Si el archivo no tiene extensión, se le agrega .json
            var file =
                archivo.Contains(".") ?
                $"{archivo.Split('.')[0]}.json" :
                $"{archivo}.json";
            /// Se obtiene la ruta completa del archivo
            var ruta_larga = $"{FicheroBase}\\{file}";
            /// Si el archivo no existe, se crea con un array vacío
            if (!File.Exists(ruta_larga))
                await File.WriteAllTextAsync(ruta_larga, "[]");
            ///Se lee el contenido del archivo
            var contenido = await File.ReadAllTextAsync(ruta_larga);
            /// Si el archivo está vacío, se retorna una lista vacía
            if (string.IsNullOrEmpty(contenido))
                return new List<T>();
            /// Se deserializa el contenido del archivo
            return JsonSerializer.Deserialize<List<T>>(contenido) ?? new List<T>();
        }

        public async Task Escribir(string archivo, List<T> datos)
        {
            /// Si el archivo no tiene extensión, se le agrega .json
            var file =
                archivo.Contains(".") ?
                $"{archivo.Split('.')[0]}.json" :
                $"{archivo}.json";
            /// Se obtiene la ruta completa del archivo
            var ruta_larga = $"{FicheroBase}\\{file}";
            /// Se serializa la lista de datos
            var contenido = JsonSerializer.Serialize(datos);
            /// Se escribe el contenido en el archivo
            await File.WriteAllTextAsync(ruta_larga, contenido);
        }
    }

}
