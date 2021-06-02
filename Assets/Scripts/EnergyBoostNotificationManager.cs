using DG.Tweening;
using UnityEngine;

public class EnergyBoostNotificationManager : MonoBehaviour
{
    public void Start()
    {
        EventsManager.current.onTurboJumpNotificationShow += showNotification; 
    }
    public void showNotification()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(transform.DOScale(new Vector3(1, 1, 1), 1f).SetEase(Ease.InOutElastic));
        sequence.AppendInterval(2f);
        sequence.Append(transform.DOScale(new Vector3(0, 0, 0), 0.2f).SetEase(Ease.OutQuad));
    }

    private void OnDestroy()
    {
        EventsManager.current.onTurboJumpNotificationShow -= showNotification;
    }
}
