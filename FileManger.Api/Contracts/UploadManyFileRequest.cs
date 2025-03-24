namespace FileManger.Api.Contracts;

public record UploadManyFileRequest
(
    IFormFileCollection Files
);
