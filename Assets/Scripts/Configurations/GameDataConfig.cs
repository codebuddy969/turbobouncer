
[System.Serializable]
public class GameDataConfig
{
    public int score = 100;
    public int level = 0;
    public float lastScore = 0;
    public float musicLevel = 0.7f;
    public float effectsLevel = 1.0f;

    private int firefigherCount = 5;
    private int timeBoostCount = 1;
    private int healthBoostCount = 2;
    private int turboJumperCount = 3;

    public int FirefigherCount { get => firefigherCount; set => firefigherCount = value; }
    public int TimeboostCount { get => timeBoostCount; set => timeBoostCount = value; }
    public int HealthBoostCount { get => healthBoostCount; set => healthBoostCount = value; }
    public int TurboJumperCount { get => turboJumperCount; set => turboJumperCount = value; }
}
