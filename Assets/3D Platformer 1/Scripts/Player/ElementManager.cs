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

    public bool IsFire { get { return element == Element.Fire; } }

    public bool IsIce { get { return element == Element.Ice; } }

    public bool IsElectrical { get { return element == Element.Electrical; } }

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
