using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Words : WorldBehaviour
{
    
    

    /// <summary>
    /// Fonction qui recupere la liste des objets/entity utilisé dans 
    /// les operators states
    /// </summary>
    /// <returns></returns>
    public virtual GameObject[] GiveListObject()
    {
        return null;
    }
    /// <summary>
    /// Fonction qui donne la fonction a appliquer a la liste d'objets
    /// </summary>
    /// <returns></returns>
    public virtual string GiveTypeToUse()
    {
        return null;
    }

}
