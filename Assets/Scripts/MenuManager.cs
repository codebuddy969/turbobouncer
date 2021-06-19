using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    GameDataConfig config;

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
    }

    public void showShopMenuScreen()
    {
        homeMenuScreen.gameObject.SetActive(false);
        optionsMenuScreen.gameObject.SetActive(false);
        shopMenuScreen.gameObject.SetActive(true);
    }

    public void musicValueChange(Slider slider)
    {
        config.musicLevel = slider.value;

        DBOperationsController.element.CreateSaving(config);
    }

    public void fxValueChange(Slider slider)
    {
        config.effectsLevel = slider.value;

        DBOperationsController.element.CreateSaving(config);
    }
}
