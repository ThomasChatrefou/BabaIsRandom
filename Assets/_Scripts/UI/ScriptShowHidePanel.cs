using UnityEngine;

public class ScriptShowHidePanel : MonoBehaviour
{
    public GameObject Panel;

    public void ShowHidePanel()
    {
        Panel.SetActive(!Panel.activeSelf);
    }
}
