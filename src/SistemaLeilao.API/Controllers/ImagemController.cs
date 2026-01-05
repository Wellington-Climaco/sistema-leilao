using Microsoft.AspNetCore.Mvc;
using SistemaLeilao.Application.Interface;
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

            foreach (var image in files)
            {
                if (image.Length == 0)
                    continue;
                
                using var stream = image.OpenReadStream();
                var upload = await _storageFiles.Upload(id, stream, image.ContentType,image.FileName);
                if (upload.IsFailed)
                    return BadRequest(new DefaultResponse<string>(StatusCodes.Status400BadRequest,
                        upload.Errors.Select(x => x.Message).ToList()));
            }

            return Ok();
        }

        [HttpGet]
        [Route("/image/{id:guid}")]
        public async Task<IActionResult> GetImagesByBemId()
        {
            return Ok();
        }
    }
}
