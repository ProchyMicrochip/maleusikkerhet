namespace måleusikkerhet.Database;

public abstract class ImageDb
{
    public int? Id { get; set; }
    public byte[]? ImageData { get; set; }
}