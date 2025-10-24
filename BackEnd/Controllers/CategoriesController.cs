using Ecomerce_Back_End.Data;
using Ecomerce_Back_End.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Ecomerce_Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<CategoryDto>> Get() =>
            await _context.ProductCategories.Select(c => new CategoryDto
            {
                Id = c.Id,
                CategoryName = c.CategoryName
            }).ToListAsync();



    }
}