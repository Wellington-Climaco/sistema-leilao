using FluentResults;

namespace SistemaLeilao.Application.Interface;

public interface IStorageFiles
{
    Task<Result> Upload(Guid key, Stream stream,string contentType,string fileName);
}