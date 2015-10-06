using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public sealed class CurrentLevelText : MonoBehaviour
{
	private Text text;

	void Awake()
	{
		text = GetComponent<Text>();
	}

	void OnLevelWasLoaded(int level)
	{
		// If game instance exists...
		if (Game.Exists)
		{
			int? currentLevel = Game.LevelManager.CurrentLevel;
			text.text = "Level " + (currentLevel.HasValue ? currentLevel.Value + ": " : string.Empty) + Game.LevelManager.CurrentLevelName;
		}
		else
		{
			if (Debug.isDebugBuild)
			{
				Debug.LogErrorFormat(this, "<b>Game Not Found</b>: Level selector could not be obtained as the `Game` instance does not exist.");
			}
		}
	}
}
