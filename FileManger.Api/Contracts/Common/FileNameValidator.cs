using System.Text.RegularExpressions;

namespace FileManger.Api.Contracts.Common;

public class FileNameValidator : AbstractValidator<IFormFile>
{
    public FileNameValidator()
    {
        RuleFor(x => x)
            .Must(ValidateFileName)
            .WithMessage("Invalid file name")
            .When(x => x is not null);
    }
    private static bool ValidateFileName(IFormFile file) =>
        Regex.IsMatch(file.FileName, @"^[A-Za-z0-9_\-. ]*$");
}
