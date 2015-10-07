using UnityEngine;

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
    private int startingMenu;                       // Menu to start on when loaded.

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
        SwitchToStart();
    }

    /// <summary>
    ///     Switch to the given menu.
    /// </summary>
    /// <param name="index">
    ///     Menu to switch to.
    /// </param>
    public void Switch(int index)
    {
        // Set the current menu to null for now.
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
            }
            else
            {
                // ... this menu root is not active so set active to false.
                menuRoots[i].SetActive(false);
            }
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
    ///     Switch to no menu.
    /// </summary>
    public void SwitchToNone()
    {
        // Switch to -1 as no index will equal -1.
        Switch(-1);
    }
}
