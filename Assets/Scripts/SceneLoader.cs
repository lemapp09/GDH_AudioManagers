using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    
    public void LoadPart1()
    {
        SceneManager.LoadScene(1);
    }
    
    public void LoadPart2()
    {
        SceneManager.LoadScene(2);
    }
    
    public void LoadPart3()
    {
        SceneManager.LoadScene(3);
    }
    
    public void LoadPart4()
    {
        SceneManager.LoadScene(4);
    }
}
