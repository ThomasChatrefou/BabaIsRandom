using NaughtyAttributes;
using System.Diagnostics;
using UnityEngine;

public class FrameRateCalculatorViewer : MonoBehaviour
{
    [Header("Behaviour to debug")]
    [SerializeField] private FrameRateCalculator frameRateCalculator;

    private bool calculatorIsValid => frameRateCalculator != null;
    [ShowIf("calculatorIsValid")]
    [Range(1f, 30f)]
    [SerializeField] private float refreshRate = 30f;

    [Header("Debugger components")]
    [SerializeField] private Debugger debugger;
    [SerializeField] private GuiDrawer drawer;

    private GuiContainer container;
    private float timeFlag = 0f;

    enum DebugVariables
    {
        FPS
    }

    private void OnEnable()
    {
        enabled = true;
        container = drawer.CreateContainer("Frame Rate Calculator");
        container.Add((int)DebugVariables.FPS, "FPS");
    }

    private void LateUpdate()
    {
        if (debugger.IsEnabled)
        {
            if (Mathf.Abs(Time.time - timeFlag) > 1f / refreshRate)
            {
                timeFlag = Time.time;
                container.UpdateVal((int)DebugVariables.FPS, frameRateCalculator.GetFrameRate());
            }
        }
    }

    private void OnDisable()
    {
        drawer.RemoveContainer(container);
    }
}
