namespace FileManger.Api.Persistence.Configuration;

public class UplodedFileConfiguration : IEntityTypeConfiguration<UplodedFile>
{
    public void Configure(EntityTypeBuilder<UplodedFile> builder)
    {
        builder
            .Property(f => f.FileName)
            .HasMaxLength(250);
        builder
            .Property(f => f.StoredFileName)
            .HasMaxLength(250);
        builder
            .Property(f => f.ContentType)
            .HasMaxLength(50);
        builder
            .Property(f => f.FileExtension)
            .HasMaxLength(10);

    }
}
