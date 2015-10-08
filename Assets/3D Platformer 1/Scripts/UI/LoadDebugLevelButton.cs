using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public sealed class LoadDebugLevelButton : MonoBehaviour
{
    void Awake()
    {
        // Add a listener to the local `Button` component to load the debug level.
        GetComponent<Button>().onClick.AddListener(Game.LevelManager.LoadDebugLevel);
    }
}
