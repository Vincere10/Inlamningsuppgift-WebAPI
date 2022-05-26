using Inlamningsuppgift.Entities;
using Inlamningsuppgift.Filter;
using Inlamningsuppgift.Models;
using Inlamningsuppgift.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inlamningsuppgift.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [UseApiKey]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }



        [HttpPost]
        public async Task<ActionResult> Create(CreateProductModel model)
        {
            var result = await _service.CreateAsync(model);
            if (result != null)
            {
                return new OkObjectResult(result);
            };
            return new BadRequestResult();
        }




        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadProductModel>>> GetAll()
        {
            return new OkObjectResult(await _service.GetAllAsync());
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<ReadProductModel>> Get(int id)
        {
            var product = await _service.GetAsync(id);
            if (product != null)
                return new OkObjectResult(product);

            return new NotFoundResult();
        }


        

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateProductModel model)
        {
            var result = await _service.UpdateAsync(id, model);
            if (result != null)
            {
                return new OkObjectResult(result);
            }
            return new BadRequestResult();

        }





        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (await _service.DeleteAsync(id))
            {
                return new OkResult();
            }
            return new NotFoundResult();

        }




        //private bool ProductExists(int id)
        //{
        //    return _context.Products.Any(e => e.ProductId == id);
        //}



    }
}

