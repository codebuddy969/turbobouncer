using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Facebook.Unity;
using System.Collections;

public class FacebookManager : MonoBehaviour
{
    public GameObject loginPopup;

    private AudioManager audioManager;

    private void Awake()
    {
        if (!FB.IsInitialized)
        {
            FB.Init( () => {
                if (FB.IsInitialized) {
                    FB.ActivateApp();
                } else {
                    Debug.LogError("Couldn't initialize!");
                }
            }, isGameShown => {
                if (!isGameShown)
                {
                    Time.timeScale = 0;
                } else
                {
                    Time.timeScale = 1;
                }
            } );
        } else
        {
            FB.ActivateApp();
        }
    }

    public void Start()
    {
        audioManager = AudioManager.instance;

        EventsManager.current.onShowFacebookPopup += callLoginPopup;
    }

    public void FacebookLogin()
    {
        var permissions = new List<string>() {"public_profile", "email"};
        FB.LogInWithReadPermissions(permissions, AuthCallback);
    }

    public void FacebookLogOut()
    {
        FB.LogOut();
    }

    public void FacebookShare()
    {
        FB.FeedShare(
            string.Empty,
            new System.Uri("https://play.google.com/store/apps/details?id=com.king.candycrushjellysaga"),
            "PollyCube",
            "LinkCaption",
            "LinkDescription",
            new System.Uri("https://stimg.cardekho.com/images/carexteriorimages/930x620/Renault/Kiger/8361/1615378654259/front-left-side-47.jpg?tr=w-880,h-495"),
            string.Empty
        );

        //FB.ShareLink(null, "Check this out!", "That's a testing post", null);
    }

    public void callLoginPopup()
    {
        if (!FB.IsLoggedIn)
        {
            StartCoroutine(showLoginPopup(1f));
        }
    }

    private IEnumerator showLoginPopup(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        audioManager.Play("coin");
        loginPopup.SetActive(true);
        loginPopup.transform.Find("Popup").DOScale(new Vector3(1, 1, 1), 1).SetEase(Ease.InOutElastic).OnComplete(() => { Time.timeScale = 0; });
    }

    public void closeLoginPopup()
    {
        Time.timeScale = 1;
        loginPopup.SetActive(false);
        loginPopup.transform.Find("Popup").DOScale(new Vector3(0, 0, 0), 0.2f);
    }

    private void AuthCallback(ILoginResult result)
    {
        closeLoginPopup();

        Debug.LogErrorFormat("Error in Facebook login {0}", result.Error);
    }
}
