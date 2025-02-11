using System.ComponentModel.DataAnnotations;

namespace ListaVehiculos.web.Domain.Dto;

public class VehicleDto
{
    public int Id { get; set; }
    [Required(ErrorMessage = "La matricula del vehiculo es obligatorio")]
    

    public string Marca { get; set; }

    public string Tipo { get; set; }

    public string Modelo { get; set; }


  

   
}
