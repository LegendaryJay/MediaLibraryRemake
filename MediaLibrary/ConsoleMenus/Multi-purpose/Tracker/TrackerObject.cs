namespace ConsoleApp1.ConsoleMenus.Multi_purpose;

public class TrackerObject<T>
{
    public int LocalIndex { get; }
    public int GlobalIndex{ get; }
    public T Item{ get; }
    public bool isValid{ get; }

    public TrackerObject(int localIndex, int globalIndex, T item, bool isValid)
    {
        LocalIndex = localIndex;
        GlobalIndex = globalIndex;
        Item = item;
        this.isValid = isValid;
    }
}