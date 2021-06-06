using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class MenuButtonConfig
{
    public string name;
    public Image sceneImage;
    public Color normalColor = Color.white;
    public Color highlightedColor = Color.grey;
    public Color pressedColor = Color.gray;
}
