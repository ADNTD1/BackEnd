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

        public class ProductCreateRequest
        {
            public Product Product { get; set; }
            public DetailProduct Detail { get; set; }
        }

        public class DetailProduct
        {
            public string Type { get; set; }
            public JsonElement Data { get; set; }
        }
      
        [HttpGet("/GetProductos")]
        public async Task<IActionResult> GetProducts(int id)
        {
            var Products = await _Context.Products
                .Where(p => p.Id == id)
                .Include(p => p.Brand)
                .ToListAsync();
            return Ok(Products);
        }

        [HttpPost("/PostProduct")]
        public async Task<IActionResult> PostProduct([FromBody] ProductCreateRequest request)
        {
            var producto = request.Product;
            var detail = request.Detail;

            _Context.Products.Add(producto);

            try
            {
                await _Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al guardar el producto: {ex.Message}");
            }

            try
            {
                bool detailAdded = false;

                switch (detail.Type.ToLower())
                {
                    case "case":
                        var Case = JsonSerializer.Deserialize<Case>(detail.Data);
                        Case.Id = producto.Id;
                        _Context.Cases.Add(Case);
                        detailAdded = true;
                        break;

                    case "cpucooler":
                        var Cpucooler = JsonSerializer.Deserialize<CpuCooler>(detail.Data);
                        Cpucooler.Id = producto.Id;
                        _Context.CpuCoolers.Add(Cpucooler);
                        detailAdded = true;
                        break;

                    case "graphicscard":
                        var Graphicscard = JsonSerializer.Deserialize<GraphicsCard>(detail.Data);
                        Graphicscard.Id = producto.Id;
                        _Context.Graficas.Add(Graphicscard);
                        detailAdded = true;
                        break;

                    case "motherboard":
                        var motherboard = JsonSerializer.Deserialize<Motherboard>(detail.Data);
                        motherboard.Id = producto.Id;
                        _Context.Motherboards.Add(motherboard);
                        detailAdded = true;
                        break;

                    case "powersupply":
                        var Powersupply = JsonSerializer.Deserialize<PowerSupply>(detail.Data);
                        Powersupply.Id = producto.Id;
                        _Context.PowerSupplies.Add(Powersupply);
                        detailAdded = true;
                        break;

                    case "processor":
                        var Procesor = JsonSerializer.Deserialize<Processors>(detail.Data);
                        Procesor.Id = producto.Id;
                        _Context.Processors.Add(Procesor);
                        detailAdded = true;
                        break;

                    case "ram":
                        var Ram = JsonSerializer.Deserialize<Ram>(detail.Data);
                        Ram.Id = producto.Id;
                        _Context.Ram.Add(Ram);
                        detailAdded = true;
                        break;

                    case "storage":
                        var Storage = JsonSerializer.Deserialize<Storage>(detail.Data);
                        Storage.Id = producto.Id;
                        _Context.Storages.Add(Storage);
                        detailAdded = true;
                        break;

                    default:
                        return BadRequest($"Tipo de producto no reconocido: {detail.Type}");
                }

                if (detailAdded)
                {
                    await _Context.SaveChangesAsync();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al guardar el detalle del producto: {ex.Message}");
            }
        }



        [HttpPut("/PutProdcut")]

        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id == 0 || id != product.Id)
            {
                return BadRequest();
            }

            _Context.Entry(product).State = EntityState.Modified;

            try
            {
                await _Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        [HttpDelete("/DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(int id, string Type)
        {
            var product = await _Context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            switch (Type.ToLower())
            {
                case "case":
                    var CaseToDelete = await _Context.Cases.FindAsync(id);
                    if (CaseToDelete != null)
                    {
                        _Context.Cases.Remove(CaseToDelete);
                    }
                    break;

                case "cpucooler":
                    var CpucoolerToDelete = await _Context.CpuCoolers.FindAsync(id);
                    if (CpucoolerToDelete != null)
                    {
                        _Context.CpuCoolers.Remove(CpucoolerToDelete);
                    }
                    break;

                case "graphicscard":
                    var GraphicsCardToDelete = await _Context.Graficas.FindAsync(id);
                    if (GraphicsCardToDelete != null)
                    {
                        _Context.Graficas.Remove(GraphicsCardToDelete);
                    }
                    break;
                case "motherboard":
                    var MoboToDelete = await _Context.Motherboards.FindAsync(id);
                    if (MoboToDelete != null)
                    {
                        _Context.Motherboards.Remove(MoboToDelete);
                    }
                    break;
                case "powersupply":
                    var PsuToDelete = await _Context.PowerSupplies.FindAsync(id);
                    if (PsuToDelete != null)
                    {
                        _Context.PowerSupplies.Remove(PsuToDelete);
                    }
                    break;
                case "processor":
                    var CpuToDelete = await _Context.Processors.FindAsync(id);
                    if (CpuToDelete != null)
                    {
                        _Context.Processors.Remove(CpuToDelete);
                    }
                    break;
                case "ram":
                    var RamToDelete = await _Context.Ram.FindAsync(id);
                    if (RamToDelete != null)
                    {
                        _Context.Ram.Remove(RamToDelete);
                    }
                    break;
                case "storage":
                    var StorageToDelete = await _Context.Storages.FindAsync(id);
                    if (StorageToDelete != null)
                    {
                        _Context.Storages.Remove(StorageToDelete);
                    }
                    break;
                default:
                    return BadRequest();
            }

            _Context.Products.Remove(product);
            await _Context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _Context.Products.Any(e => e.Id == id);
        }
    }




}

