namespace ConsoleApp1.FileAccessor;

public sealed class FileIoSingleton
{
    private static readonly Lazy<FileIoSingleton> Lazy = new(() => new FileIoSingleton(new DatabaseIo()));

    private FileIoSingleton(IFileIo fileIo)
    {
        _fileIo = fileIo;
    }

    public static IFileIo FileIo => Lazy.Value._fileIo;

    private readonly IFileIo _fileIo;
}