using UnityEngine;

using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Collider))]
public class Burnable : MonoBehaviour
{
    [SerializeField]
    private float burnTime;

    private Collider[] colliders;

    void Awake()
    {
        colliders = GetComponents<Collider>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && Game.LevelManager.LevelController.Player.ElementManager.IsFire)
        {
            Burn();
        }
    }

    public void Burn()
    {
        StartCoroutine(BurnProcess());
    }

    private IEnumerator BurnProcess()
    {
        // TODO Move this into an animation event that happends at the end of the event

        yield return new WaitForSeconds(burnTime);
        DisableColliders();
    }

    void DisableColliders()
    {
        foreach (Collider collider in colliders)
        {
            collider.enabled = false;
        }
    }
}
