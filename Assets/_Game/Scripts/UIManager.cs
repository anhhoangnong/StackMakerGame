using StackMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public GameObject UImenu;
    public GameObject UIfinish;

    public void OpenMenuUI()
    {
        UImenu.SetActive(true);
        UIfinish.SetActive(false);
    }    

    public void OpenFinishUI()
    {
        UImenu.SetActive(false);
        UIfinish.SetActive(true);
    }

    public void PlayButton()
    {
        UImenu.SetActive(false);
        LevelManager.Instance.OnStart();
    }

    public void RetryButton()
    {
        LevelManager.Instance.RestartLevel();
        GameManager.Instance.ChangeState(GameState.Menu);
        OpenMenuUI();
    }   
    
    public void NextButton()
    {
        LevelManager.Instance.NextLevel();
        GameManager.Instance.ChangeState(GameState.Menu);
        OpenMenuUI();
    }
}
