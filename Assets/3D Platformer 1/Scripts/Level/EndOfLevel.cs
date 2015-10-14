using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
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

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0, 1, 0.25f);
        BoxCollider boxCollider = GetComponent<BoxCollider>();

        Vector3 center = boxCollider.center;
        center.Scale(transform.lossyScale);
        center += transform.position;

        Vector3 size = boxCollider.size;
        size.Scale(transform.lossyScale);

        Gizmos.DrawCube(center, size);
    }
}
