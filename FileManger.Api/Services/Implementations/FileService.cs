
using FileManger.Api.Entities;
using FileManger.Api.Persistence;
using System.Threading;

namespace FileManger.Api.Services.Implementations;

public class FileService(IWebHostEnvironment webHostEnvironment, ApplicationDbContext context) : IFileService
{
    private readonly string _filePath = $"{webHostEnvironment.WebRootPath}/Uploads";
    private readonly string _ImagesPath = $"{webHostEnvironment.WebRootPath}/Images";
    private readonly ApplicationDbContext _context = context;

    public async Task<Guid> UploadAsync(IFormFile file, CancellationToken cancellationToken = default)
    {
        var uploadedFile = await SaveFile(file, cancellationToken);

        await _context.AddAsync(uploadedFile, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return uploadedFile.Id;
    }
    public async Task<IEnumerable<Guid>> UploadManyAsync(IFormFileCollection files, CancellationToken cancellationToken = default)
    {
        List<UplodedFile> uplodedFiles = [];

        foreach (var file in files)
        {
            var uploadedFile = await SaveFile(file, cancellationToken);
            uplodedFiles.Add(uploadedFile);
        }

        await _context.AddRangeAsync(uplodedFiles, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return uplodedFiles.Select(x => x.Id).ToList();
    }
    public async Task UploadImageAsync(IFormFile image, CancellationToken cancellationToken = default)
    {
        var path = Path.Combine(_ImagesPath, image.FileName);

        using var stream = File.Create(path);
        await image.CopyToAsync(stream, cancellationToken);
    }
    public async Task<(byte[] fileContent, string contentType, string fileName)> DownloadAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var file = await _context.Files.FindAsync(id);

        if (file is null)
            return ([], string.Empty, string.Empty);

        var path = Path.Combine(_filePath, file.StoredFileName);

        MemoryStream memoryStream = new();
        using FileStream fileStream = new(path, FileMode.Open);
        fileStream.CopyTo(memoryStream);

        memoryStream.Position = 0;

        return (memoryStream.ToArray(), file.ContentType, file.FileName);
    }
    public async Task<(FileStream? stream, string contentType, string fileName)> StreamAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var file = await _context.Files.FindAsync(id);

        if (file is null)
            return (null, string.Empty, string.Empty);

        var path = Path.Combine(_filePath, file.StoredFileName);

        var fileStream = File.OpenRead(path);

        return (fileStream, file.ContentType, file.FileName);
    }

    private async Task<UplodedFile> SaveFile(IFormFile file, CancellationToken cancellationToken = default)
    {
        var randomFileName = Path.GetRandomFileName();

        var uploadedFile = new UplodedFile
        {
            FileName = file.FileName,
            ContentType = file.ContentType,
            StoredFileName = randomFileName,
            FileExtension = Path.GetExtension(file.FileName),
        };

        var path = Path.Combine(_filePath, randomFileName);

        using var stream = File.Create(path);
        await file.CopyToAsync(stream, cancellationToken);

        return uploadedFile;
    }
}
