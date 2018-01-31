using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class MainMenuController : BaseController
{

    private RectTransform mainMenuPanel;
    private RectTransform aboutGamePanel;
    private bool IsAboutPanelActive;

    private UIManager uiManager;

    [SerializeField] Button buttonPlay;
    [SerializeField] Button buttonExit;
    [SerializeField] Button buttonAbout;
    private Text startText;
    private Text gameText;

    void Awake()
    {
        uiManager = GameObject.FindObjectOfType<UIManager>();

        var panels = GetComponentsInChildren<RectTransform>(true).Where(rT => rT.CompareTag("Panel")).ToArray();
        mainMenuPanel = panels[0];
        aboutGamePanel = panels[1];
        IsAboutPanelActive = false;
        aboutGamePanel.gameObject.SetActive(IsAboutPanelActive);
        aboutGamePanel.gameObject.layer++;

        var buttons = mainMenuPanel.GetComponentsInChildren<Button>();
        buttonPlay = buttons[0];
        buttonExit = buttons[1];
        buttonAbout = buttons[2];
        AddButtonEvents();

        var mainMenuText = mainMenuPanel.GetComponentsInChildren<Text>();
        startText = mainMenuText[3];
        startText.text = "Game Squares";
        gameText = mainMenuText[4];
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

        if (buttonAbout != null)
        {
            buttonAbout.onClick.AddListener(AboutGame);
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

    public void AboutGame()
    {
        if (IsAboutPanelActive) IsAboutPanelActive = false;
        else IsAboutPanelActive = true;
        aboutGamePanel.gameObject.SetActive(IsAboutPanelActive);        
    }

    public override void On()
    {
        base.On();
        if(mainMenuPanel != null) mainMenuPanel.gameObject.SetActive(true);
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
