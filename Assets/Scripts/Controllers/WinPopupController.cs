using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class WinPopupController : MonoBehaviour
{
    public void Start()
    {
        EventsManager.current.onWinPopupAction += callPopup;
    }

    public void callPopup(Hashtable parameters, Action callback)
    {
        gameObject.transform.Find("Headline").GetComponent<TextMeshProUGUI>().text = "Level " + ((int)parameters["level"]).ToString();

        gameObject.transform.Find("Points").GetComponent<TextMeshProUGUI>().text = ((int)parameters["count"]).ToString();

        gameObject.transform.DOScale(new Vector3(1, 1, 1), 2f).SetEase(Ease.InOutElastic).OnComplete(() => { callback?.Invoke(); });
    }

    public void loadMainMenu()
    {
        GlobalVariables.Set("openShopMenu", true);
        SceneManager.LoadScene("Menu");
    }

    public void restartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void continueGame()
    {
        SceneManager.LoadScene("Game");
    }
}
