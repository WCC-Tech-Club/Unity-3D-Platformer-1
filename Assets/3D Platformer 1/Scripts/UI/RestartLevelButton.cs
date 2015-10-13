using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public sealed class RestartLevelButton : MonoBehaviour
{
    void Awake()
    {
        // Add a listener to the local `Button` component to reload current level.
        GetComponent<Button>().onClick.AddListener(Game.LevelManager.ReloadCurrent);
    }
}
