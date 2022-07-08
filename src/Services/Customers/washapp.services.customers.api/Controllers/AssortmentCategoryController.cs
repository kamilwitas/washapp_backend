using System.ComponentModel.DataAnnotations;
using washapp.services.customers.api.Models;
using washapp.services.customers.application.Commands.AssortmentCategories;
using washapp.services.customers.application.Queries.AssortmentCategories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using washapp.services.customers.api.Models.Categories;

namespace washapp.services.customers.api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AssortmentCategoryController : ControllerBase
    {
        private readonly IMediator _mediatR;
        public AssortmentCategoryController(IMediator mediatR)
        {
            _mediatR = mediatR;
        }
        
        [HttpGet]
        [SwaggerOperation(Summary = "Endpoint do pobierania wszystkich kategorii asortymentów. Wymagana rola: Admin, User", Tags = new string[] {"Assortment categories"})]
        public async Task<ActionResult> GetAllCategories()
        {
            var categories = await _mediatR.Send(new GetAssortmentCategories());
            return Ok(categories);
        }
        
        [HttpPost]
        [Authorize(Policy = "Administrator")]
        [SwaggerOperation(Summary = "Endpoint do tworzenia kategorii asortymentów. Wymagana rola: Admin", Tags = new string[] {"Assortment categories"})]
        [Route("createCategory")]
        public async Task<ActionResult>CreateCategory([FromBody] CreateCategoryRequest request)
        {
            var command = request.AsCommand();
            await _mediatR.Send(command);
            return Created($"", null);
        }
        
        [HttpDelete]
        [Authorize(Policy = "Administrator")]
        [SwaggerOperation(Summary = "Endpoint do usuwania kategorii asortymentów. Wymagana rola: Admin", Tags = new string[] {"Assortment categories"})]
        [Route("deleteCategory/{categoryId}")]
        public async Task<ActionResult>DeleteCategory([Required] Guid categoryId)
        {
            await _mediatR.Send(new DeleteCategory(categoryId));
            return Ok();
        }
        [HttpPut]
        [Authorize(Policy = "Administrator")]
        [SwaggerOperation(Summary = "Endpoint do aktualizacji kategorii asortymentów. Wymagana rola: Admin", Tags = new string[] {"Assortment categories"})]
        [Route("updateCategory/{categoryId}")]
        public async Task<ActionResult>UpdateCategory(Guid categoryId, [FromBody] UpdateCategoryRequest request)
        {
            var command = request.AsCommand(categoryId);
            await _mediatR.Send(command);
            return Ok();
        }

    }
}
