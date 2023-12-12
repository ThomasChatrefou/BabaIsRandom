using System;
using NaughtyAttributes;
using UnityEngine;

public class EntityBaba : Words
{
    [SerializeField]
    private GameObject[] _babas;

    private void OnEnable()
    {
        string tag = "Baba";
        _babas = GameObject.FindGameObjectsWithTag(tag);
    }
    public override GameObject[] GiveListObject()
    {
        return _babas;
    }
}
