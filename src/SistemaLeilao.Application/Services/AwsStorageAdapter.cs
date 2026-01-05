using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using FluentResults;
using Microsoft.Extensions.Configuration;
using SistemaLeilao.Application.Interface;

namespace SistemaLeilao.Application.Services;

public class AwsStorageAdapter : IStorageFiles
{
    private readonly IConfiguration _configuration;
    private IAmazonS3 _awsS3Client;
    private string[] imageFormatAllowed = ["image/jpeg", "image/png", "image/webp"];

    public AwsStorageAdapter(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    private void S3Connection()
    {
        string acessKeyId = _configuration["Aws:KeyId"] ?? throw new Exception("Fail to get aws credentials");
        string secretKey = _configuration["Aws:SecretKey"] ?? throw new Exception("Fail to get aws credentials");
        var awsCredentials = new BasicAWSCredentials(acessKeyId, secretKey);
        var config = new AmazonS3Config() { RegionEndpoint = RegionEndpoint.SAEast1 };
        
        _awsS3Client = new AmazonS3Client(awsCredentials, config);
    }

    public async Task<Result> Upload(Guid key, Stream stream, string contentType,string fileName)
    {
        try
        {
            if (!imageFormatAllowed.Contains(contentType))
                return Result.Fail("formato de imagem inválido");

            S3Connection();
            var request = new PutObjectRequest
            {
                BucketName = "leilao-api-s3",
                Key = $"{key}/images/{DateTime.UtcNow:yyyyMMddHHmmssfff}{fileName}",
                InputStream = stream,
                ContentType = contentType
            };
            
            await _awsS3Client.PutObjectAsync(request);
            
            return Result.Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            return Result.Fail(e.ToString());
        }
        
    }
}