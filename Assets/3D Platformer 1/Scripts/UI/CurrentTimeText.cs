using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public sealed class CurrentTimeText : MonoBehaviour
{
    private Text text;          // Reference to local Text component.

    void Awake()
    {
        // Reference local Text component.
        text = GetComponent<Text>();
    }

    void OnEnable()
    {
        // Set the text to represent the formated time.
        text.text = LevelManager.FormatTime(Time.timeSinceLevelLoad);
    }
}
