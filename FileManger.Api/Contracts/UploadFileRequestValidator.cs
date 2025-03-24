namespace FileManger.Api.Contracts;

public class UploadFileRequestValidator : AbstractValidator<UploadFileRequest>
{
    public UploadFileRequestValidator()
    {
        RuleFor(x => x.File)
            .SetValidator(new FileNameValidator())
            .SetValidator(new FileSizeValidator())
            .SetValidator(new BlockedSignaturesValidator());
    }
}
