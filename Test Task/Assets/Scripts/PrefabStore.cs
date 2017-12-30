using UnityEngine;

public class PrefabStore : MonoBehaviour
{

    [Header("Префабы")]

    [SerializeField]
    public GameObject Square;
    [SerializeField]
    public GameObject Circle;

    public static PrefabStore Instance { get; set; }

    // Use this for initialization
    void Awake()
    {
        Instance = this;
    }

    public GameObject GetSquarePrefab()
    {
        return Square;
    }

    public GameObject GetCicrlePrefab()
    {
        return Circle;
    }
}
