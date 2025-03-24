namespace FileManger.Api.Entities;

public sealed class UplodedFile
{
    public Guid Id  { get; set; } = Guid.CreateVersion7();
    public string FileName { get; set; } = string.Empty;
    public string StoredFileName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public string FileExtension { get; set; } = string.Empty;
}
