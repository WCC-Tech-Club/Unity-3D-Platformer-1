using UnityEngine;

public sealed class EndOfLevel : MonoBehaviour
{
    [SerializeField]
    private MenuSwitcher menuSwitcher;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            menuSwitcher.SwitchToFinal();
        }
    }
}
