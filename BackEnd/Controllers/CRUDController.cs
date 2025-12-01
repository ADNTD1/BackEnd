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

        // Laptops ------------------------------------------------------------------------------------------------

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

        // Computadoras -------------------------------------------------------------------------

        [HttpGet("/GetComputadoras")]

        public async Task<IActionResult> GetComputers(int id)
        {
            var Computadora = await _Context.Computers.
                Where(c => c.Id == id).ToListAsync();
            return Ok(Computadora);
        }

        [HttpPost("/PostComputadora")]

        public async Task<IActionResult> PostComputers(Computers computer)
        {
            _Context.Computers.Add(computer);
            await _Context.SaveChangesAsync();

            return Ok(computer);

        }

        [HttpDelete("/DeleteComputer")]

        public async Task<IActionResult> DeleteComputer(int id)
        {

            var computadora = await _Context.Computers.FirstOrDefaultAsync(c => c.Id == id);

            if (computadora != null)
            {
                _Context.Computers.Remove(computadora);
            }
            else
            {
                return NotFound("computadora no encontrada");
            }
            await _Context.SaveChangesAsync();
            return Ok("computadora eliminda correctamente");

        }

        [HttpPut("/PutComputer")]
        public async Task<IActionResult> PutComputer(Computers computer)
        {
            if (computer == null || computer.Id == 0)
                return BadRequest("Computadora inválida o ID no proporcionado");

            var existingComputer = await _Context.Computers
                .FirstOrDefaultAsync(c => c.Id == computer.Id);

            if (existingComputer == null)
                return NotFound(new { message = "Computadora no encontrada", computer.Id });


            existingComputer.Name = computer.Name;
            existingComputer.Description = computer.Description;
            existingComputer.Stock = computer.Stock;
            existingComputer.Price = computer.Price;
            existingComputer.ImageUrl = computer.ImageUrl;
            existingComputer.BrandId = computer.BrandId;

            existingComputer.Cpu = computer.Cpu;
            existingComputer.Disk = computer.Disk;
            existingComputer.Gpu = computer.Gpu;
            existingComputer.TotalRam = computer.TotalRam;
            existingComputer.MemType = computer.MemType;
            existingComputer.Psu = computer.Psu;
            existingComputer.Os = computer.Os;
            existingComputer.Case = computer.Case;

            _Context.Computers.Update(existingComputer);
            await _Context.SaveChangesAsync();

            return Ok(new { message = "Computadora actualizada correctamente", computadora = existingComputer });
        }

        // usuarios / permisos --------------------------------------------------------------------------


        [HttpGet("/Usuarios")]

        public async Task<IActionResult> GetUsuarios()
        {
            var users = await _Context.Users.ToListAsync();

            return Ok(users);

        }

        [HttpPut("/AdminUser")]
        public async Task<IActionResult> SetAdminUser(int id, [FromBody] bool isAdmin)
        {
            var user = await _Context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
                return NotFound(new { message = "Usuario no encontrado", userId = id });

            user.Admin = isAdmin;
            _Context.Users.Update(user);
            await _Context.SaveChangesAsync();

            return Ok(new { message = "Rol de usuario actualizado correctamente", userId = id, Admin = user.Admin });
        }



    }
}










