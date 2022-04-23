using ConsoleApp1.MediaEntities;

namespace ConsoleApp1.ConsoleMenus.Multi_purpose;

public class ItemIndexTracker<T>
{
 
    public List<T> Items { get; }
    public int CurrentPage;
    public int TotalPages => (int) Math.Ceiling(Items.Count/(double) PageLength);
    private const int PageLength = 5;

    public int GetGlobalIndex(int localIndex)
    {
        return CurrentPage * PageLength + localIndex;
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

    public ItemIndexTracker(List<T> items)
    {
        Items = items;
    }

}