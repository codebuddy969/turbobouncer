using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    GameDataConfig config;
    public AudioManager audioManager;

    public Canvas homeMenuScreen;
    public Canvas optionsMenuScreen;
    public Canvas shopMenuScreen;

    public Slider musicSlider;
    public Slider effectsSlider;

    void Start()
    {
        checkShopMenuScreenStatus();
       
        config = DBOperationsController.element.LoadSaving();

        musicSlider.value = config.musicLevel;
        effectsSlider.value = config.effectsLevel;

        audioManager.Play("main-theme");
    }

    private void checkShopMenuScreenStatus()
    {
        bool shopMenuStatus = GlobalVariables.Get<bool>("openShopMenu");
        if (shopMenuStatus)
        {
            showShopMenuScreen();
            GlobalVariables.Set("openShopMenu", false);
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void showOptionsMenu()
    {
        homeMenuScreen.gameObject.SetActive(false);
        optionsMenuScreen.gameObject.SetActive(true);
        shopMenuScreen.gameObject.SetActive(false);

        audioManager.Play("click");
    }

    public void showShopMenuScreen()
    {
        homeMenuScreen.gameObject.SetActive(false);
        optionsMenuScreen.gameObject.SetActive(false);
        shopMenuScreen.gameObject.SetActive(true);

        audioManager.Play("click");
    }

    public void musicValueChange()
    {
        config.musicLevel = musicSlider.value;

        audioManager.MusicVolumeControl(musicSlider.value / 3);

        DBOperationsController.element.CreateSaving(config);
    }

    public void fxValueChange()
    {
        config.effectsLevel = effectsSlider.value;

        audioManager.EffectsVolumeControl(effectsSlider.value / 3);

        DBOperationsController.element.CreateSaving(config);
    }
}
