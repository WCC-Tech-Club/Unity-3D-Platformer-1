using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public sealed class QuitGameButton : MonoBehaviour
{
    void Awake()
    {
        // Add a listener to the local `Button` component.
        GetComponent<Button>().onClick.AddListener(() =>
        {
#if UNITY_EDITOR
            // Exit play mode if in the unity editor.
            UnityEditor.EditorApplication.isPlaying = false;
#else
            // Quit the application if outside the unity editor.
			Application.Quit();
#endif
        });
    }
}
