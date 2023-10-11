using EvetWebsite.Services.AwsDtos;

namespace EvetWebsite.Services.AWS
{
    public interface IStorageService
    {
        Task<S3ResponseDto> UploadFileAsync(S3Object obj, AwsCredentials awsCredentialsValues);
        Task<FileReturnResponseDto> UploadFileReturnUrlAsync(S3Object obj, AwsCredentials awsCredentialsValues, string old_key);
        Task<FileReturnResponseDto> MainUploadFileReturnUrlAsync(string old_key, IFormFile? file);
        Task<FileReturnResponseDto> DeleteObjectAsync(AwsCredentials awsCredentialsValues, string bucket, string key);
        Task<string> DownloadFileAsync(string file, string bucketName, AwsCredentials awsCredentialsValues);
    }
}
