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

        [HttpPost("/PostLaptop")]

        public async Task<IActionResult> PostLaptop(Laptop laptop)
        {

            _Context.laptops.Add(laptop);
            await _Context.SaveChangesAsync();
            return Ok(laptop);
        }

        [HttpDelete("/DeleteLaptop")]
        public async Task<IActionResult> DeleteLaptop(int id)
        {
       
            var laptop = await _Context.laptops
                .FirstOrDefaultAsync(x => x.Id == id);

            

            if (laptop == null)
            {
                return NotFound(new
                {
                    message = "La laptop no existe",
                    laptopId = id
                });
            }

       
            _Context.laptops.Remove(laptop);
            await _Context.SaveChangesAsync();

            return Ok(new
            {
                message = "Laptop eliminada correctamente",
                laptopId = id
            });
        }

        [HttpPut("/PutLaptop")]
        public async Task<IActionResult> PutLaptop(Laptop laptop)
        {
            if (laptop == null || laptop.Id == 0)
            {
                return BadRequest(new { message = "Laptop inválida o ID no proporcionado." });
            }

            var existingLaptop = await _Context.laptops
                .FirstOrDefaultAsync(l => l.Id == laptop.Id);

            if (existingLaptop == null)
            {
                return NotFound(new { message = "Laptop no encontrada", laptopId = laptop.Id });
            }

            // Actualizar todas las propiedades
            existingLaptop.Processor = laptop.Processor;
            existingLaptop.RAM = laptop.RAM;
            existingLaptop.RAMType = laptop.RAMType;
            existingLaptop.Storage = laptop.Storage;
            existingLaptop.StorageType = laptop.StorageType;
            existingLaptop.GPU = laptop.GPU;
            existingLaptop.ScreenSize = laptop.ScreenSize;
            existingLaptop.ScreenResolution = laptop.ScreenResolution;
            existingLaptop.RefreshRate = laptop.RefreshRate;
            existingLaptop.HasWifi6 = laptop.HasWifi6;
            existingLaptop.HasBluetooth = laptop.HasBluetooth;
            existingLaptop.Ports = laptop.Ports;
            existingLaptop.Weight = laptop.Weight;
            existingLaptop.Material = laptop.Material;
            existingLaptop.Color = laptop.Color;
            existingLaptop.BatteryCapacity = laptop.BatteryCapacity;
            existingLaptop.ChargerPower = laptop.ChargerPower;
            existingLaptop.OperatingSystem = laptop.OperatingSystem;
            existingLaptop.HasBacklitKeyboard = laptop.HasBacklitKeyboard;
            existingLaptop.HasFingerprintReader = laptop.HasFingerprintReader;
            existingLaptop.HasWebcam = laptop.HasWebcam;
            existingLaptop.HasSpeakers = laptop.HasSpeakers;
            existingLaptop.ProductId = laptop.ProductId;

            _Context.laptops.Update(existingLaptop);
            await _Context.SaveChangesAsync();

            return Ok(new { message = "Laptop actualizada correctamente", laptop = existingLaptop });
        }



    }

















}







