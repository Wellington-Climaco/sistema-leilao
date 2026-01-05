using Microsoft.AspNetCore.Mvc;
using SistemaLeilao.Application.Interface;
using SistemaLeilao.Application.Request.Imagem;
using SistemaLeilao.Application.Response;

namespace SistemaLeilao.API.Controllers
{
    [ApiController]
    public class ImagemController : ControllerBase
    {
        private readonly IStorageFiles _storageFiles;

        public ImagemController(IStorageFiles storageFiles)
        {
            _storageFiles = storageFiles;
        }

        [HttpPost]
        [Route("upload/{id:guid}")]
        public async Task<IActionResult> UploadImagens([FromRoute] Guid id, List<IFormFile> files)
        {
            if (files is null)
                return BadRequest(new DefaultResponse<string>(StatusCodes.Status400BadRequest, "Nenhum arquivo enviado"));

            var imagens = files.Select(f => new UploadImagemRequest(f.FileName, f.ContentType, f.OpenReadStream()));
            var upload = await _storageFiles.Upload(imagens,id);

            if(upload.IsSuccess)
                return Created($"/image/{id}",upload.Value);

            if (upload.Errors.First().Message.Contains("invalido",StringComparison.OrdinalIgnoreCase))
                return BadRequest(new DefaultResponse<string>(StatusCodes.Status400BadRequest,upload.Errors.Select(x => x.Message).ToList()));

            return StatusCode(500);
        }

        [HttpGet]
        [Route("/image/{id:guid}")]
        public async Task<IActionResult> GetImagesByBemId([FromRoute] Guid id)
        {
            var result = await _storageFiles.GetImagesByBem(id);
            if(result.IsSuccess)
                return Ok(result.Value);

            if(result.Errors.First().Message.Contains("nao encontrado",StringComparison.OrdinalIgnoreCase))
                return NotFound(new DefaultResponse<string>(StatusCodes.Status404NotFound,result.Errors.First().Message));

            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
