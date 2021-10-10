using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Infrastructure.Sql;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
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
            var entity = await _repository.GetAsync();
            if (entity == null || entity.Count == 0)
            {
                return NotFound();
            }

            var response = _mapper.Map<IEnumerable<ProductResponse>>(entity);
            return Ok(response);
        }
    }
}
