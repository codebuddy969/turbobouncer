using UnityEngine;
using System.Collections.Generic;

public class PieMenuManager : MonoBehaviour
{
    public List<MenuButtonConfig> buttons = new List<MenuButtonConfig>();

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

        foreach (MenuButtonConfig button in buttons)
        {
            button.sceneImage.color = button.normalColor;
        }
    }

    void Update()
    {
        if (DataStoreManager.store.pieMenuOpenedStatus)
        {
            GetCurrentMenuItem();

            if (Input.GetButtonDown("Fire1") && curMenuItem >= 0 && curMenuItem <= menuItem - 1)
            {
                buttons[curMenuItem].sceneImage.color = buttons[curMenuItem].pressedColor;
            }
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

        var dx = mousePosition.x - (screenSize.x / 2);
        var dy = mousePosition.y - (screenSize.y / 2);

        var circle = Mathf.Sqrt(dx * dx + dy * dy);

        if (widthCondition && heightCondition && (circle > 80))
        {
            toVector2M = new Vector2(mousePosition.x / screenSize.x, mousePosition.y / screenSize.y);

            float angle = (Mathf.Atan2(fromVector2M.y - centerCircle.y, fromVector2M.x - centerCircle.x) - Mathf.Atan2(toVector2M.y - centerCircle.y, toVector2M.x - centerCircle.x)) * Mathf.Rad2Deg;

            if (angle < 0)
            {
                angle += 360;
            }

            curMenuItem = (int)(angle / (360 / menuItem));

            if (curMenuItem >= 0 && (curMenuItem <= menuItem - 1) && curMenuItem != oldMenuItem)
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
}
