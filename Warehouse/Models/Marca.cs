using System.ComponentModel.DataAnnotations;

namespace Warehouse.Models
{
    public class Marca
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage ="El nombre de la marca es requerido.")]
        [Display(Name = "Nombre de la marca")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre de la marca debe tener entre 2 y 100 caracteres.")]
        public string Nombre { get; set; }



        public ICollection<Producto> Productos { get; set; }

    }
}
