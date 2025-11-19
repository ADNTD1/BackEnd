using BackEnd.Models;
using Ecomerce_Back_End.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PcBuildController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PcBuildController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("/paso1")]

        public async Task<IEnumerable<Brand>> GetCpuBrands() =>   // devuelve las marcas de los cpus (Amd, intel)
        await _context.Brands
            .Where(b => b.BrandId == 4 || b.BrandId == 5)
            .Select(b => new Brand
            {
                BrandId = b.BrandId,
                Name = b.Name,
            }).ToListAsync();

        [HttpGet("/paso2")]
        public async Task<IEnumerable<Processors>> GetProcessors(int brandId) =>  // devuelve los procesadores de la marca seleccionada
            await _context.Processors
                .Where(p => p.BrandId == brandId)
                .ToListAsync();


        [HttpGet("/paso3")] // devuelve las motheboards segun el socket del cual el procesador pertenece 
        public async Task<IEnumerable<Motherboard>> GetMotherboards(String SocketType) =>
            await _context.Motherboards
                .Where(m => m.Socket == SocketType)
                .ToListAsync();

        [HttpGet("/paso4")]
        public async Task<IEnumerable<CpuCooler>> GetCoolers() =>  // devuelve los coolers (universales para todos los sockets)
            await _context.CpuCoolers.ToListAsync();

        [HttpGet("/paso5")]
        public async Task<IEnumerable<GraphicsCard>> GetGraphicsCards() =>  // devuelve todas las graficas 
            await _context.Graficas.ToListAsync();

        [HttpGet("/paso6")]
        public async Task<IEnumerable<Ram>> GetRams(int RamSlots, string MemType) =>    // segun los slots de ram de la tarjeta madre y el tipo de memoria que acepta devuelve las rams
            await _context.Ram
                .Where(r => r.Slots <= RamSlots && r.MemType == MemType)
                .ToListAsync();


        [HttpGet("/paso7")]
        public async Task<IEnumerable<Storage>> GetStorages(int m2Slots)
        {
            if (m2Slots == 0)
            {
                // Devuelve solo los que NO son M.2
                return await _context.Storages
                    .Where(s => s.FormFactor != "M2")
                    .ToListAsync();
            }
            else
            {
                // Devuelve todos los almacenamientos
                return await _context.Storages.ToListAsync();
            }
        }

        [HttpGet("/paso8")]
        public async Task<IEnumerable<Case>> GetCases(  // devuelve gabinetes conforme el Form Factor de ma motherboard, el largo de la gpu y la altura de el disipador
            [FromQuery] int GpuLength,
            [FromQuery] int CoolerHeight,
            [FromQuery] string MoboFF)
        {
            var query = _context.Cases.AsQueryable();

            if (GpuLength > 0)
                query = query.Where(c => c.MaxGpuLengthMM >= GpuLength);

            if (CoolerHeight > 0)
                query = query.Where(c => c.MaxCoolerHeightMM >= CoolerHeight);

            if (!string.IsNullOrWhiteSpace(MoboFF))
                query = query.Where(c => c.FormFactorSupport.Contains(MoboFF));

            return await query.ToListAsync();
        }

        [HttpGet("/paso9")]

        public async Task<IEnumerable<PowerSupply>> GetPsus(String PsuFormFactor, int TotalWattage) =>
            await _context.PowerSupplies.Where(p => p.FormFactor == PsuFormFactor && p.Wattage > TotalWattage)
            .ToListAsync();




    }
}
