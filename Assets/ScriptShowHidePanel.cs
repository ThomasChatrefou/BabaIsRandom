using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptShowHidePanel : MonoBehaviour
{
    public GameObject Panel;

    public void ShowHidePanel()
    {
        Panel.SetActive(!Panel.active);
    }
}
