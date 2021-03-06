﻿using UnityEngine;

public class MenuSwitcher : MonoBehaviour
{
    public enum MenuButtonAction : byte
    {
        None,       // No action when the menu button is pressed.
        Toggle,     // Sets to start if none, sets to none if not none.
        Return      // Returns to start if not none.
    }

    [SerializeField]
    private MenuButtonAction menuButtonAction;      // Action to take when menu button is pressed.
    [SerializeField]
    private GameObject[] menuRoots;                 // `GameObject`s representing the root objects for various menues.
    [SerializeField]
    private bool awakeToNone;                       // Flag to determine if the menu should awake with no open menu.
    [SerializeField]
    private int startingMenu;                       // Menu to start on when loaded.
    [SerializeField]
    private int finalMenu;                         // Final menu if any.

    private int? currentMenu;                       // Current shown menu.

    /// <summary>
    ///     Gets the current menu open.
    /// </summary>
    /// <remarks>
    ///		<para>
    ///			This value is <c>null</c> if no menu is open.
    ///		</para>
    /// </remarks>
    public int? CurrentMenu { get { return currentMenu; } }

    void OnValidate()
    {
        startingMenu = Mathf.Clamp(startingMenu, 0, menuRoots.Length - 1);
    }

    void Awake()
    {
        if (awakeToNone)
        {
            SwitchToNone();
        }
        else
        {
            SwitchToStart();
        }
    }

    /// <summary>
    ///     Switch to the given menu.
    /// </summary>
    /// <param name="index">
    ///     Menu to switch to.
    /// </param>
    public void Switch(int index)
    {
        // If enabled...
        if (enabled)
        {
            // ... set the current menu to null for now.
            currentMenu = null;

            // For each menu root...
            for (int i = 0; i < menuRoots.Length; i++)
            {
                // ... if the index equals the menu root index.
                if (i == index)
                {
                    // ... then set the current menu to the index.
                    currentMenu = i;
                    // Set active to true the new current menu.
                    menuRoots[i].SetActive(true);

                    // If switched menu is final menu...
                    if (finalMenu == i)
                    {
                        // ... disable the menu switcher;
                        enabled = false;
                    }
                }
                else
                {
                    // ... this menu root is not active so set active to false.
                    menuRoots[i].SetActive(false);
                }
            }

            // Very simple and frankly lazy way to handle game pausing.
            Game.Paused = currentMenu.HasValue;
        }
    }

    /// <summary>
    ///     Switch to the starting menu.
    /// </summary>
    public void SwitchToStart()
    {
        // Switch to start menu index.
        Switch(startingMenu);
    }

    /// <summary>
    ///     Switch to the final menu if exists.
    /// </summary>
    public void SwitchToFinal()
    {
        if (finalMenu >= 0 && finalMenu < menuRoots.Length)
        {
            Switch(finalMenu);
        }
    }

    /// <summary>
    ///     Switch to no menu.
    /// </summary>
    public void SwitchToNone()
    {
        // Switch to -1 as no index will equal -1.
        Switch(-1);
    }

    void Update()
    {
        Game.InputManager.MenuButton(OnMenuButton);
    }

    void OnMenuButton()
    {
        switch (menuButtonAction)
        {
        case MenuButtonAction.Return:
            if (currentMenu.HasValue && currentMenu.Value != startingMenu)
            {
                SwitchToStart();
            }
            break;
        case MenuButtonAction.Toggle:
            if (currentMenu.HasValue)
            {
                SwitchToNone();
            }
            else
            {
                SwitchToStart();
            }
            break;
        case MenuButtonAction.None:
        default:
            break;
        }
    }
}
