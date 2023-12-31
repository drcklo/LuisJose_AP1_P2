﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuisJose_AP1_P2.Shared
{
	public class EntradasDetalle
	{
		[Key]
		public int DetalleID { get; set; }
		public int EntradaID { get; set; }

		[Required(ErrorMessage = "El ProductoID es un campo requerido")]
		public int ProductoID { get; set; }

		[Required(ErrorMessage = "Se requiere especificar la cantidad utilizada")]
		[Range(1, int.MaxValue, ErrorMessage ="El valor debe ser mayor de 0")]
		public int CantidadUtilizada { get; set; }
	}
}
