using UnityEngine;

using System;

[RequireComponent(typeof(Renderer))]
public class ElementManager : MonoBehaviour
{
    [Serializable]
    private class Materials
    {
        public Material normal;
        public Material fire;
        public Material ice;
        public Material electrical;
    }

    [SerializeField]
    private Element element;

    [SerializeField]
    private Materials materials;

    [SerializeField]
    private GameObject fireParticlePrefab;
    [SerializeField]
    private GameObject iceParticlePrefab;
    [SerializeField]
    private GameObject electricalParticlePrefab;

    public Element Element
    {
        get { return element; }
        set { element = Enum.IsDefined(typeof(Element), value) ? value : Element.None; }
    }

    public bool IsFire { get { return element == Element.Fire; } }

    public bool IsIce { get { return element == Element.Ice; } }

    public bool IsElectrical { get { return element == Element.Electrical; } }

    private new Renderer renderer;

    private GameObject fireParticles, iceParticles, electricalParticles;

    void Awake()
    {
        renderer = GetComponent<Renderer>();
        fireParticles = Instantiate(fireParticlePrefab);
        iceParticles = Instantiate(iceParticlePrefab);
        electricalParticles = Instantiate(electricalParticlePrefab);
    }

    void Update()
    {
        fireParticles.transform.position = transform.position;
        iceParticles.transform.position = transform.position;
        electricalParticles.transform.position = transform.position;

        switch (element)
        {
        case Element.None:
        default:
            renderer.sharedMaterial = materials.normal;
            fireParticles.SetActive(false);
            iceParticles.SetActive(false);
            electricalParticles.SetActive(false);
            break;
        case Element.Fire:
            renderer.sharedMaterial = materials.fire;
            fireParticles.SetActive(true);
            iceParticles.SetActive(false);
            electricalParticles.SetActive(false);
            break;
        case Element.Ice:
            renderer.sharedMaterial = materials.ice;
            fireParticles.SetActive(false);
            iceParticles.SetActive(true);
            electricalParticles.SetActive(false);
            break;
        case Element.Electrical:
            renderer.sharedMaterial = materials.electrical;
            fireParticles.SetActive(false);
            iceParticles.SetActive(false);
            electricalParticles.SetActive(true);
            break;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        switch (collider.gameObject.tag)
        {
        case "Fire Powerup":
            Element = Element.Fire;
            break;
        }
    }
}

public enum Element : byte
{
    None,
    Fire,
    Ice,
    Electrical
}
