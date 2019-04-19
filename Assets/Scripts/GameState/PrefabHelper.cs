using UnityEngine;

public class PrefabHelper : MonoBehaviour
{
    public static PrefabHelper Instance { get; private set; }

    public GameObject PlayerPrefab;

    private void Awake()
    {
        Instance = this.GetAndEnforceSingleInstance(Instance);
        DontDestroyOnLoad(this);
    }
}
