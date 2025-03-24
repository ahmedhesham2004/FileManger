namespace FileManger.Api.Contracts;

public class UploadManyFileRequestValidator : AbstractValidator<UploadManyFileRequest>
{
    public UploadManyFileRequestValidator()
    {
        RuleForEach(x => x.Files)
            .SetValidator(new FileNameValidator())
            .SetValidator(new FileSizeValidator())
            .SetValidator(new BlockedSignaturesValidator());
    }
}
