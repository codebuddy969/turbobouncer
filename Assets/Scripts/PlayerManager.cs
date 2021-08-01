using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerManager : MonoBehaviour
{
    public JoystickManager joystick;

    public GameObject firePrefab;
    public GameObject energySlider;
    public GameObject healthSlider;
    public GameObject energyExplosionPrefab;

    public GameObject jumpButton;

    private Rigidbody playerBody;

    private Image healthBar;
    private Image energyBar;

    private AudioManager audioManager;

    private float memorySize = 0;
    private int jumpMultiplier = 15;

    private bool isDead = false;
    private bool isIgnited = false;
    private bool isGrounded = false;

    void Start()
    {
        Time.timeScale = 1;

        playerBody = gameObject.GetComponent<Rigidbody>();

        energyBar = energySlider.gameObject.transform.GetChild(0).GetComponent<Image>();
        healthBar = healthSlider.gameObject.transform.GetChild(0).GetComponent<Image>();

        memorySize = SystemInfo.systemMemorySize;

        audioManager = AudioManager.instance;

        audioManager.PlayRandomTrack();

        EventsManager.current.showFacebookPopup();

        EventsManager.current.onJumpMultiplierChange += changeJumpMultiplierValue;
        EventsManager.current.onIgnitePlayer += ignitePlayer;
        EventsManager.current.onPieOptionClicked += pieOptionClicked;
    }

    void Update()
    {
        if ((joystick.Horizontal >= .1f) && !isGrounded)
        {
            playerBody.AddForce(Vector3.right * (joystick.Horizontal / 2), ForceMode.VelocityChange);
        }

        if ((joystick.Horizontal <= -.1f) && !isGrounded)
        {
            playerBody.AddForce(Vector3.left * -(joystick.Horizontal / 2), ForceMode.VelocityChange);
        }

        if (Input.GetKeyDown("space")) jumpForceIncrease();

        if (isIgnited) healthBar.fillAmount -= 0.0001f;

        energyBar.fillAmount += memorySize < 8000 ? 0.018f : 0.004f;

        if ((transform.position.y < -1) || (healthBar.fillAmount == 0 && !isDead))
        {
            playerFailed();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        playerBody.transform.position = new Vector3(playerBody.transform.position.x, playerBody.transform.position.y, 0);

        environmentCollisions(collision);
    }

    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

    private void playerFailed()
    {
        isDead = true;

        gameObject.transform.DetachChildren();

        Hashtable parameters = new Hashtable();

        parameters["time"] = 1;
        parameters["quitButton"] = false;
        parameters["closeButton"] = false;
        parameters["optionsButtons"] = true;
        parameters["message"] = "Sorry, but you lost, want to try again ?";

        EventsManager.current.popupAction(parameters, () => { Time.timeScale = 0; });

        Destroy(gameObject);
    }

    private void environmentCollisions(Collision collision)
    {
        if (collision.collider.name.StartsWith("platform")) {

            int index = System.Convert.ToInt32(collision.collider.name.Substring(collision.collider.name.Length - 1));

            GameObject current = GameObject.Find("platform-" + index);
            GameObject next = GameObject.Find("platform-" + (index + 1));

            if (next)
            {
                float angle = Vector2.Angle(current.transform.position, next.transform.position);

                jumpButton.transform.localRotation = Quaternion.Euler(0, 0, current.transform.position.x < next.transform.position.x ? -angle : +angle);
            }
        }

        if (collision.collider.name.StartsWith("platform") || collision.collider.name == "soil-collider")
        {
            playerBody.AddForce(Vector3.up * 4, ForceMode.VelocityChange);
        }

        if (collision.collider.name != "left-limiter" && collision.collider.name != "right-limiter")
        {
            isGrounded = true;
            playerBody.AddForce(Vector3.right * 0);
            playerBody.AddForce(Vector3.left * 0);
            playerBody.velocity = Vector3.zero;
            playerBody.angularVelocity = Vector3.zero;
        }

        audioManager.Play("ball-hit");
    }

    public void changeJumpMultiplierValue()
    {
        jumpMultiplier = 25;
    }

    public void jumpForceIncrease()
    {
        if (energyBar.fillAmount >= 1)
        {
            var prefab = Instantiate(energyExplosionPrefab, new Vector3(
                playerBody.transform.position.x,
                playerBody.transform.position.y,
                playerBody.transform.position.z - 1
            ), Quaternion.identity);

            energyBar.fillAmount = 0;

            playerBody.AddForce(Vector3.up * jumpMultiplier, ForceMode.VelocityChange);

            jumpMultiplier = 12;

            audioManager.Play("jump-boost");

            Object.Destroy(prefab, 3.0f);
        }
    }

    public void ignitePlayer(bool status)
    {
        ParticleSystem fire = gameObject.transform.Find("Fire").GetComponent<ParticleSystem>();

        var emission = fire.emission;

        emission.enabled = status;

        isIgnited = status;

        if (!status)
        {
            audioManager.Stop("fire");
            audioManager.Play("fire-fade");
        }
    }

    public void pieOptionClicked(string name)
    {
        GameDataConfig game_config = DBOperationsController.element.LoadSaving();

        switch (name)
        {
            case "firefighter":
                if (isIgnited && game_config.FirefigherCount > 0)
                {
                    ignitePlayer(false);

                    game_config.FirefigherCount = game_config.FirefigherCount - 1;
                }
            break;
            case "timeboost":
                if (game_config.TimeboostCount > 0)
                {
                    EventsManager.current.timeBoostAction(60);

                    game_config.TimeboostCount = game_config.TimeboostCount - 1;
                }
            break;
            case "turbojumper":
                if (game_config.TurboJumperCount > 0)
                {
                    EventsManager.current.turboJumpNotificationShow();

                    EventsManager.current.jumpMultiplierChange();

                    game_config.TurboJumperCount = game_config.TurboJumperCount - 1;
                }
            break;
            case "healthboost":
                if (game_config.HealthBoostCount > 0)
                {
                    healthBar.fillAmount = 1;

                    game_config.HealthBoostCount = game_config.HealthBoostCount - 1;
                }
            break;
        }

        EventsManager.current.hidePieMenu();

        DBOperationsController.element.CreateSaving(game_config);
    }

    private void OnDestroy()
    {
        EventsManager.current.onJumpMultiplierChange -= changeJumpMultiplierValue;
        EventsManager.current.onIgnitePlayer -= ignitePlayer;
    }
}
