// Infrastructure/Services/Storage/ProfilePictureUploader.cs
/*using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using TeqetariApi.Application.DTOs.Create.ProfilePicture;
using TeqetariApi.Application.DTOs.Response.ProfilePicture;
using TeqetariApi.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace TeqetariApi.Infrastructure.Services.ProfilePicture;

public class ProfilePictureUploader : IProfilePictureUploader
{
    private readonly BlobContainerClient _container;
    private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png" };
    private const long MaxFileSize = 2 * 1024 * 1024;

    public ProfilePictureUploader(BlobServiceClient blobServiceClient)
    {
        _container = blobServiceClient.GetBlobContainerClient("profile-pictures");
        _container.CreateIfNotExists(PublicAccessType.Blob);
    }

    public async Task<ProfilePictureResponseDto> UploadAsync(UploadProfilePictureDto dto, CancellationToken ct)
    {
        var file = dto.File;

        if (file.Length == 0 || file.Length > MaxFileSize)
            throw new InvalidOperationException("Invalid profile picture size.");

        var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!AllowedExtensions.Contains(ext))
            throw new InvalidOperationException("Only JPG/PNG allowed for profile pictures.");

        if (!await IsValidImageAsync(file, ct))
            throw new InvalidOperationException("File content is not a valid image.");

        var blobClient = _container.GetBlobClient($"{dto.OwnerId}{ext}");
        using var stream = file.OpenReadStream();
        await blobClient.UploadAsync(stream, new BlobUploadOptions
        {
            HttpHeaders = new BlobHttpHeaders { ContentType = file.ContentType }
        }, ct);

        return new ProfilePictureResponseDto
        {
            Url = blobClient.Uri.ToString(),
            UploadedAt = DateTime.UtcNow
        };
    }

    private static async Task<bool> IsValidImageAsync(IFormFile file, CancellationToken ct)
    {
        var buffer = new byte[8];
        using var stream = file.OpenReadStream();
        await stream.ReadAsync(buffer, 0, buffer.Length, ct);

        bool isJpg = buffer[0] == 0xFF && buffer[1] == 0xD8;
        bool isPng = buffer[0] == 0x89 && buffer[1] == 0x50;
        return isJpg || isPng;
    }
}*/