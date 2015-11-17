using UnityEngine;
using System.Collections;

public class Explosive : MonoBehaviour
{
    [SerializeField]
    private Transform explosionPrefab;
    [SerializeField]
    private float spreadRadius = 1.5f;
    [SerializeField]
    private float spreadDelay = 0.2f;

    [SerializeField]
    private LayerMask spreadLayers = -1;

    private bool exploding;

    void OnTriggerEnter(Collider collider)
    {
        if (!exploding && collider.gameObject.tag == "Player" && collider.GetComponent<ElementManager>().IsFire)
        {
            StartCoroutine(Explode());
        }
    }

    private void ExplosionSpread()
    {
        if (!exploding)
        {
            StartCoroutine(Explode(spreadDelay));
        }
    }

    private IEnumerator Explode(float delay = 0)
    {
        exploding = true;

        if (delay > 0)
        {
            yield return new WaitForSeconds(delay);
        }

        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        foreach (Collider collider in Physics.OverlapSphere(transform.position, spreadRadius, spreadLayers, QueryTriggerInteraction.Ignore))
        {
            if (collider.gameObject.tag == "Explosive")
            {
                collider.GetComponent<Explosive>().ExplosionSpread();
            }
        }

        Destroy(gameObject);
    }
}
