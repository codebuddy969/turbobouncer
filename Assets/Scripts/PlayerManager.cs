using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public GameObject energySlider;
    public GameObject energyExplosionPrefab;

    public JoystickManager joystick;

    Rigidbody playerBody;

    Image energyBar;
    bool isGrounded = false;

    void Start()
    {
        playerBody = gameObject.GetComponent<Rigidbody>();
        energyBar = energySlider.gameObject.transform.GetChild(0).GetComponent<Image>();
    }

    void Update()
    {
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

    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
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
