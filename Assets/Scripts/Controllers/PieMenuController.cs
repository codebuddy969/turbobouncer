using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;

public class PieMenuController : MonoBehaviour
{
    public GameObject pieMenuAction;
    public Transform pieMenuObject;
    public float duration;
    private bool pieMenuOpened = false;

    public void Start()
    {
        EventsManager.current.onHidePieMenu += hidePieMenu;
    }

    public void showPieMenu()
    {
        Button actionButton = pieMenuAction.GetComponent<Button>();

        if (!pieMenuOpened)
        {
            pieMenuObject.transform.rotation = Quaternion.identity;

            actionButton.interactable = false;
            pieMenuObject.transform.DOScale(new Vector3(1, 1, 1), duration).SetEase(Ease.InOutSine);
            pieMenuObject.transform.DORotate(new Vector3(0, 0, 720), duration, RotateMode.WorldAxisAdd).SetEase(Ease.InOutSine).OnComplete(() => {
                pieMenuOpened = true;
                actionButton.interactable = true;
                DataStoreManager.store.pieMenuOpenedStatus = true;
            });
        } else {
            actionButton.interactable = false;
            pieMenuObject.transform.DOScale(new Vector3(0, 0, 0), duration).SetEase(Ease.InOutSine);
            pieMenuObject.transform.DORotate(new Vector3(0, 0, -720), duration, RotateMode.WorldAxisAdd).SetEase(Ease.InOutSine).OnComplete(() => {
                pieMenuOpened = false;
                actionButton.interactable = true;
                DataStoreManager.store.pieMenuOpenedStatus = false;
            });
        }

    }

    public void hidePieMenu()
    {
        Button actionButton = pieMenuAction.GetComponent<Button>();

        actionButton.interactable = false;
        pieMenuObject.transform.DOScale(new Vector3(0, 0, 0), duration).SetEase(Ease.InOutSine);
        pieMenuObject.transform.DORotate(new Vector3(0, 0, -720), duration, RotateMode.WorldAxisAdd).SetEase(Ease.InOutSine).OnComplete(() => {
            pieMenuOpened = false;
            actionButton.interactable = true;
            DataStoreManager.store.pieMenuOpenedStatus = false;
        });
    }

    public void loadQuitPopup()
    {
        hidePieMenu();

        Hashtable parameters = new Hashtable();

        parameters["quitButton"] = true;
        parameters["closeButton"] = true;
        parameters["optionsButtons"] = false;
        parameters["message"] = "Are you sure you want to quit ?";

        EventsManager.current.popupAction(parameters, () => {});
    }
}
