using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Web.Domain.Entities;

public class Cliente
{
    public int Id { get; set; }
    public int PersonaId { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal LimeteDeCredito { get; set; }
    public decimal Sueldo { get; set; }
    [ForeignKey(nameof(PersonaId))]
    public virtual Persona DatosPersonales { get; set; } = null!;
}
