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
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrdersController(IOrderService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderFormModel model)
        {
            var order = await _service.CreateAsync(model);

            if (order != null)
            {
                return new OkObjectResult(order);
            }

            return new BadRequestResult();
        }

        


        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return new OkObjectResult(await _service.GetAllAsync());
        }



       
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var order = await _service.GetAsync(id);
            if (order != null)
            {
                return new OkObjectResult(order);
            }

            return new NotFoundResult();
        }


        
       [HttpPut("{id}")]
       public async Task<ActionResult> Update(int id, UpdateOrderModel model)
       {
            var orderRow = await _service.UpdateAsync(id, model);
            if (orderRow != null)
            {
                return new OkObjectResult(orderRow);
            }

            return new BadRequestResult();
        }




       [HttpDelete("{id}")]
       public async Task<ActionResult> DeleteOrderById(int id)
       {
            if (await _service.DeleteAsync(id))
            {
                return new OkResult();
            }

            return new BadRequestResult();


        }


      





    }
}
