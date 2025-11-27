using Ecomerce_Back_End.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController (AppDbContext context)
        {
            _context = context;
        }


        [HttpGet("ProductDetail/{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _context.Products
                .Include(p => p.Brand)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }



        [HttpGet("/graficas")]

        public async Task<IActionResult> GetGraphics()
        {
            var graphics = await _context.Graficas
                .Include(g => g.Brand)
                .ToListAsync();


            return (Ok (graphics));

        }

        [HttpGet("/cpus")]

        public async Task<IActionResult> GetCpus()
        {
            var procesadores = await _context.Processors
                .Include(p => p.Brand)
                .ToListAsync();
            return (Ok (procesadores));
        }

        [HttpGet("/rams")]
        
        public async Task<IActionResult> GetRams()
        {
            var rams = await _context.Ram
                .Include(r => r.Brand)
                .ToListAsync();
            return (Ok (rams));
        }

        [HttpGet("/gabinetes")]
        
        public async Task<IActionResult> GetCases()
        {
            var gabinetes = await _context.Cases
                .Include(c => c.Brand)
                .ToListAsync();
            return (Ok (gabinetes));    
        }

        [HttpGet("/enfriamientos")]

        public async Task<IActionResult> GetCoolers()
        {
            var enfriamientos = await _context.CpuCoolers
                .Include(c => c.Brand)
                .ToListAsync();
            return (Ok (enfriamientos));
        }

        [HttpGet("/motherboards")]

        public async Task<IActionResult> GetMotherboards()
        {
            var motherboards = await _context.Motherboards
                .Include(m => m.Brand)
                .ToListAsync();
            return (Ok(motherboards));
        }
        [HttpGet("/fuentes")]        
        
        public async Task<IActionResult> GetPsus()
        {
            var fuentes = await _context.PowerSupplies
                .Include(p => p.Brand)
                .ToListAsync(); 
            return (Ok (fuentes));
        }


        [HttpGet("/almacenamientos")]

        public async Task<IActionResult> GetStorages()
        {
            var almacenamientos = await _context.Storages
                .Include(a => a.Brand)
                .ToListAsync();
            return (Ok (almacenamientos));
        }

        [HttpGet("/pcs")]

        public async Task<IActionResult> GetPcs()
        {
            var Pc = await _context.Computers
                .Include(c => c.Brand)
                .ToListAsync();
            return (Ok (Pc));
        }

        [HttpGet("/laptops")]
        public async Task<IActionResult> GetLaptops()
        {
            var laptops = await _context.laptops
                .Include(l => l.Product)
                .ThenInclude(p => p.Brand)
                .ToListAsync();

            return Ok(laptops);
        }







    }
}
