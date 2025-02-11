using ListaVehiculos.web.Domain.Dto;
using ListaVehiculos.web.Infraestructure.Services;

namespace ListaVehiculos.web.Application.Services
{
 
    public interface IVehicleService
    {
        public Task<List<VehicleDto>> Consultar();

        public Task<List<VehicleDto>> Eliminar(int Id);

        
        public Task<List<VehicleDto>> Guardar(VehicleDto vehiculo);
    }

    public class VehicleService(IJsonFileService<VehicleDto> jsonFileService) : IVehicleService
    {
        private readonly string archivo = "vehiculos";

        
        /// <param name="vehiculo">
        
      
        public async Task<List<VehicleDto>> Guardar(VehicleDto vehiculo)
        {
        
            var vehiculos = await jsonFileService.Leer(archivo);
           
            vehiculo.Id = !vehiculos.Any() ? 1 : vehiculos.Max(x => x.Id) + 1;
            
            vehiculos.Add(vehiculo);
          
            await jsonFileService.Escribir(archivo, vehiculos);
          
            return vehiculos;
        }

       
        public async Task<List<VehicleDto>> Consultar()
        {
             
            var vehiculos = await jsonFileService.Leer(archivo);
           
            return vehiculos;
        }

       
        /// <param name="Id">
       
        public async Task<List<VehicleDto>> Eliminar(int Id)
        {
            
            var vehiculos = await jsonFileService.Leer(archivo);
           
            var vehiculo = vehiculos.FirstOrDefault(x => x.Id == Id);
           
            if (vehiculo != null)
            {
                
                vehiculos.Remove(vehiculo);
                
                await jsonFileService.Escribir(archivo, vehiculos);
            }
            
            return vehiculos;
        }
    }

}
