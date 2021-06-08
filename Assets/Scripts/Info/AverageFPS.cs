using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AverageFPS : MonoBehaviour {
    //Frames counted in interval
    private ulong fpsInInterval = 0;

    //Should the interval reset
    private bool intervalReset = false;

    //Time between samples
    [SerializeField] private float intervalTime = 1f;

    //Time since last sample
    private float sampleTime = 0f;
    [SerializeField] private float frameTime = 0.05f;

    //Average fps
    private float avgFPS = 0f;

    //Time to display the val
    [SerializeField] private float timeToDisplay = 0.5f;
    private bool displayAVG = true;

    //Counter
    [SerializeField] private DynamicCounter counter;

    private void Start () {
        counter.AddRecord("avgfps", $"Average FPS (last {intervalTime} seconds)");
        StartCoroutine(SampleTime());
        if (intervalTime > 0f)
            StartCoroutine(WaitInterval());
        StartCoroutine(TimeToDisplay());

    }

    private void Update() {
        if (intervalReset) {
            sampleTime = 0f;
            fpsInInterval = 0;
            avgFPS = 0;
            intervalReset = false;
        }

        fpsInInterval++;
        avgFPS = fpsInInterval / sampleTime;

        if (displayAVG) {
            counter.SetRecordVal("avgfps", (float) Mathf.Round(avgFPS * 100) / 100);
            displayAVG = false;
        }
    }

    IEnumerator WaitInterval () {
        while (true) {
            yield return new WaitForSecondsRealtime(intervalTime);
            intervalReset = true;
        }
    }

    IEnumerator SampleTime () {
        while (true) {
            yield return new WaitForSecondsRealtime(frameTime);
            sampleTime += frameTime;
        }
    }

    IEnumerator TimeToDisplay () {
        while (true) {
            yield return new WaitForSecondsRealtime(timeToDisplay);
            displayAVG = true;
        }
    }
}
