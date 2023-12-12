using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityRock : Words
{
    [SerializeField]
    private GameObject[] _rocks;

    private void OnEnable()
    {
        string tag = "Rock";
        _rocks = GameObject.FindGameObjectsWithTag(tag);
    }
    public override GameObject[] GiveListObject()
    {
        return _rocks;
    }
}
