using FileManger.Api.Settings;

namespace FileManger.Api.Contracts.Common;

public class BlockedSignaturesValidator : AbstractValidator<IFormFile>
{
    public BlockedSignaturesValidator()
    {
        RuleFor(x => x)
          .Must(ValidateFileContent)
          .WithMessage("Not allowed file content")
          .When(x => x is not null);

    }
    private static bool ValidateFileContent(IFormFile file)
    {
        BinaryReader binary = new(file.OpenReadStream());
        var bytes = binary.ReadBytes(2); // السجنيتشر بتاع الفيل بيبقي موجود في اول تو بايت للفايل كل اكستينشن ليه سجنيتشر معينة

        var fileSequenceHex = BitConverter.ToString(bytes);

        //return !FileSettings.BlockedSignatures.Contains(fileSequenceHex.ToUpper());

        foreach (var signature in FileSettings.BlockedSignatures)
            if (signature.Equals(fileSequenceHex, StringComparison.OrdinalIgnoreCase))
                return false;

        return true;
    }
}
