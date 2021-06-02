using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour
{
    public void callPopup()
    {
        gameObject.transform.localScale = new Vector3(1, 1, 1);

        Image panelImage = gameObject.GetComponent<Image>();
        Color panelColor = panelImage.color;
        panelColor.a = 0.5f;
        panelImage.DOColor(panelColor, 2f);

        gameObject.transform.Find("Screen").transform.DOScale(new Vector3(1, 1, 1), 2f).SetEase(Ease.InOutElastic);
    }
}
