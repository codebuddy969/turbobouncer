using UnityEngine;

public class PlatformsManager : MonoBehaviour
{
    private CommonConfig config = new CommonConfig();
    private GameDataConfig game_config;

    private int skinSelection;
    private int platformsCount;
    public int interval = 6;
    public GameObject ball;
    private Vector3 lastPlatformPosition = new Vector3(0, 3.0f, 0);
    private Vector3 primitivePosition;

    public GameObject soil;
    public Sprite[] soilSprites;
    public GameObject[] platforms;

    public GameObject fire;
    public GameObject heart;
    public GameObject thorns;
    public GameObject bomb;
    public GameObject energy;
    public GameObject score;
    public GameObject win;
    void Start()
    {
        game_config = DBOperationsController.element.LoadSaving();

        skinSelection = Random.Range(0, 2);

        for (int i = 0; i <= config.platformsCount + game_config.level; i += 1)
        {
            generatePlatforms(i);
        }

        soil.GetComponent<SpriteRenderer>().sprite = soilSprites[skinSelection];
    }

    void generatePlatforms(int index)
    {
        float randomPosition;
        float lastPlatformSign = Mathf.Sign(lastPlatformPosition.x);

        if (lastPlatformSign == 1)
        {

            randomPosition = Random.Range(-lastPlatformPosition.x, -6.0f);

            if ((lastPlatformPosition.x - randomPosition) < 2)
            {
                randomPosition = randomPosition + 2;
            }
        }
        else
        {
            randomPosition = Random.Range(lastPlatformPosition.x, 6.0f);
        }

        primitivePosition = new Vector3(
            randomPosition,
            lastPlatformPosition.y + 6,
            lastPlatformPosition.z
        );

        createPlatform(primitivePosition, index);

        platformsCount -= platformsCount;

        lastPlatformPosition = primitivePosition;
    }

    void createPlatform(Vector3 primitivePosition, int index)
    {
        GameObject platform = new GameObject();
        BoxCollider collider = platform.AddComponent<BoxCollider>();
        Rigidbody rigidBody = platform.AddComponent<Rigidbody>();
        SpriteRenderer renderer = platform.AddComponent<SpriteRenderer>();

        int randomIndex = Random.Range(0, 4);

        collider.size = new Vector3(1.8f, 1.2f, 1.0f);

        renderer.sprite = platforms[skinSelection].transform.GetChild(randomIndex).GetComponent<SpriteRenderer>().sprite;

        platform.name = "platform-" + index;
        platform.transform.position = primitivePosition;
        platform.transform.localScale = new Vector3(1.6f, 1.2f, 1);

        rigidBody.mass = 1000;
        rigidBody.useGravity = false;
        rigidBody.constraints = RigidbodyConstraints.FreezePosition;
        rigidBody.constraints = RigidbodyConstraints.FreezeRotation;

        createEntity(rigidBody.transform.position, platform.transform.localScale, index);
    }

    void createEntity(Vector3 position, Vector3 scale, int platformIndex)
    {
        int index = Random.Range(0, 6);
        float dividedPosition = scale.x / 2;
        bool lastPlatform = (config.platformsCount + game_config.level) == platformIndex;
        float randomHorizontalPosition = Random.Range(-dividedPosition, dividedPosition);

        GameObject[] enemy = new GameObject[] { fire, thorns, bomb, energy, heart, score };

        GameObject objectModel = Instantiate(lastPlatform ? win : enemy[index]) as GameObject;

        GameObject cube = new GameObject();

        objectModel.transform.localPosition = lastPlatform ? new Vector3(0, 0.45f, 0) : Vector3.zero;
        objectModel.transform.parent = cube.transform;

        cube.transform.position = new Vector3(
            position.x + randomHorizontalPosition,
            position.y + 1.0f,
            position.z
        );

        //cube.AddComponent<BoxCollider>();

        //string[] entities = new string[] { "fireEnemy", "panzerEnemy", "bombEnemy", "powerBoost", "healthBoost", "scoreBoost" };

        string[] entities = new string[] { "healthBoost", "healthBoost", "healthBoost", "healthBoost", "healthBoost", "healthBoost" };

        cube.name = "gameEntity";

        cube.transform.GetChild(0).name = entities[index];
    }
}
