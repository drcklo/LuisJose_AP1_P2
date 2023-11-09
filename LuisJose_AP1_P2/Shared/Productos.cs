using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuisJose_AP1_P2.Shared
{
	public class Productos
	{
		[Key]
		public int ProductoID { get; set; }

		[Required(ErrorMessage = "La descripción es requerida")]
		public string? Descripción { get; set; }

		[Required(ErrorMessage = "Se debe indicar el precio al cual se compró el producto")]
		public double PrecioCompra { get; set; }

		[Required(ErrorMessage = "Se debe indicar con que precio se quiere vender el producto")]
		public double PrecioVenta { get; set; }

		[Required(ErrorMessage = "Se debe indicar la cantidad de existencias deseadas")]
		public int Existencia { get; set; }
	}
}
