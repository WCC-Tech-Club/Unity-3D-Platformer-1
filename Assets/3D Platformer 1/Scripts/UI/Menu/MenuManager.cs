using UnityEngine;

public class MenuManager : MonoBehaviour
{
	[SerializeField]
	private GameObject[] menuRoots;
	[SerializeField]
	private int startingMenu;

    private int? currentMenu;

    public int? CurrentMenu { get { return currentMenu; } }

    void OnValidate()
	{
		startingMenu = Mathf.Clamp(startingMenu, 0, menuRoots.Length - 1);
	}

	void Awake()
	{
        SwitchToStart();
	}

	public void Switch(int? index)
    {
        currentMenu = null;
        for (int i = 0; i < menuRoots.Length; i++)
        {
            if (i == index)
            {
                currentMenu = i;
                menuRoots[i].SetActive(true);
            }
            else
            {
                menuRoots[i].SetActive(false);
            }
        }
    }

    public void SwitchToStart()
    {
        Switch(startingMenu);
    }

    public void SwitchToNone()
    {
        Switch(null);
    }
}
