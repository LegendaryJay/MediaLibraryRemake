namespace ConsoleApp1.FileAccessor;

public sealed class FileIoSingleton
{
    private static readonly Lazy<FileIoSingleton> Lazy = new(() => new FileIoSingleton(new DatabaseIo()));

    public static FileIoSingleton Instance => Lazy.Value;

    public IFileIo FileIo { get; init; }

    private FileIoSingleton(IFileIo fileIo)
    {
        FileIo = fileIo;
    }
}