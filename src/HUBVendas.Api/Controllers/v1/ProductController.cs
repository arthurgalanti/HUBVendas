using System.ComponentModel.DataAnnotations;
using HUBVendas.Api.Extensions;
using HUBVendas.Domain.Entities;
using HUBVendas.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HUBVendas.Api.Controllers.v1 {

    [ApiController]
    [Route("api/v1/products")]
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
                var filteredList = result.Where(x => !onlyActive || x.Active).ToList();

                if (filteredList.Count == 0) {
                    response.SetSucess("Não há nenhum produto na lista.");
                    return Ok(response);
                }

                if (!loadImages)
                    filteredList.ForEach(p => p.Image = null);

                response.SetSucess("Produtos listados com sucesso!", filteredList);
                return Ok(response);
            }
            catch (Exception e) {
                return this.InternalServerError(response, e);
            }
        }

        [ProducesResponseType(typeof(ResponseResult<Product>), 200)]
        [ProducesResponseType(typeof(ResponseResult<object>), 404)]
        [ProducesResponseType(typeof(ResponseResult<object>), 500)]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ResponseResult<Product>>> GetAsync([FromRoute, Required] Guid id) {
            var response = new ResponseResult<Product>();

            try {
                var product = await _productService.GetById(id);

                if (product == null) {
                    response.SetError("Não há nenhum produto com o ID especificado.");
                    return NotFound(response);
                }

                response.SetSucess("Produto listado com sucesso!", product);
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
                request.Validate();
                if (!request.IsValid) {
                    var notifications = request.Notifications.Select(x => x.Message).ToList();
                    response.SetErrors(notifications);
                    return BadRequest(response);
                }

                var category = await _categoryService.GetById(request.CategoryId);

                if (category == null) {
                    response.SetError("Não há nenhuma categoria com o ID especificado.");
                    return BadRequest(response);
                }

                Product product = new() {
                    Name = request.Name.Trim(),
                    Description = string.IsNullOrWhiteSpace(request.Description) ? string.Empty : request.Description.Trim(),
                    Sku = request.Sku,
                    BarCode = request.BarCode,
                    SellingPrice = request.SellingPrice,
                    CostPrice = request.CostPrice,
                    Stock = request.Stock,
                    Category = category
                };

                if (request.Image != null) {
                    product.Image = new ProductImage();
                    product.Image.SetImage($"{DateTime.Now:yyyyMMddHHmm}-{request.Image.Name}", request.Image.Type, request.Image.Base64);
                }

                var result = await _productService.Create(product);

                if (result == false) {
                    response.SetError("Ocorreu um erro ao tentar cadastrar o produto.");
                    return StatusCode(500, response);
                }

                response.SetSucess("Produto cadastrado com sucesso!", product);
                return Created(nameof(CreateAsync), response);
            }
            catch (Exception e) {
                return this.InternalServerError(response, e);
            }
        }

        [ProducesResponseType(typeof(ResponseResult<object>), 200)]
        [ProducesResponseType(typeof(ResponseResult<object>), 400)]
        [ProducesResponseType(typeof(ResponseResult<object>), 404)]
        [ProducesResponseType(typeof(ResponseResult<object>), 500)]
        [HttpPut("{id:guid}/edit")]
        public async Task<ActionResult<ResponseResult<object>>> UpdateAsync([FromRoute, Required] Guid id, [FromBody] ProductDTO request, [FromQuery] bool active = true) {
            var response = new ResponseResult<object>();

            try {
                request.Validate();
                if (!request.IsValid) {
                    var notifications = request.Notifications.Select(x => x.Message).ToList();
                    response.SetErrors(notifications);
                    return BadRequest(response);
                }

                var product = await _productService.GetById(id);

                if (product == null) {
                    response.SetError("O produto não foi encontrado.");
                    return NotFound(response);
                }

                product.Active = active;
                product.Name = request.Name != product.Name ? request.Name : product.Name;
                product.Description = request.Description != product.Description ? request.Description : product.Description;
                product.Sku = request.Sku != product.Sku ? request.Sku : product.Sku;
                product.BarCode = request.BarCode != product.BarCode ? request.BarCode : product.BarCode;
                product.SellingPrice = request.SellingPrice != product.SellingPrice ? request.SellingPrice : product.SellingPrice;
                product.CostPrice = request.CostPrice != product.CostPrice ? request.CostPrice : product.CostPrice;
                product.Stock = request.Stock != product.Stock ? request.Stock : product.Stock;
                product.Category = new Category { Id = request.CategoryId };

                if (request.Image != null) {
                    product.Image = new ProductImage();
                    product.Image.SetImage($"{DateTime.Now:yyyyMMddHHmm}-{request.Image.Name}", request.Image.Type, request.Image.Base64);
                }

                var result = await _productService.Update(product);

                if (result == false) {
                    response.SetError("Ocorreu um erro ao tentar atualizar o produto.");
                    return StatusCode(500, response);
                }

                response.SetSucess("Produto atualizado com sucesso!", product);
                return Ok(response);
            }
            catch (Exception e) {
                return this.InternalServerError(response, e);
            }
        }

        [ProducesResponseType(typeof(ResponseResult<object>), 200)]
        [ProducesResponseType(typeof(ResponseResult<object>), 404)]
        [ProducesResponseType(typeof(ResponseResult<object>), 500)]
        [HttpPatch("{id:guid}/remove")]
        public async Task<ActionResult<ResponseResult<object>>> RemoveAsync([FromRoute, Required] Guid id) {
            var response = new ResponseResult<object>();

            try {
                var product = await _productService.GetById(id);

                if (product == null) {
                    response.SetError("O produto não foi encontrado.");
                    return NotFound(response);
                }

                var result = await _productService.Delete(product);

                if (result == false) {
                    response.SetError("Ocorreu um erro ao tentar remover o produto.");
                    return StatusCode(500, response);
                }

                return Ok(response);
            }
            catch (Exception e) {
                return this.InternalServerError(response, e);
            }
        }
    }
}