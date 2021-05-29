using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public JoystickManager joystick;

    public GameObject firePrefab;
    public GameObject energySlider;
    public GameObject healthSlider;
    public GameObject energyExplosionPrefab;

    public Rigidbody playerBody;

    private GameObject fireInstance;
    private GameObject fireInstanceContainer;

    private Image healthBar;
    private Image energyBar;

    private bool isGrounded = false;
    private bool fireBallIgnitionStatus = false;
    private bool fireBallImmutableStatus = false;

    void Start()
    {
        fireInstance = new GameObject();
        fireInstanceContainer = new GameObject();

        playerBody = gameObject.GetComponent<Rigidbody>();
        energyBar = energySlider.gameObject.transform.GetChild(0).GetComponent<Image>();
        healthBar = healthSlider.gameObject.transform.GetChild(0).GetComponent<Image>();
    }

    void Update()
    {
        decreaseHealthOnIgnition();

        if ((joystick.Horizontal >= .1f) && !isGrounded)
        {
            playerBody.AddForce(Vector3.right * joystick.Horizontal, ForceMode.VelocityChange);
        }

        if ((joystick.Horizontal <= -.1f) && !isGrounded)
        {
            playerBody.AddForce(Vector3.left * -joystick.Horizontal, ForceMode.VelocityChange);
        }

        energyBar.fillAmount += 0.004f;
    }

    void OnCollisionEnter(Collision collision)
    {
        playerBody.transform.position = new Vector3(playerBody.transform.position.x, playerBody.transform.position.y, 0);

        environmentCollisions(collision);

        entityiesCollisions(collision);
    }

    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

    private void entityiesCollisions(Collision collision)
    {
        if (collision.collider.transform.childCount == 0)
        {
            return;
        }

        switch (collision.collider.transform.GetChild(0).name)
        {
            case "fireEnemy":
                if (!fireBallImmutableStatus)
                {
                    setTheBallOnFire();
                }
                Destroy(collision.gameObject);
                break;
            case "panzerEnemy":
                if (!fireBallImmutableStatus)
                {
                    //healthSlider.value -= 0.15f;
                }
                Destroy(collision.gameObject);
                break;
            case "bombEnemy":
                if (!fireBallImmutableStatus)
                {
                    //healthSlider.value -= 0.40f;
                }
                Destroy(collision.gameObject);
                //initializeBombExplosion(collision.gameObject.transform.position);
                break;
            case "healthBoost":
                //healthSlider.value += 0.50f;
                Destroy(collision.gameObject);
                break;
            case "powerBoost":
                //powerSlider.value += 0.99f;
                Destroy(collision.gameObject);
                break;
            case "scoreBoost":
                //scoreBoostOperations();
                Destroy(collision.gameObject);
                break;
        }
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

    private void setTheBallOnFire()
    {
        if (!fireBallIgnitionStatus)
        {
            Vector3 position = new Vector3(
                playerBody.transform.position.x,
                playerBody.transform.position.y,
                playerBody.transform.position.z - 1.5f
            );

            var rotation = Quaternion.Euler(
                transform.rotation.eulerAngles.x,
                transform.rotation.eulerAngles.y,
                transform.rotation.eulerAngles.z
            );

            fireInstance = Instantiate(firePrefab, position, rotation, fireInstanceContainer.transform) as GameObject;

            fireInstance.name = "FireBallInstance";

            fireInstanceContainer.name = "FireBall";

            fireInstance.transform.parent = gameObject.transform;

            fireBallIgnitionStatus = true;
        }
    }

    private void decreaseHealthOnIgnition()
    {
        Debug.Log(fireBallIgnitionStatus);
        if (fireBallIgnitionStatus)
        {
            healthBar.fillAmount -= 0.0001f;
        }
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

            playerBody.AddForce(Vector3.up * 15, ForceMode.VelocityChange);

            Object.Destroy(prefab, 3.0f);
        }
    }
}
