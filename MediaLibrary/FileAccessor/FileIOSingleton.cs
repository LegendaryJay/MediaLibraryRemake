namespace ConsoleApp1.FileAccessor;

public sealed class FileIoSingleton
{
    private static readonly Lazy<FileIoSingleton> Lazy = new(() => new FileIoSingleton(new DatabaseIo()));

    private FileIoSingleton(IFileIo fileIo)
    {
        FileIo = fileIo;
    }

    public static FileIoSingleton Instance => Lazy.Value;

    public IFileIo FileIo { get; init; }
}