using UnityEngine;

public class DebugLevelButton : MonoBehaviour
{
    void Awake()
    {
        if (!Debug.isDebugBuild)
        {
            gameObject.SetActive(false);
        }
    }
}
