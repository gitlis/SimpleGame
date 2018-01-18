using UnityEngine;

public class PrefabStore : MonoBehaviour
{

    [Header("Prefabs")]

    [SerializeField]
    public GameObject square;
    [SerializeField]
    public GameObject circle;


    void Awake()
    {
        square = Resources.Load<GameObject>("Square");
        circle = Resources.Load<GameObject>("Circle");
    }

    public GameObject GetSquarePrefab()
    { 
        return square;
    }

    public GameObject GetCicrlePrefab()
    {
        return circle;
    }
}
