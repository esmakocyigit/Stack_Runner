using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{

    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] int levelCurrent;
    private int levelDefault;


    private void Start()
    {
        levelDefault = levelCurrent;
    }

    private void Update()
    {
        if (PlayerController.Instance.forwardSpeed == 0)
        {
            WinGameUI();
           
        }
        else
        {
            winPanel.SetActive(false);
        }


    }

    #region Other Methods


    private void WinGameUI()
    {

        gamePanel.transform.DOScale(Vector3.zero, 1f);
       

        StartCoroutine(WinScreen());
    }



    public void RestartButton()
    {
        levelCurrent -= 1;
        gamePanel.transform.DOScale(Vector3.one, .1f);

    
        GameManager.Instance.isStart = true;
        PlayerController.Instance.Game();


        if (levelCurrent == 0)
        {
            levelCurrent = levelDefault;
            SceneManager.LoadScene(0);
        }

    }

    IEnumerator WinScreen()
    {
        winPanel.SetActive(true);
   
        yield return new WaitForSeconds(1f);
       // winPanel.SetActive(true);
    }

 


    #endregion
}
