using UnityEngine;

using System;

[Serializable]
public sealed class ReorderableStringArray
{
    [SerializeField]
    private string[] strings;

    public int Length { get { return strings.Length; } }

    public string this[int index] { get { return strings[index]; } }
}
