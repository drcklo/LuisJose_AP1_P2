using LuisJose_AP1_P2.Shared;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace LuisJose_AP1_P2.Server.DAL
{
	public class Context : DbContext
	{
		public DbSet<Entradas> Entradas { get; set; }
		public DbSet<Productos> Productos { get; set; }

		public Context(DbContextOptions<Context> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Productos>().HasData
			(
				new Productos
				{
					ProductoID = 1,
					Descripción = "Maní",
					PrecioCompra = 8,
					PrecioVenta = 15,
					Existencia = 250
				},

				new Productos
				{
					ProductoID = 2,
					Descripción = "Pistachos",
					PrecioCompra = 15,
					PrecioVenta = 30,
					Existencia = 300
				},

				new Productos
				{
					ProductoID = 3,
					Descripción = "Pasas",
					PrecioCompra = 10,
					PrecioVenta = 25,
					Existencia = 130
				},

				new Productos
				{
					ProductoID = 4,
					Descripción = "Ciruelas",
					PrecioCompra = 25,
					PrecioVenta = 50,
					Existencia = 350
				},

				new Productos
				{
					ProductoID = 5,
					Descripción = "Mixto MPP",
					PrecioCompra = 30,
					PrecioVenta = 60,
					Existencia = 320
				},

				new Productos
				{
					ProductoID = 6,
					Descripción = "Mixto MPC",
					PrecioCompra = 30,
					PrecioVenta = 60,
					Existencia = 310
				},

				new Productos
				{
					ProductoID = 7,
					Descripción = "Mixto MPP",
					PrecioCompra = 25,
					PrecioVenta = 50,
					Existencia = 250
				}
			);
		}
	}
}
