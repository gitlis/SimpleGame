using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : BaseController
{

    private RectTransform mainMenuPanel;
    private UIManager uiManager;

    [SerializeField] Button buttonPlay;
    [SerializeField] Button buttonExit;
    private Text startText;
    private Text gameText;

    void Awake()
    {
        mainMenuPanel = GetComponent<RectTransform>();
        uiManager = GameObject.FindObjectOfType<UIManager>();

        var buttons = mainMenuPanel.GetComponentsInChildren<Button>();
        buttonPlay = buttons[0];
        buttonExit = buttons[1];

        AddButtonEvents();
        var mainMenuText = mainMenuPanel.GetComponentsInChildren<Text>();
        startText = mainMenuText[2];
        startText.text = "Game Squares";
        gameText = mainMenuText[3];
        gameText.text = "Game Squares\n\r You got score:\n\r 0";
        gameText.gameObject.SetActive(false);

    }

    private void AddButtonEvents()
    {
        if (buttonPlay != null)
        {
            buttonPlay.onClick.AddListener(StartGame);
        }

        if (buttonExit != null)
        {
            buttonExit.onClick.AddListener(EndGame);
        }
    }

    public void StartGame()
    {
        if (!uiManager.gameController.IsLevelDone) uiManager.gameController.StartGame();
        uiManager.uiMenu.StartLevel();
        startText.gameObject.SetActive(false);
        gameText.gameObject.SetActive(true);
    }

    public void EndGame()
    {
        uiManager.uiMenu.EndLevel();
        if (uiManager.gameController.IsLevelDone) uiManager.gameController.EndGame();
        startText.gameObject.SetActive(true);
        gameText.gameObject.SetActive(false);
        Off();
        Application.Quit();
    }

    public override void On()
    {
        base.On();
        if (mainMenuPanel != null) mainMenuPanel.gameObject.SetActive(true);
        ShowScoreInMenu();

    }

    public override void Off()
    {
        base.Off();
        if (mainMenuPanel != null) mainMenuPanel.gameObject.SetActive(false);
    }

    private void ShowScoreInMenu()
    {
        string inscription = "Game Squares\n\r You got score:\n\r";
        gameText.text = inscription + uiManager.score.ScoreNumber.ToString();
    }

}
