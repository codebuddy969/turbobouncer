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

    public event Action<bool> onIgnitePlayer;
    public void ignitePlayer(bool status)
    {
        if (onIgnitePlayer != null)
        {
            onIgnitePlayer(status);
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

    //----------------------------------------------------

    public event Action<Hashtable, Action> onWinPopupAction;
    public void winPopupAction(Hashtable parameters, Action callback)
    {
        if (onWinPopupAction != null)
        {
            onWinPopupAction(parameters, callback);
        }
    }

    //----------------------------------------------------

    public event Action<string> onPieOptionClicked;
    public void pieOptionClicked(string name)
    {
        if (onPieOptionClicked != null)
        {
            onPieOptionClicked(name);
        }
    }

    //----------------------------------------------------

    public event Action<int> onTimeBoostAction;
    public void timeBoostAction(int count)
    {
        if (onTimeBoostAction != null)
        {
            onTimeBoostAction(count);
        }
    }

    //----------------------------------------------------

    public event Action onHidePieMenu;
    public void hidePieMenu()
    {
        if (onHidePieMenu != null)
        {
            onHidePieMenu();
        }
    }
}
