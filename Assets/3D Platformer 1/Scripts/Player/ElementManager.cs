using UnityEngine;

using System;

public class ElementManager : MonoBehaviour
{
    [SerializeField]
    private Element element;

    public Element Element
    {
        get { return element; }
        set { element = Enum.IsDefined(typeof(Element), value) ? value : Element.None; }
    }

    void Update()
    {
        switch (element)
        {
        case Element.None:
        default:
            // Default stuff
            break;
        case Element.Fire:
            // Fire stuff
            break;
        case Element.Ice:
            // Ice stuff
            break;
        case Element.Electrical:
            // Electrical stuff
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
