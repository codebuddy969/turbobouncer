using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public JoystickManager joystick;

    public GameObject firePrefab;
    public GameObject energySlider;
    public GameObject healthSlider;
    public GameObject energyExplosionPrefab;

    private Rigidbody playerBody;

    private Image healthBar;
    private Image energyBar;

    private int jumpMultiplier = 15;

    private bool isIgnited = false;
    private bool isGrounded = false;

    void Start()
    {
        playerBody = gameObject.GetComponent<Rigidbody>();

        energyBar = energySlider.gameObject.transform.GetChild(0).GetComponent<Image>();
        healthBar = healthSlider.gameObject.transform.GetChild(0).GetComponent<Image>();

        EventsManager.current.onJumpMultiplierChange += changeJumpMultiplierValue;
        EventsManager.current.onIgnitePlayer += ignitePlayer;
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

        energyBar.fillAmount += 0.006f;
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

    private void environmentCollisions(Collision collision)
    {
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

        if (collision.collider.name == "right-limiter")
        {
            playerBody.AddForce(Vector3.left * 15, ForceMode.VelocityChange);
            return;
        }

        if (collision.collider.name == "left-limiter")
        {
            playerBody.AddForce(Vector3.right * 15, ForceMode.VelocityChange);
            return;
        }
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

            jumpMultiplier = 15;

            Object.Destroy(prefab, 3.0f);
        }
    }

    public void ignitePlayer()
    {
        ParticleSystem fire = gameObject.transform.Find("Fire").GetComponent<ParticleSystem>();

        var emission = fire.emission;

        emission.enabled = true;

        isIgnited = true;
    }

    private void OnDestroy()
    {
        EventsManager.current.onJumpMultiplierChange -= changeJumpMultiplierValue;
        EventsManager.current.onIgnitePlayer -= ignitePlayer;
    }
}
