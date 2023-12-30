using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Debugger : MonoBehaviour
{
    //[MenuItem("GameObject/Tools/Debugger")]
    public static void Init()
    {
        GameObject go = new("Debugger");
        go.AddComponent<Debugger>();
        go.AddComponent<GuiDrawer>();
    }

    public bool IsEnabled { get; private set; }

    private DebuggerInputs inputs;

    private void Awake()
    {
        inputs = new DebuggerInputs();
        inputs.Debug.Toggle.performed += ToggleDebugger;
    }

    private void ToggleDebugger(InputAction.CallbackContext obj)
    {
        IsEnabled = !IsEnabled;
        string state = IsEnabled ? "enabled" : "disabled";
        print("Debug mode " + state);
    }

    private void OnEnable()
    {
        inputs.Debug.Enable();
    }

    private void OnDisable()
    {
        inputs.Debug.Disable();
    }
}
