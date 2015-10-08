using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public sealed class QuitGameButton : MonoBehaviour
{
    void Awake()
    {
        // Add a listener to the local `Button` component to quit the game.
        GetComponent<Button>().onClick.AddListener(Game.Quit);
    }
}
