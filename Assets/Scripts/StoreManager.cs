using UnityEngine;
using TMPro;
using DG.Tweening;

public class StoreManager : MonoBehaviour
{
    GameDataConfig config;

    private CommonConfig common_config = new CommonConfig();

    private string options = "ScrollView/Viewport/Content/Options";

    private TextMeshProUGUI fireFighterCount;
    private TextMeshProUGUI invincibilityCount;
    private TextMeshProUGUI turbojumperCount;
    private TextMeshProUGUI timeboostCount;

    private TextMeshProUGUI fireFighterPrice;
    private TextMeshProUGUI invincibilityPrice;
    private TextMeshProUGUI turbojumperPrice;
    private TextMeshProUGUI timeboostPrice;

    private Transform score;

    void Start()
    {
        config = DBOperationsController.element.LoadSaving();

        fireFighterCount = transform.Find($"{options}/Firefighter/Count").GetComponent<TextMeshProUGUI>();
        invincibilityCount = transform.Find($"{options}/Invincibility/Count").GetComponent<TextMeshProUGUI>();
        turbojumperCount = transform.Find($"{options}/Turbojumper/Count").GetComponent<TextMeshProUGUI>();
        timeboostCount = transform.Find($"{options}/Timeboost/Count").GetComponent<TextMeshProUGUI>();

        fireFighterPrice = transform.Find($"{options}/Firefighter/Price").GetComponent<TextMeshProUGUI>();
        invincibilityPrice = transform.Find($"{options}/Invincibility/Price").GetComponent<TextMeshProUGUI>();
        turbojumperPrice = transform.Find($"{options}/Turbojumper/Price").GetComponent<TextMeshProUGUI>();
        timeboostPrice = transform.Find($"{options}/Timeboost/Price").GetComponent<TextMeshProUGUI>();

        score = transform.Find("Score/Count");

        updateItemsCount(config);
        updateItemsPrices();
        updateScore(config);
    }

    public void updateItemsCount(GameDataConfig config) {
        fireFighterCount.text = config.FirefigherCount.ToString();
        invincibilityCount.text = config.HealthBoostCount.ToString();
        turbojumperCount.text = config.TurboJumperCount.ToString();
        timeboostCount.text = config.TimeboostCount.ToString();
    }

    public void updateItemsPrices() {
        fireFighterPrice.text = common_config.fireFighterPrice.ToString();
        invincibilityPrice.text = common_config.healthBoostPrice.ToString();
        turbojumperPrice.text = common_config.turboJumperPrice.ToString();
        timeboostPrice.text = common_config.timeBoostPrice.ToString();
    }

    public void updateScore(GameDataConfig config)
    {
        score.GetComponent<TextMeshProUGUI>().text = config.score.ToString();
    }

    private void updateAfterBuy(GameDataConfig game_config)
    {
        updateItemsCount(game_config);
        updateScore(game_config);

        DBOperationsController.element.CreateSaving(game_config);
    }

    public void buy(string action)
    {
        GameDataConfig game_config = DBOperationsController.element.LoadSaving();

        switch (action)
        {
            case "firefighter":

                if (game_config.score < common_config.fireFighterPrice) 
                {
                    AudioManager.instance.Play("failed");
                    DOTween.Sequence().Append(score.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.3f)).Append(score.DOScale(new Vector3(1f, 1f, 1f), 0.3f));
                    return;
                }

                game_config.score = game_config.score - common_config.fireFighterPrice;
                game_config.FirefigherCount = game_config.FirefigherCount + 1;

                updateAfterBuy(game_config);

                break;
            case "invincibility":

                if (game_config.score < common_config.healthBoostPrice)
                {
                    AudioManager.instance.Play("failed");
                    DOTween.Sequence().Append(score.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.3f)).Append(score.DOScale(new Vector3(1f, 1f, 1f), 0.3f));
                    return;
                }

                game_config.score = game_config.score - common_config.healthBoostPrice;
                game_config.HealthBoostCount = game_config.HealthBoostCount + 1;

                updateAfterBuy(game_config);

                break;

            case "turbojumper":

                if (game_config.score < common_config.turboJumperPrice)
                {
                    AudioManager.instance.Play("failed");
                    DOTween.Sequence().Append(score.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.3f)).Append(score.DOScale(new Vector3(1f, 1f, 1f), 0.3f));
                    return;
                }

                game_config.score = game_config.score - common_config.turboJumperPrice;
                game_config.TurboJumperCount = game_config.TurboJumperCount + 1;

                updateAfterBuy(game_config);

                break;

            case "timeboost":

                if (game_config.score < common_config.timeBoostPrice)
                {
                    AudioManager.instance.Play("failed");
                    DOTween.Sequence().Append(score.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.3f)).Append(score.DOScale(new Vector3(1f, 1f, 1f), 0.3f));
                    return;
                }

                game_config.score = game_config.score - common_config.timeBoostPrice;
                game_config.TimeboostCount = game_config.TimeboostCount + 1;

                updateAfterBuy(game_config);

                break;
        }

        AudioManager.instance.Play("coin");
    }
}
