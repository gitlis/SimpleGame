using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : BaseController
{

    private RectTransform mainMenuPanel;
    private UIManager uiManager;
    private GameController gameController;

    [SerializeField] Button buttonPlay;
    [SerializeField] Button buttonExit;
    private Text startText;
    private Text gameText;

    private void Awake()
    {
        uiManager = GameObject.FindObjectOfType<UIManager>();
        gameController = GameObject.FindObjectOfType<GameController>();
        mainMenuPanel = gameObject.GetComponent<RectTransform>();

        var buttons = mainMenuPanel.gameObject.GetComponentsInChildren<Button>();
        buttonPlay = buttons[0];
        buttonExit = buttons[1];

        AddButtonEvents();
        var mainMenuText = mainMenuPanel.gameObject.GetComponentsInChildren<Text>();
        startText = mainMenuText[0];
        gameText = mainMenuText[1];

    }

    void Start()
    {
        On();
        startText.gameObject.SetActive(true);
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
        uiManager.uiMenu.StartLevel();
        gameController.StartGame();
        startText.gameObject.SetActive(false);
        gameText.gameObject.SetActive(true);
        Off();
    }

    public void EndGame()
    {
        Off();
        Application.Quit();
    }

    public override void On()
    {
        base.On();
        if (mainMenuPanel != null) mainMenuPanel.gameObject.SetActive(true);

    }

    public override void Off()
    {
        base.Off();
        if (mainMenuPanel != null) mainMenuPanel.gameObject.SetActive(false);
    }
}
