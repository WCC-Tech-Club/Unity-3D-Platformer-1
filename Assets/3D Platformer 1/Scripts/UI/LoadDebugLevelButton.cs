using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public sealed class LoadDebugLevelButton : MonoBehaviour
{
    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            if (Game.Exists)
            {
                Game.LevelManager.LoadDebugLevel();
            }
            else
            {
                if (Debug.isDebugBuild)
                {
                    Debug.LogErrorFormat(this, "<b>Game Not Found</b> Level selector could not be obtained as the `Game` instance does not exist.");
                }
            }
        });
    }
}
