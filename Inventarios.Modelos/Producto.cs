using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventarios.Modelos
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(30, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        public string NumeroSerie{ get; set; } = null!;

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(60, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        public string Descripcion { get; set; } = null!;

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Column(TypeName = "Decimal(12:0)")]
        [DisplayName("Precio ")]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public double Precio { get; set; }


        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Column(TypeName = "Decimal(12:0)")]
        [DisplayName("Costo")]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public double Costo { get; set; }   

        public string ImagenUrl { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public bool Estado { get; set; }

        [Required(ErrorMessage = "Categoria es requerida.")]
        public int CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        public Categoria Categoria { get; set; }

        [Required(ErrorMessage = "Marca es requerida.")]
        public int MarcaId { get; set; }

        [ForeignKey("MarcaId")]
        public Marca Marca { get; set; }

        public int? PadreId { get; set; }
        public virtual Producto Padre { get; set; }

    }
}
