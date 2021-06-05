using System;
using UnityEngine;

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

    public event Action onGameDataSave;
    public void gameDataSave()
    {
        if (onGameDataSave != null)
        {
            onGameDataSave();
        }
    }

    //----------------------------------------------------

    public event Action onGameDataLoad;
    public void gameDataLoad()
    {
        if (onGameDataLoad != null)
        {
            onGameDataLoad();
        }
    }
}
