using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class FrameRateCalculator : MonoBehaviour
{
    [SerializeField] private bool doAverage = false;

    [EnableIf("doAverage")]
    [OnValueChanged("OnFramesToCountChanged")]
    [Range(1, 100)]
    [SerializeField] private int framesToCount = 100;

    private Queue<float> deltaTimeQueue;
    private float cumulatedTime = 0f;

    private void Awake()
    {
        deltaTimeQueue = new Queue<float>(framesToCount);
    }

    private void Update()
    {
        if (doAverage)
        {
            if (deltaTimeQueue.Count >= framesToCount)
            {
                cumulatedTime -= deltaTimeQueue.Dequeue();
            }

            deltaTimeQueue.Enqueue(Time.deltaTime);
            cumulatedTime += Time.deltaTime;
        }
    }

    private int ComputeAverageFrameRate()
    {
        return (int)(framesToCount / cumulatedTime);
    }

    private int ComputeInstantFrameRate()
    {
        return (int)(1.0f / Time.deltaTime);
    }

    public int GetFrameRate()
    {
        return doAverage ? ComputeAverageFrameRate() : ComputeInstantFrameRate();
    }

    private void OnFramesToCountChanged()
    {
        deltaTimeQueue = new Queue<float>(framesToCount);
        cumulatedTime = 0f;
    }
}