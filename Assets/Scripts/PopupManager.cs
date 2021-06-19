using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PopupManager : MonoBehaviour
{
    public void Start()
    {
        EventsManager.current.onPopupAction += callPopup;
    }

    public void callPopup(Hashtable parameters, Action callback) 
    {
        gameObject.transform.Find("QuestionText").GetComponent<TextMeshProUGUI>().text = (string)parameters["message"];

        gameObject.transform.Find("Close").gameObject.SetActive((bool)parameters["closeButton"]);
        gameObject.transform.Find("Quit").gameObject.SetActive((bool)parameters["quitButton"]);
        gameObject.transform.Find("Options").gameObject.SetActive((bool)parameters["optionsButtons"]);

        gameObject.transform.DOScale(new Vector3(1, 1, 1), parameters["time"] != null ? (int)parameters["time"] : 2).SetEase(Ease.InOutElastic).OnComplete(() => { callback?.Invoke(); });
    }

    public void closePopup()
    {
        gameObject.transform.DOScale(new Vector3(0, 0, 0), 0.2f);
    }

    public void loadMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void restartGame()
    {
        SceneManager.LoadScene("Game");
    }
}
