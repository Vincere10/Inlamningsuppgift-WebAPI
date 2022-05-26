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
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserModel model)
        {
            var result = await _service.CreateAsync(model);
            if (result != null)
            {
                return new OkObjectResult(result);
            };
            return new BadRequestResult();
            
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            return new OkObjectResult(await _service.GetAllAsync());
        }



        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var user = await _service.GetAsync(id);
            if (user != null)
                return new OkObjectResult(user);

            return new NotFoundResult();
        }





        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateById(int id, UpdateUserModel model)
        {
            var result = await _service.UpdateAsync(id, model);
            if (result != null)
            {
                return new OkObjectResult(result);
            }
            return new BadRequestResult();
        }



        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteById(int id)
        {
            if (await _service.DeleteAsync(id))
            {
                return new OkResult();
            }
            return new NotFoundResult();
        }

    }
}
