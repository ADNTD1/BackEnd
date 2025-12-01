using BackEnd.Migrations;
using BackEnd.Models;
using Ecomerce_Back_End.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CRUDController : ControllerBase
    {
        private readonly AppDbContext _Context;
        public CRUDController(AppDbContext context)
        {
            _Context = context;
        }

        [HttpGet("/GetLaptops")]

        public async Task<IActionResult> GetLaptop(int id)
        {
            var laptops = await _Context.laptops
                .Where(l => l.Id == id)
                .ToListAsync();
            return Ok(laptops);
        }


       











        }
       




}

