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

        [HttpGet("/graficas")]

        public async Task<IActionResult> GetGraphics()
        {
            var graphics = await _context.Graficas
                .Include(g => g.Brand)
                .ToListAsync();


            return (Ok (graphics));

        }

        [HttpGet("/Cpus")]

        public async Task<IActionResult> GetCpus()
        {
            var procesadores = await _context.Processors
                .Include(p => p.Brand)
                .ToListAsync();
            return (Ok (procesadores));
        }
     

        
        



    }
}
