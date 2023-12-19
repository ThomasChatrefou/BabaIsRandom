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
        GiveListObject();
    }
    public override GameObject[] GiveListObject()
    {
        /*test pour le futur is*/
         
        if (_rocks[0].TryGetComponent<GameEntityHandler>(out GameEntityHandler geh))
        {
            //Debug.Log("youpi ta race"+geh.GetEntity().State);
            //geh.GetEntity().SetIsDefeat();
        }
        //
        return _rocks;
    }
}
