using System;

public class PageBookManager
{
    private readonly PageBook[] _pages;

    private readonly int _pageCount;
    private int _currentPageIndex;

    public int GetPageCount => _pageCount;

    public int CurrentPageIndex => _currentPageIndex;

    public PageBookManager(PageBook[] pages)
    {
        _pages = pages;
        _pageCount = pages.Length / 2;
        _currentPageIndex = 0;
    }

    public void UpdatePageIndex(NoteInWorldTrigger trigger)
    {
        switch (trigger)
        {
            case NoteInWorldPasswordTV:
            case NoteInWorldPasswordWorkAccount:
            case NoteInWorld2:
                _currentPageIndex = 0;
                break;

            case NoteInWorld3:
            case NoteInWorld4:
            case NoteInWorld4_5:
                _currentPageIndex = 1;
                break;

            case NoteInWorld5:
            case NoteInWorld6:
            case NoteInWorld7:
                _currentPageIndex = 2;
                break;
        }
    }

    public void UpdatePageIndex(ButtonClickInformer button)
    {
        switch (button)
        {
            case ArrowLeft _:
                _currentPageIndex--;
                break;

            case ArrowRight _:
                _currentPageIndex++;
                break;

            default:
                throw new ArgumentOutOfRangeException(button.name);
        }
    }

    public void ShowCurrentPage()
    {
        switch (_currentPageIndex)
        {
            case 0:
                ShowPage<FirstCouplePages>();
                break;

            case 1:
                ShowPage<SecondCouplePages>();
                break;

            case 2:
                ShowPage<ThirdCouplePages>();
                break;

            default:
                throw new ArgumentOutOfRangeException($"Page index {_currentPageIndex} is out of range.");
        }
    }

    private void ShowPage<T>() where T : PageBook
    {
        foreach (PageBook page in _pages)
        {
            if (page is T _)
                page.ShowObject();
            else
                page.HideObject();
        }
    }
}