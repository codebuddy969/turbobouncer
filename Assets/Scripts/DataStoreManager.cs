using UnityEngine;

public class DataStoreManager : MonoBehaviour
{
    public static DataStoreManager store;

    [HideInInspector]
    public int starsCounter = 0;

    [HideInInspector]
    public bool pieMenuOpenedStatus = false;

    [HideInInspector]
    public int gameSkin = 0;

    private void Awake()
    {
        store = this;

        gameSkin = Random.Range(0, 2);
    }
}
