using FileManger.Api.Contracts;
using Microsoft.AspNetCore.Mvc;
namespace FileManger.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class FilesController(IFileService fileService) : ControllerBase
{   
    private readonly IFileService _fileService = fileService;

    [HttpPost("")]
    public async Task<IActionResult> Upload([FromForm] UploadFileRequest request, CancellationToken cancellationToken)
    {
        var fileId = await _fileService.UploadAsync(request.File, cancellationToken);
        return Ok(fileId);  
    }

    [HttpPost("")]
    public async Task<IActionResult> UploadMany([FromForm] UploadManyFileRequest request, CancellationToken cancellationToken)
    {
        var filesIds = await _fileService.UploadManyAsync(request.Files, cancellationToken);
        return Ok(filesIds);
    }

    [HttpPost("")]
    public async Task<IActionResult> UploadImage([FromForm] UploadImageRequest request, CancellationToken cancellationToken)
    {
        await _fileService.UploadImageAsync(request.Image, cancellationToken);

        return Created();
    }
}
