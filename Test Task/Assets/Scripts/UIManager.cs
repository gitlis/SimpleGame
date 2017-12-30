using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public MainMenuController mainMenu;
    public UIMenuController uiMenu;
    public Score score;

    private void Awake()
    {
        score = GameObject.Find("UIMenu").AddComponent<Score>();
        mainMenu = GameObject.Find("MainMenu").AddComponent<MainMenuController>();
        uiMenu = GameObject.Find("UIMenu").AddComponent<UIMenuController>();

    }

} 
