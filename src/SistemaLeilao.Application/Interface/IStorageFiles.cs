using FluentResults;
using SistemaLeilao.Application.Request.Imagem;
using SistemaLeilao.Application.Response.Imagem;

namespace SistemaLeilao.Application.Interface;

public interface IStorageFiles
{
    Task<Result<IEnumerable<string>>> Upload(IEnumerable<UploadImagemRequest> request,Guid BemId);
    Task<Result<IEnumerable<ImagemResponse>>> GetImagesByBem(Guid bemId);
}