using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuisJose_AP1_P2.Shared
{
	public class Entradas
	{
		[Key]
		public int EntradaId { get; set; }
		public DateTime Fecha { get; set; } = DateTime.Now;

		[Required(ErrorMessage = "El Concepto es un campo requerido")]
		public string? Concepto { get; set; }

		[Required(ErrorMessage = "El ProductoID es un campo requerido")]
		public int ProductoId { get; set; }

		[Required(ErrorMessage = "Se requiere especificar la cantidad producida")]
		public int CantidadProducida { get; set; }

		[ForeignKey("EntradaId")]
		public List<EntradasDetalle> entradasDetalle { get; set; } = new List<EntradasDetalle>();
	}
}
