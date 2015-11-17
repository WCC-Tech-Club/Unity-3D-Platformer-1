using UnityEngine;

using System;

public class PowerupSpin : MonoBehaviour
{
    [Serializable]
    private class Spinner
    {
        public Transform transform;
        public Vector3 rotation;
    }

    [SerializeField]
    private Spinner[] spinners;

    void Update()
    {
        foreach (Spinner spinner in spinners)
        {
            Vector3 rotation = spinner.transform.localEulerAngles;
            rotation += spinner.rotation * Time.deltaTime;
            spinner.transform.eulerAngles = rotation;
        }
    }
}
