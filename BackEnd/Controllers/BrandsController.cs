using BackEnd.DTOs;
using BackEnd.Models;
using Ecomerce_Back_End.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {

        private readonly AppDbContext _context;
        public BrandsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("/GetBrands")]

        public async Task<IActionResult> GetBrands()
        {
            var brands = await _context.Brands.ToListAsync();
            return Ok(brands);
        }

        [HttpPost("/PostBrand")]

        public async Task<IActionResult> PostBrand(BrandDto Dto)
        {

        
            var exists = await _context.Brands
            .AnyAsync(b => b.Name.ToLower() == Dto.Name.ToLower());

            if (exists)
                return BadRequest("Esa marca ya existe");

            var brand = new Brand
            {
                Name = Dto.Name,
            };


            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();

            return Ok("Marca agregada con éxito");
        }


    }
}
