using UnityEngine;
using UnityEngine.UI;

public sealed class LevelSelector : MonoBehaviour
{
	[SerializeField]
	private GameObject levelSelectorButtonPrefab;

	void Awake()
	{
		if (Game.Exists)
		{
			LevelManager levelManager = Game.LevelManager;
            Transform transform = base.transform;

            for (int i = 0; i < levelManager.LevelCount; i++)
            {
                GameObject levelSelectionButton = Instantiate(levelSelectorButtonPrefab);

                Text text = levelSelectionButton.GetComponentInChildren<Text>();
                text.text = "Level " + (i + 1) + ": " + levelManager.GetLevelName(i);

                int levelIndex = i;
                Button button = levelSelectionButton.GetComponentInChildren<Button>();
                button.onClick.AddListener(() => { levelManager.LoadLevel(levelIndex); });

                levelSelectionButton.transform.SetParent(transform);
            }
        }
		else
		{
			if (Debug.isDebugBuild)
			{
				Debug.LogErrorFormat(this, "<b>Game Not Found</b> Level selector could not be obtained as the `Game` instance does not exist.");
			}
		}
	}
}
