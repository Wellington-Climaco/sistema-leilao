using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using FluentResults;
using Microsoft.Extensions.Configuration;
using SistemaLeilao.Application.Interface;
using SistemaLeilao.Application.Request.Imagem;
using SistemaLeilao.Core.Entities;
using SistemaLeilao.Core.Interface;
using static System.Net.Mime.MediaTypeNames;

namespace SistemaLeilao.Application.Services;

public class AwsStorageAdapter : IStorageFiles
{
    private readonly IConfiguration _configuration;
    private readonly IImagemRepository _imagemRepository;
    private readonly IBemRepository _bemRepository;

    private IAmazonS3 _awsS3Client;
    private string[] imageFormatAllowed = ["image/jpeg", "image/png", "image/webp"];

    public AwsStorageAdapter(IConfiguration configuration, IImagemRepository imagemRepository, IBemRepository bemRepository)
    {
        _configuration = configuration;
        _imagemRepository = imagemRepository;
        _bemRepository = bemRepository;
    }

    private void S3Connection()
    {
        string acessKeyId = _configuration["Aws:KeyId"] ?? throw new Exception("Fail to get aws credentials");
        string secretKey = _configuration["Aws:SecretKey"] ?? throw new Exception("Fail to get aws credentials");

        var awsCredentials = new BasicAWSCredentials(acessKeyId, secretKey);
        var config = new AmazonS3Config() { RegionEndpoint = RegionEndpoint.SAEast1 };
        
        _awsS3Client = new AmazonS3Client(awsCredentials, config);
    }

    public async Task<Result<IEnumerable<string>>> Upload(IEnumerable<UploadImagemRequest> request,Guid BemId)
    {
        try
        {
            List<Imagem> imagens = new();

            bool possuiFormatoInvalido = request.Any(x => !imageFormatAllowed.Contains(x.ContentType));
            if (possuiFormatoInvalido)
                return Result.Fail("formato de imagem invalido");

            var bem = await _bemRepository.FindById(BemId);
            if (bem is null)
                return Result.Fail("Id invalido, bem não existe");

            foreach (var imagem in request)
            {                
                string key = $"{BemId}/images/{DateTime.UtcNow:yyyyMMddHHmmssfff}{imagem.FileName}";

                S3Connection();
                var s3Request = new PutObjectRequest
                {
                    BucketName = "leilao-api-s3",
                    Key = key,
                    InputStream = imagem.Stream,
                    ContentType = imagem.ContentType
                };

                await _awsS3Client.PutObjectAsync(s3Request);

                string url = $"https://leilao-api-s3.s3.sa-east-1.amazonaws.com/{key}";
                imagens.Add(new Imagem(url,BemId));
            }

            await _imagemRepository.SaveCollection(imagens);
            return Result.Ok(imagens.Select(i => i.Url));

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            return Result.Fail(e.ToString());
        }
    }
}