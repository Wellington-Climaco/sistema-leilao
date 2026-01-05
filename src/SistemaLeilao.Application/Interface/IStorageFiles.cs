using FluentResults;
using SistemaLeilao.Application.Request.Imagem;

namespace SistemaLeilao.Application.Interface;

public interface IStorageFiles
{
    Task<Result<IEnumerable<string>>> Upload(IEnumerable<UploadImagemRequest> request,Guid BemId);
}