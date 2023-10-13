using System.ComponentModel.DataAnnotations;
using HUBVendas.Api.Extensions;
using HUBVendas.Domain.Entities;
using HUBVendas.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HUBVendas.Api.Controllers.v1 {

    [ApiController]
    [Route("api/v1")]
    public class CategoryController : Controller {

        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService) {
            _categoryService = categoryService;
        }

        [ProducesResponseType(typeof(ResponseResult<List<Category>>), 200)]
        [ProducesResponseType(typeof(ResponseResult<object>), 500)]
        [HttpGet]
        public async Task<ActionResult> GetAsync([FromQuery] bool onlyActive = true) {

            var response = new ResponseResult<List<Category>>();

            try {
                var result = await _categoryService.GetList();

                response.Data = result.Where(x => !onlyActive || x.Active).ToList();
                return Ok(response);
            }
            catch (Exception e) {
                return this.InternalServerError(response, e);
            }
        }

        [ProducesResponseType(typeof(ResponseResult<List<Category>>), 200)]
        [ProducesResponseType(typeof(ResponseResult<object>), 404)]
        [ProducesResponseType(typeof(ResponseResult<object>), 500)]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ResponseResult<Category>>> GetAsync([FromRoute, Required] Guid id) {

            var response = new ResponseResult<Category>();

            try {
                var category = await _categoryService.GetById(id);

                if (category == null) {
                    response.SetError("Não há nenhuma categoria com o ID especificado.");
                    return NotFound(response);
                }

                response.Data = category;


                return Ok(response);
            }
            catch (Exception e) {
                return this.InternalServerError(response, e);
            }
        }

        [ProducesResponseType(typeof(ResponseResult<Category>), 201)]
        [ProducesResponseType(typeof(ResponseResult<object>), 500)]
        [HttpPost]
        public async Task<ActionResult<ResponseResult<object>>> CreateAsync([FromBody] CategoryDTO request) {
            var response = new ResponseResult<object>();

            try {
                Category category = new() {
                    Name = request.Name.Trim(),
                    Description = string.IsNullOrWhiteSpace(request.Description) ? string.Empty : request.Description.Trim()
                };

                var resultId = await _categoryService.Create(category);

                if (resultId == false)
                    throw new Exception("Ocorreu um erro ao tentar cadastrar a categoria.");

                response.Data = category;

                return Created(nameof(CreateAsync), response);
            }
            catch (Exception e) {
                return this.InternalServerError(response, e);
            }
        }

        [ProducesResponseType(typeof(ResponseResult<object>), 200)]
        [ProducesResponseType(typeof(ResponseResult<object>), 404)]
        [ProducesResponseType(typeof(ResponseResult<object>), 500)]
        [HttpPut("{id:int}/edit")]
        public async Task<ActionResult<ResponseResult<object>>> UpdateAsync([FromRoute, Required] Guid id, [FromBody] CategoryDTO request, [FromQuery] bool active = true) {
            var response = new ResponseResult<object>();

            try {
                var category = await _categoryService.GetById(id);

                if (category == null) {
                    response.SetError("A categoria não foi encontrada.");
                    return NotFound(response);
                }

                category.Active = active;
                category.Name = string.IsNullOrEmpty(request.Name) ? request.Name : category.Name;
                category.Description = request.Description ?? category.Description;

                var result = await _categoryService.Update(category);

                if (!result)
                    throw new Exception("Ocorreu um erro ao tentar atualizar a categoria.");
                return response;
            }
            catch (Exception e) {
                return this.InternalServerError(response, e);
            }
        }

        [ProducesResponseType(typeof(ResponseResult<object>), 200)]
        [ProducesResponseType(typeof(ResponseResult<object>), 404)]
        [ProducesResponseType(typeof(ResponseResult<object>), 500)]
        [HttpPatch("{id:int}/remove")]
        public async Task<ActionResult<ResponseResult<object>>> RemoveAsync([FromRoute, Required] Guid id) {
            var response = new ResponseResult<object>();

            try {
                var category = await _categoryService.GetById(id);

                if (category == null) {
                    response.SetError("A categoria não foi encontrada.");
                    return NotFound(response);
                }

                var result = await _categoryService.Delete(category);

                if (!result)
                    throw new Exception("Ocorreu um erro ao tentar remover a categoria.");

                return response;
            }
            catch (Exception e) {
                return this.InternalServerError(response, e);
            }
        }


    }
}