using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharactersManager : MonoBehaviour
{
    public GameObject player;
    public Character[] characters;

    private float BallVerticalPosition;

    private DateTime timestamp;

    public void Start()
    {
        timestamp = DateTime.Now;
    }

    public void Update()
    {
        int randomYIndex = UnityEngine.Random.Range(5, 50);

        if (player)
        {
            BallVerticalPosition = player.transform.position.y + randomYIndex;
        }
    }

    private void FixedUpdate()
    {
        DateTime now = DateTime.Now;

        if (timestamp < now)
        {
            CreateCharacter();
            timestamp = timestamp.AddSeconds(UnityEngine.Random.Range(0.5f, 3));
        }

    }

    private void CreateCharacter()
    {
        int random = UnityEngine.Random.Range(0, characters.Length - 1);

        Character character = characters[random];

        GameObject characterGameObject = new GameObject(character.name);

        characterGameObject.AddComponent<SpriteRenderer>();

        SpriteRenderer renderer = characterGameObject.GetComponent<SpriteRenderer>();

        renderer.sprite = character.sprite;

        renderer.color = new Color(1f, 1f, 1f, UnityEngine.Random.Range(0.8f, 1f));

        CharacterParameters(characterGameObject, character.speed);
    }

    private IEnumerator CreateCharacters()
    {
        while (true)
        {
            foreach (Character s in characters)
            {
                GameObject characterObject = new GameObject(s.name);

                characterObject.AddComponent<SpriteRenderer>();

                var spriterenderer = characterObject.GetComponent<SpriteRenderer>();

                spriterenderer.sprite = s.sprite;

                CharacterParameters(characterObject, s.speed);
            }

            yield return new WaitForSeconds(4.0f);
        }
    }
    private void CharacterParameters(GameObject characterObject, float speed)
    {
        int randomXIndex = UnityEngine.Random.Range(0, 2);
        int[] startHPosition = { -20, 20 };

        float ZRange = UnityEngine.Random.Range(1.0f, 2.5f);
        float ScaleRange = UnityEngine.Random.Range(0.8f, 3.0f);

        Rigidbody rigidBody = characterObject.AddComponent<Rigidbody>();

        characterObject.transform.position = new Vector3(startHPosition[randomXIndex], BallVerticalPosition, ZRange);
        characterObject.transform.localScale = new Vector3(ScaleRange, ScaleRange, 1);

        CharacterSetMovement(characterObject, rigidBody, speed);
    }
    private void CharacterSetMovement(GameObject characterObject, Rigidbody rigidbody, float speed)
    {
        float position = characterObject.transform.position.x;
        Vector3 direction = Mathf.Sign(position) == 1 ? Vector3.left : Vector3.right;

        characterObject.transform.Rotate(Mathf.Sign(position) == 1 ? Vector3.up * 180 : Vector3.up);

        rigidbody.useGravity = false;

        rigidbody.AddForce(direction * speed, ForceMode.VelocityChange);

        StartCoroutine(DestroyCharacter(characterObject, speed, Mathf.Sign(position)));
    }
    private IEnumerator DestroyCharacter(GameObject characterObject, float speed, float mathSign)
    {
        bool loopCondition = true;

        while (loopCondition)
        {
            float position = characterObject.transform.position.x;
            bool condition = mathSign == 1 ? position < -15 : position > 15;

            if (condition)
            {
                loopCondition = false;
                Destroy(characterObject);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}

[System.Serializable]
public class Character
{
    public string name;

    public Sprite sprite;

    [Range(.1f, 2f)]
    public float speed = 1;
}