using FileManger.Api.Settings;

namespace FileManger.Api.Contracts;

public class UploadImageRequestValidator : AbstractValidator<UploadImageRequest>
{
    public UploadImageRequestValidator()
    {
        RuleFor(x => x.Image)
            .SetValidator(new FileSizeValidator())
            .SetValidator(new BlockedSignaturesValidator()); 

        RuleFor(x => x.Image)
            .Must(ValidImageExtension)
            .WithMessage("Image extension is not allowed")
            .When(x => x.Image is not null);
    }
    private static bool ValidImageExtension(IFormFile image) =>
        FileSettings.AllowedImagesExtensions.Contains(Path.GetExtension(image.FileName.ToLower()));
}
