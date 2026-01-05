namespace SistemaLeilao.Application.Request.Imagem;

public record UploadImagemRequest(string FileName, string ContentType, Stream Stream);

