using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityWall : Words
{
    [SerializeField]
    private GameObject[] _walls;
    private void OnEnable()
    {
        string tag = "Wall";
        _walls = GameObject.FindGameObjectsWithTag(tag);
    }
    public override GameObject[] GiveListObject()
    {
        return _walls;
    }
}
