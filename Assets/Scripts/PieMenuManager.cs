using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityScreen = UnityEngine.Screen;

public class PieMenuManager : MonoBehaviour
{
    public List<MenuButton> buttons = new List<MenuButton>();

    private int menuItem;
    private int curMenuItem;
    private int oldMenuItem;

    private Vector2 toVector2M;
    private Vector2 mousePosition;
    private Vector2 fromVector2M = new Vector2(0.5f, 1.0f);
    private Vector2 centerCircle = new Vector2(0.5f, 0.5f);

    void Start()
    {
        curMenuItem = 0;
        oldMenuItem = 0;
        menuItem = buttons.Count;

        foreach (MenuButton button in buttons)
        {
            button.sceneImage.color = button.normalColor;
        }
    }

    void Update()
    {
        GetCurrentMenuItem();

        if (Input.GetButtonDown("Fire1"))
        {
            ButtonAction();
        }
    }

    public void GetCurrentMenuItem()
    {
        Vector2 screenSize = new Vector2(transform.parent.GetComponent<RectTransform>().rect.width, transform.parent.GetComponent<RectTransform>().rect.height);
        Vector2 containerSize = new Vector2(transform.GetComponent<RectTransform>().rect.width, transform.GetComponent<RectTransform>().rect.height);

        float screenPointSize = screenSize.x / Screen.width;

        mousePosition = new Vector2(Input.mousePosition.x * screenPointSize, Input.mousePosition.y * screenPointSize);

        float widthDistance = (screenSize.x - containerSize.x) / 2;
        float heightDistance = (screenSize.y - containerSize.y) / 2;

        bool widthCondition = mousePosition.x > widthDistance && mousePosition.x < widthDistance + containerSize.x;
        bool heightCondition = mousePosition.y > heightDistance && mousePosition.y < heightDistance + containerSize.y;

        if (widthCondition && heightCondition)
        {
            toVector2M = new Vector2(mousePosition.x / screenSize.x, mousePosition.y / screenSize.y);

            float angle = (Mathf.Atan2(fromVector2M.y - centerCircle.y, fromVector2M.x - centerCircle.x) - Mathf.Atan2(toVector2M.y - centerCircle.y, toVector2M.x - centerCircle.x)) * Mathf.Rad2Deg;

            if (angle < 0)
            {
                angle += 360;
            }

            curMenuItem = (int)(angle / (360 / menuItem));

            if (curMenuItem != oldMenuItem)
            {
                buttons[oldMenuItem].sceneImage.color = buttons[oldMenuItem].normalColor;
                oldMenuItem = curMenuItem;
                buttons[curMenuItem].sceneImage.color = buttons[curMenuItem].highlightedColor;
            }
        }
        else
        {
            curMenuItem = menuItem + 1;
        }
    }

    public void ButtonAction()
    {
        if (menuItem >= curMenuItem)
        {
            buttons[curMenuItem].sceneImage.color = buttons[curMenuItem].pressedColor;
        }
    }
}

[System.Serializable]
public class MenuButton
{
    public string name;
    public Image sceneImage;
    public Color normalColor = Color.white;
    public Color highlightedColor = Color.grey;
    public Color pressedColor = Color.gray;
}
