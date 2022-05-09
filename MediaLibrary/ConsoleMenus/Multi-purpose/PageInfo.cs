namespace ConsoleApp1.ConsoleMenus.Multi_purpose;

public class PageInfo<T>
{
    private int _pageIndex;

    public int PageIndex => _pageIndex - GetTotalPageCount * (int) Math.Floor(_pageIndex / (double) GetTotalPageCount);

    public void ResetPage() => _pageIndex = 0;
    public void NextPage() => _pageIndex++;
    public void PreviousPage() => _pageIndex--;
    public void SetPage(int input) => _pageIndex = input;

    public int PageLength { get; set; }
    public int GetTotalPageCount
    {
        get
        {
            if (TotalItemCount == 0) return 1;
            return (int) Math.Ceiling((double) TotalItemCount / PageLength);
        }
    }

    public List<T>? Items { get; set; }
    public int TotalItemCount { get; set; }

    public PageInfo(int pageIndex, int pageLength)
    {
        PageLength = pageLength;
        _pageIndex = pageIndex;
    }
    
    
}