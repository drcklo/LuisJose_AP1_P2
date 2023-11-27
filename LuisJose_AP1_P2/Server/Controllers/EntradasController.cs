using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LuisJose_AP1_P2.Server.DAL;
using LuisJose_AP1_P2.Shared;

namespace LuisJose_AP1_P2.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntradasController : ControllerBase
    {
        private readonly Context _context;

        public EntradasController(Context context)
        {
            _context = context;
        }

        // GET: api/Entradas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Entradas>>> GetEntradas()
        {
          if (_context.Entradas == null)
          {
              return NotFound();
          }
            return await _context.Entradas.ToListAsync();
        }

        // GET: api/Entradas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Entradas>> GetEntradas(int id)
        {
            if(_context.Entradas == null)
            {
                return NotFound();
            }

            var entrada = await _context.Entradas.Include(e => e.entradasDetalle).Where( e => e.EntradaID == id).FirstOrDefaultAsync();

            if(entrada == null)
            {
                return NotFound();
            }

            foreach(var item in entrada.entradasDetalle)
            {
                Console.WriteLine($"{item.DetalleID}, {item.EntradaID}, {item.ProductoID}, {item.CantidadUtilizada}");
            }

            return entrada;
        }

        // PUT: api/Entradas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEntradas(int id, Entradas entradas)
        {
            if (id != entradas.EntradaID)
            {
                return BadRequest();
            }

            _context.Entry(entradas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntradasExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Entradas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Entradas>> PostEntradas(Entradas entradas)
        {
            if (!EntradasExists(entradas.EntradaID))
            {
                Productos? producto = new Productos();
                foreach (var productoConsumido in entradas.entradasDetalle)
                {
                    producto = _context.Productos.Find(productoConsumido.ProductoID);

                    if (producto != null)
                    {
                        producto.Existencia -= productoConsumido.CantidadUtilizada;
                        _context.Productos.Update(producto);
                        await _context.SaveChangesAsync();
                        _context.Entry(producto).State = EntityState.Detached;
                    }
                }
                await _context.Entradas.AddAsync(entradas);
            }
            else
            {
                var entradaAnterior = _context.Entradas.Include(e => e.entradasDetalle).AsNoTracking()
                .FirstOrDefault(e => e.EntradaID == entradas.EntradaID);

                Productos? producto = new Productos();

                if (entradaAnterior != null && entradaAnterior.entradasDetalle != null)
                {
                    foreach (var productoConsumido in entradaAnterior.entradasDetalle)
                    {
                        if (productoConsumido != null)
                        {
                            producto = _context.Productos.Find(productoConsumido.ProductoID);

                            if (producto != null)
                            {
                                producto.Existencia += productoConsumido.CantidadUtilizada;
                                _context.Productos.Update(producto);
                                await _context.SaveChangesAsync();
                                _context.Entry(producto).State = EntityState.Detached;
                            }
                        }
                    }
                }

                if (entradaAnterior != null)
                {
                    producto = _context.Productos.Find(entradaAnterior.ProductoID);

                    if (producto != null)
                    {
                        producto.Existencia -= entradaAnterior.CantidadProducida;
                        _context.Productos.Update(producto);
                        await _context.SaveChangesAsync();
                        _context.Entry(producto).State = EntityState.Detached;
                    }
                }

                _context.Database.ExecuteSqlRaw($"Delete from entradasDetalle where EntradaID = {entradas.EntradaID}");

                foreach (var productoConsumido in entradas.entradasDetalle)
                {
                    producto = _context.Productos.Find(productoConsumido.ProductoID);

                    if (producto != null)
                    {
                        producto.Existencia -= productoConsumido.CantidadUtilizada;
                        _context.Productos.Update(producto);
                        await _context.SaveChangesAsync();
                        _context.Entry(producto).State = EntityState.Detached;
                        _context.Entry(productoConsumido).State = EntityState.Added;
                    }
                }

                producto = _context.Productos.Find(entradas.ProductoID);

                if (producto != null)
                {
                    producto.Existencia += entradas.CantidadProducida;
                    _context.Productos.Update(producto);
                    await _context.SaveChangesAsync();
                    _context.Entry(producto).State = EntityState.Detached;
                }
                _context.Entradas.Update(entradas);
            }

            await _context.SaveChangesAsync();
            _context.Entry(entradas).State = EntityState.Detached;
            return Ok(entradas);
        }

        // DELETE: api/Entradas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntradas(int id)
        {
            var entrada = await _context.Entradas.Include(e => e.entradasDetalle).FirstOrDefaultAsync(e => e.EntradaID == id);

            if (entrada == null)
            {
                return NotFound();
            }

            foreach (var productoConsumido in entrada.entradasDetalle)
            {
                var producto = await _context.Productos.FindAsync(productoConsumido.ProductoID);

                if (producto != null)
                {
                    producto.Existencia += productoConsumido.CantidadUtilizada;
                    _context.Productos.Update(producto);
                }
            }

            var productoInicial = await _context.Productos.FindAsync(entrada.ProductoID);

            if (productoInicial != null)
            {
                productoInicial.Existencia += entrada.CantidadProducida;
                _context.Productos.Update(productoInicial);
            }

            _context.Entradas.Remove(entrada);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EntradasExists(int id)
        {
            return (_context.Entradas?.Any(e => e.EntradaID == id)).GetValueOrDefault();
        }
    }
}
