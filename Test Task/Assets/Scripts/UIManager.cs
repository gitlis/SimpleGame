using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public MainMenuController mainMenu;
    public UIMenuController uiMenu;
    public GameController gameController;
    public MouseInputController mouseInputController;
    public Score score;

    void Awake()
    {
        gameController = GameObject.Find("Main").AddComponent<GameController>();
        mouseInputController = GameObject.Find("Main").AddComponent<MouseInputController>();
        score = GameObject.Find("UIMenu").AddComponent<Score>();
        mainMenu = GameObject.Find("MainMenu").AddComponent<MainMenuController>();
        uiMenu = GameObject.Find("UIMenu").AddComponent<UIMenuController>();
    }

} 
