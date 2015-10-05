using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public sealed class QuitGameButton : MonoBehaviour
{
	void Awake()
	{
		GetComponent<Button>().onClick.AddListener(() =>
		{
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#else
			Application.Quit();
#endif
		});
	}
}
