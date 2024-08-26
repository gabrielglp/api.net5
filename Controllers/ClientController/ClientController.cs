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
            var clients = await _context.clients
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

            return Ok(clients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCliente(int id)
        {
            var cliente = await _context.clients
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

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.clients.Add(cliente);
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();

                    return CreatedAtAction(nameof(GetCliente), new { id = cliente.ClienteId }, cliente);
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    return StatusCode(500, "Ocorreu um erro no servidor.");
                }
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCliente(int id, ClientModel cliente)
        {
            if (id != cliente.ClienteId)
            {
                return BadRequest();
            }

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var existingCliente = await _context.clients.FindAsync(id);
                    if (existingCliente == null)
                    {
                        return NotFound();
                    }

                    _context.Entry(existingCliente).CurrentValues.SetValues(cliente);
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();

                    return Ok(new { message = "Client successfully updated." });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    return StatusCode(500, "Ocorreu um erro no servidor.");
                }
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _context.clients.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            _context.clients.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchClientes([FromQuery] string cpf = null, [FromQuery] string name = null)
        {
            var query = _context.clients.AsQueryable();

            if (!string.IsNullOrEmpty(cpf))
            {
                query = query.Where(c => c.CPF == cpf);
            }

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(c => c.Name.Contains(name));
            }

            var clients = await query
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

            return Ok(clients);
        }

        private bool ClienteExists(int id)
        {
            return _context.clients.Any(e => e.ClienteId == id);
        }
    }
}
