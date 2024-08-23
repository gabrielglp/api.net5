using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using api.net5.Context;
using api.net5.Models.ClientModel;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using api.net5.DTOs;
using System.Globalization;

namespace api.net5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClientesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClientesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetClientes()
        {
            var clientes = await _context.clientes
                .Select(c => new ClientDTO
                {
                    ClienteId = c.ClienteId,
                    Name = c.Name,
                    CPF = c.CPF,
                    Telephone = c.Telephone,
                    CEP = c.CEP,
                    UF = c.UF,
                    Address = c.Address,
                    District = c.District,
                    Complement = c.Complement,
                    BirthDate = c.BirthDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)
                })
                .ToListAsync();

            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCliente(int id)
        {
            var cliente = await _context.clientes
                .Where(c => c.ClienteId == id)
                .Select(c => new ClientDTO
                {
                    ClienteId = c.ClienteId,
                    Name = c.Name,
                    CPF = c.CPF,
                    Telephone = c.Telephone,
                    CEP = c.CEP,
                    UF = c.UF,
                    Address = c.Address,
                    District = c.District,
                    Complement = c.Complement,
                    BirthDate = c.BirthDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)
                })
                .FirstOrDefaultAsync();

            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCliente(ClientModel cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCliente), new { id = cliente.ClienteId }, cliente);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCliente(int id, ClientModel cliente)
        {
            if (id != cliente.ClienteId)
            {
                return BadRequest();
            }

            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _context.clientes.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            _context.clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchClientes([FromQuery] string cpf = null, [FromQuery] string name = null)
        {
            var query = _context.clientes.AsQueryable();

            if (!string.IsNullOrEmpty(cpf))
            {
                query = query.Where(c => c.CPF == cpf);
            }

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(c => c.Name.Contains(name));
            }

            var clientes = await query
                .Select(c => new ClientDTO
                {
                    ClienteId = c.ClienteId,
                    Name = c.Name,
                    CPF = c.CPF,
                    Telephone = c.Telephone,
                    CEP = c.CEP,
                    UF = c.UF,
                    Address = c.Address,
                    District = c.District,
                    Complement = c.Complement,
                    BirthDate = c.BirthDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)
                })
                .ToListAsync();

            return Ok(clientes);
        }

        private bool ClienteExists(int id)
        {
            return _context.clientes.Any(e => e.ClienteId == id);
        }
    }
}
