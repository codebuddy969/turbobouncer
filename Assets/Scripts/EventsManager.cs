using System;
using UnityEngine;
using System.Collections;
public class EventsManager : MonoBehaviour
{
    public static EventsManager current;

    private void Awake()
    {
        current = this;
    }

    //----------------------------------------------------

    public event Action onTurboJumpNotificationShow;
    public void turboJumpNotificationShow()
    {
        if (onTurboJumpNotificationShow != null)
        {
            onTurboJumpNotificationShow();
        }
    }

    //----------------------------------------------------

    public event Action onJumpMultiplierChange;
    public void jumpMultiplierChange()
    {
        if (onJumpMultiplierChange != null)
        {
            onJumpMultiplierChange();
        }
    }

    //----------------------------------------------------

    public event Action onIgnitePlayer;
    public void ignitePlayer()
    {
        if (onIgnitePlayer != null)
        {
            onIgnitePlayer();
        }
    }

    //----------------------------------------------------

    public event Action<Hashtable, Action> onPopupAction;
    public void popupAction(Hashtable parameters, Action callback)
    {
        if (onPopupAction != null)
        {
            onPopupAction(parameters, callback);
        }
    }
}
