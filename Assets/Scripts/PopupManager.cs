using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour
{
    public float duration;
    // Start is called before the first frame update
    void Start()
    {
        Image panelImage = gameObject.GetComponent<Image>();
        Color panelColor = panelImage.color;
        panelColor.a = 0.5f;
        panelImage.DOColor(panelColor, 2f);

        gameObject.transform.FindChild("Screen").transform.DOScale(new Vector3(1, 1, 1), duration).SetEase(Ease.InOutElastic);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
