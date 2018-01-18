using UnityEngine;

public class UIManager : MonoBehaviour
{
    public MainMenuController mainMenu;
    public UIMenuController uiMenu;
    public GameController gameController;
    public MouseInputController mouseInputController;
    public PrefabStore prefabInstance;
    public GameSettings gameSettings;
    public Score score;

    void Awake()
    {
        gameSettings = new GameSettings();

        prefabInstance = GameObject.Find("Main").AddComponent<PrefabStore>();
        gameController = GameObject.Find("Main").AddComponent<GameController>();
        mouseInputController = GameObject.Find("Main").AddComponent<MouseInputController>();
        score = GameObject.Find("UIMenu").AddComponent<Score>();
        mainMenu = GameObject.Find("MainMenu").AddComponent<MainMenuController>();
        uiMenu = GameObject.Find("UIMenu").AddComponent<UIMenuController>();
    }

    void Start()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            Debug.Log(Application.platform.ToString());

#if UNITY_STANDALONE_WIN
        Debug.Log("Stand Alone Windows");
#else
            Debug.Log("Any other platform");
#endif

#if UNITY_ANDROID
            Debug.Log("Android");
#endif

        }
    }
} 
