using UnityEngine;

public class MenuManager : MonoBehaviour
{
	[SerializeField]
	private GameObject[] menuRoots;
	[SerializeField]
	private int startingMenu;

	void OnValidate()
	{
		startingMenu = Mathf.Clamp(startingMenu, 0, menuRoots.Length - 1);
	}

	void Awake()
	{
		Switch(startingMenu);
	}

	public void Switch(int index)
	{
		for (int i = 0; i < menuRoots.Length; i++)
		{
			menuRoots[i].SetActive(i == index);
        }
	}
}
