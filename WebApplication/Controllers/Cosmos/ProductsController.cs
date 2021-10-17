﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Infrastructure.Cosmos;
using WebApplication.Infrastructure.Cosmos.Models;
using WebApplication.Models;

namespace WebApplication.Controllers.Cosmos
{
    [ApiExplorerSettings(GroupName = "Products with Cosmos DB")]
    [Produces("application/json")]
    [Route("api/cosmos/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// 商品の一覧を取得します。
        /// </summary>
        /// <returns></returns>
        /// <response code="200">商品の一覧を返します。</response>
        /// <response code="404">商品が登録されていません。</response>  
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> Get()
        {
            var items = await _repository.GetAsync();
            if (items == null || items.Count == 0)
            {
                return NotFound();
            }

            var response = _mapper.Map<IEnumerable<ProductResponse>>(items);
            return Ok(response);
        }

        /// <summary>
        /// 商品を ID で検索します。
        /// </summary>
        /// <param name="id">商品 ID </param>
        /// <returns></returns>
        /// <response code="200">検索した商品を返します。</response>
        /// <response code="404">指定された商品 ID が登録されていません。</response>  
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductResponse>> Get(string id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<ProductResponse>(item);
            return Ok(response);
        }

        /// <summary>
        /// 商品を追加します。
        /// </summary>
        /// <param name="product">商品</param>
        /// <returns></returns>
        /// <response code="201">追加した商品を返します。</response>
        /// <response code="400">追加する商品が指定されていません。</response>  
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductResponse>> Post([FromBody] ProductRequest product)
        {
            var item = _mapper.Map<Product>(product);
            await _repository.InsertAsync(item);

            var response = _mapper.Map<ProductResponse>(item);
            return CreatedAtAction(nameof(Get), new { id = item.Id }, response);
        }

        /// <summary>
        /// 商品を編集します。
        /// </summary>
        /// <param name="id">商品 ID </param>
        /// <param name="product">商品</param>
        /// <returns></returns>
        /// <response code="204">商品を編集しました。</response>
        /// <response code="404">編集する商品が登録されていません。</response>  
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(string id, [FromBody] ProductRequest product)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            _mapper.Map(product, item);

            await _repository.UpdateAsync(item);
            return NoContent();
        }

        /// <summary>
        /// 商品を削除します。
        /// </summary>
        /// <param name="id">商品 ID </param>
        /// <returns></returns>
        /// <response code="204">商品を削除しました。</response>
        /// <response code="404">削除する商品が登録されていません。</response>  
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(item);
            return NoContent();
        }
    }
}
