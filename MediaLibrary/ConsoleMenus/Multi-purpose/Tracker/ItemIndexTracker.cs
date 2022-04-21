namespace ConsoleApp1.ConsoleMenus.Multi_purpose;

public class ItemIndexTracker<T>
{
    public List<T> Items { get; }
    public int CurrentPage;
    public int TotalPages => (int) Math.Ceiling(Items.Count/(double) _pageLength);
    private readonly int _pageLength;


    public int GetGlobalIndex(int localIndex)
    {
        return CurrentPage * _pageLength + localIndex;
    }
    public T? GetItem(int localIndex)
    {
        return IsInBounds(localIndex) ? Items[GetGlobalIndex(localIndex)] : default;
    }

    public TrackerObject<T?> GetTrackerObject(int localIndex)
    {
        return new TrackerObject<T?>(localIndex, GetGlobalIndex(localIndex), GetItem(localIndex), IsInBounds(localIndex));
    }
    
    public void Next()
    {
        ChangePage(1);
    }
    public void Previous()
    {
        ChangePage(-1);
    }

    public bool IsInBounds(int localIndex)
    {
        return GetGlobalIndex(localIndex) < Items.Count;
    }
    private void ChangePage(int direction)
    {
        CurrentPage = (CurrentPage + direction + TotalPages) % TotalPages;
    }

    public ItemIndexTracker(List<T> items, int pageLength)
    {
        Items = items;
        _pageLength = pageLength;
    }

}