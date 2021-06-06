using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PopupManager : MonoBehaviour
{
    public void Start()
    {
        EventsManager.current.onPopupAction += callPopup;
    }

    public void callPopup()
    {
        gameObject.transform.DOScale(new Vector3(1, 1, 1), 2f).SetEase(Ease.InOutElastic);
    }

    public void closePopup()
    {
        gameObject.transform.DOScale(new Vector3(0, 0, 0), 0.2f);
    }

    public void loadMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
