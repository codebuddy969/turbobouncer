using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PieMenuController : MonoBehaviour
{
    public GameObject pieMenuAction;
    public Transform pieMenuObject;
    public float duration;
    private bool pieMenuOpened = false;

    public void showPieMenu()
    {
        Button actionButton = pieMenuAction.GetComponent<Button>();

        if (!pieMenuOpened)
        {
            actionButton.interactable = false;
            pieMenuObject.transform.DOScale(new Vector3(1, 1, 1), duration).SetEase(Ease.InOutSine);
            pieMenuObject.transform.DORotate(new Vector3(0, 0, 720), duration, RotateMode.WorldAxisAdd).SetEase(Ease.InOutSine).OnComplete(() => {
                pieMenuOpened = true;
                actionButton.interactable = true;
            });

        } else {

            actionButton.interactable = false;
            pieMenuObject.transform.DOScale(new Vector3(0, 0, 0), duration).SetEase(Ease.InOutSine);
            pieMenuObject.transform.DORotate(new Vector3(0, 0, -720), duration, RotateMode.WorldAxisAdd).SetEase(Ease.InOutSine).OnComplete(() => {
                pieMenuOpened = false;
                actionButton.interactable = true;
            });

        }

    }

    public void loadMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
