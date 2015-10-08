using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public sealed class LoadMainMenuButton : MonoBehaviour
{
    void Awake()
    {
        // Add a listener to the local `Button` component to load the main menu.
        GetComponent<Button>().onClick.AddListener(Game.LevelManager.LoadMainMenu);
    }
}
