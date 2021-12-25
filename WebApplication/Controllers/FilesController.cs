using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Infrastructure;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFileRepository _repository;

        public FilesController(IFileRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// ファイルをアップロードします。
        /// </summary>
        /// <param name="upload">アップロードするファイル</param>
        /// <returns></returns>
        /// <response code="200">ファイルをアップロードしました。</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Upload([FromForm] UploadRequest upload)
        {
            var fileName = upload.FileName;
            var file = upload.File;

            if (file.Length > 0)
            {
                using (var fileStream = file.OpenReadStream())
                {
                    await _repository.UploadAsync(fileStream, fileName, file.ContentType);
                }
                return Ok();
            }
            return BadRequest();
        }

        /// <summary>
        /// ファイルをダウンロードします。
        /// </summary>
        /// <param name="fileName">ダウンロードするファイル名</param>
        /// <returns></returns>
        /// <response code="200">ダウンロードしたファイルを返します。</response>
        [HttpGet("{fileName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> DownloadAsync(string fileName)
        {
            var fileContents = await _repository.DownloadAsync(fileName);
            return File(fileContents, "application/octet-stream", fileName);
        }

        /// <summary>
        /// ファイルの一覧を取得します。
        /// </summary>
        /// <returns></returns>
        /// <response code="200">ファイルの一覧を返します。</response>
        /// <response code="404">ファイルが登録されていません。</response>  
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            var items = await _repository.GetAsync();
            if (items == null || items.Count == 0)
            {
                return NotFound();
            }
            return Ok(items);
        }
    }
}
