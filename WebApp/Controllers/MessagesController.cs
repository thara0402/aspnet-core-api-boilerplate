using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Infrastructure;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageRepository _repository;

        public MessagesController(IMessageRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// メッセージを取得します。
        /// </summary>
        /// <returns></returns>
        /// <response code="200">メッセージを返します。</response>
        /// <response code="404">メッセージが登録されていません。</response>  
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            var messages = await _repository.ReceiveAsync();
            if (messages == null || messages.Count == 0)
            {
                return NotFound();
            }
            return Ok(messages);
        }

        /// <summary>
        /// メッセージを追加します。
        /// </summary>
        /// <param name="message">メッセージ</param>
        /// <returns></returns>
        /// <response code="200">メッセージを追加しました。</response>
        /// <response code="400">追加するメッセージが指定されていません。</response>  
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post([FromBody] string message)
        {
            await _repository.SendAsync(message);
            return Ok();
        }
    }
}
