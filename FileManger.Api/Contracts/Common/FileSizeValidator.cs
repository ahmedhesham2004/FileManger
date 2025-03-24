using FileManger.Api.Settings;

namespace FileManger.Api.Contracts.Common;

public class FileSizeValidator : AbstractValidator<IFormFile>
{
    public FileSizeValidator()
    {
        RuleFor(x => x)
            .Must(ValidateFileSize)
            .WithMessage($"Max file size is {FileSettings.MaxFileSizeInMB} MB.")
            .When(x => x is not null);
    }
    private static bool ValidateFileSize(IFormFile file) =>
        file.Length <= FileSettings.MaxFileSizeInBytes;

}
