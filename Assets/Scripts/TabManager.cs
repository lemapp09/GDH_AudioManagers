using UnityEngine;

public class TabManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] tabs;

    public void OpenTab1()
    {
        if (tabs[0] != null)
        {
            CloseAllTabs();
            tabs[0].SetActive(true);
        }
    }

    public void OpenTab2()
    {
        if (tabs[1] != null)
        {
            CloseAllTabs();
            tabs[1].SetActive(true);
        }
    }

    public void OpenTab3()
    {
        if (tabs[2] != null)
        {
            CloseAllTabs();
            tabs[2].SetActive(true);
        }
    }

    public void OpenTab4()
    {
        if (tabs[3] != null)
        {
            CloseAllTabs();
            tabs[3].SetActive(true);
        }
    }

    public void OpenTab5()
    {
        if (tabs[4] != null)
        {
            CloseAllTabs();
            tabs[4].SetActive(true);
        }
    }

    public void OpenTab6()
    {
        if (tabs[5] != null)
        {
            CloseAllTabs();
            tabs[5].SetActive(true);
        }
    }
    
    private void CloseAllTabs()
    {
        foreach (GameObject tab in tabs)
        {
            tab.SetActive(false);
        }
    }
}
