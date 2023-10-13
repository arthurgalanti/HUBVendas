using System.ComponentModel.DataAnnotations;
using HUBVendas.Api.Extensions;
using HUBVendas.Domain.Entities;
using HUBVendas.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HUBVendas.Api.Controllers.v1 {

    [ApiController]
    [Route("api/v1")]
    public class ProductController : Controller {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService) {
            _productService = productService;
            _categoryService = categoryService;
        }

        [ProducesResponseType(typeof(ResponseResult<List<Product>>), 200)]
        [ProducesResponseType(typeof(ResponseResult<object>), 500)]
        [HttpGet]
        public async Task<ActionResult> GetAsync([FromQuery] bool onlyActive = true, [FromQuery] bool loadImages = false) {
            var response = new ResponseResult<List<Product>>();

            try {
                var result = await _productService.GetList();

                response.Data = result.Where(x => !onlyActive || x.Active).ToList();

                if (!loadImages)
                    response.Data.ForEach(p => p.Image = null);

                return Ok(response);
            }
            catch (Exception e) {
                return this.InternalServerError(response, e);
            }
        }

        [ProducesResponseType(typeof(ResponseResult<Product>), 200)]
        [ProducesResponseType(typeof(ResponseResult<object>), 404)]
        [ProducesResponseType(typeof(ResponseResult<object>), 500)]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ResponseResult<Product>>> GetAsync([FromRoute, Required] Guid id) {
            var response = new ResponseResult<Product>();

            try {
                var product = await _productService.GetById(id);

                if (product == null) {
                    response.SetError("Não há nenhum produto com o ID especificado.");
                    return NotFound(response);
                }

                response.Data = product;

                return Ok(response);
            }
            catch (Exception e) {
                return this.InternalServerError(response, e);
            }
        }

        [ProducesResponseType(typeof(ResponseResult<Product>), 201)]
        [ProducesResponseType(typeof(ResponseResult<object>), 400)]
        [ProducesResponseType(typeof(ResponseResult<object>), 500)]
        [HttpPost]
        public async Task<ActionResult<ResponseResult<object>>> CreateAsync([FromBody] ProductDTO request) {
            var response = new ResponseResult<object>();

            try {
                var category = await _categoryService.GetById(request.CategoryId);

                if (category == null) {
                    response.SetError("Não há nenhuma categoria com o ID especificado.");
                    return BadRequest(response);
                }

                Product product = new() {
                    Name = request.Name.Trim(),
                    Description = string.IsNullOrWhiteSpace(request.Description) ? string.Empty : request.Description.Trim(),
                    UnitPrice = request.UnitPrice,
                    Quantity = request.Quantity,
                    Category = category
                };

                if (request.Image != null) {
                    product.Image = new ProductImage();
                    product.Image.SetImage($"{DateTime.Now:yyyyMMddHHmm}-{request.Image.Name}", request.Image.Type, request.Image.Base64);
                }

                var resultId = await _productService.Create(product);

                if (resultId == false)
                    throw new Exception("Ocorreu um erro ao tentar cadastrar o produto.");

                response.Data = product;

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
        public async Task<ActionResult<ResponseResult<object>>> UpdateAsync([FromRoute, Required] Guid id, [FromBody] ProductDTO request, [FromQuery] bool active = true) {
            var response = new ResponseResult<object>();

            try {
                var product = await _productService.GetById(id);

                if (product == null) {
                    response.SetError("O produto não foi encontrado.");
                    return NotFound(response);
                }

                product.Active = active;
                product.Name = string.IsNullOrEmpty(request.Name) ? request.Name : product.Name;
                product.Description = request.Description ?? product.Description;
                product.UnitPrice = request.UnitPrice;
                product.Quantity = request.Quantity;
                product.Category = new Category { Id = request.CategoryId };

                if (request.Image != null) {
                    product.Image = new ProductImage();
                    product.Image.SetImage($"{DateTime.Now:yyyyMMddHHmm}-{request.Image.Name}", request.Image.Type, request.Image.Base64);
                }

                var result = await _productService.Update(product);

                if (!result)
                    throw new Exception("Ocorreu um erro ao tentar atualizar o produto.");

                return Ok(response);
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
                var product = await _productService.GetById(id);

                if (product == null) {
                    response.SetError("O produto não foi encontrado.");
                    return NotFound(response);
                }

                var result = await _productService.Delete(product);

                if (!result)
                    throw new Exception("Ocorreu um erro ao tentar remover o produto.");

                return Ok(response);
            }
            catch (Exception e) {
                return this.InternalServerError(response, e);
            }
        }
    }
}