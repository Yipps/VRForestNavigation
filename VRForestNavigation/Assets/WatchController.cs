using System;
using UnityEngine;
using TMPro;
using VRTK;

public class WatchController : MonoBehaviour
{
    private int currentTimeMin = 725;
    TextMeshPro timeText;

    // Start is called before the first frame update
    void Start()
    {
        timeText = GetComponentInChildren<TextMeshPro>();
        UpdateWatchTime(0);
    }

    // Update is called once per frame
    private void UpdateWatchTime(int additionalTime)
    {
        currentTimeMin += additionalTime;
        TimeSpan ts = TimeSpan.FromMinutes(currentTimeMin);
        timeText.text = string.Format(" {0:00}\n:{1:00}", ts.TotalHours, ts.Minutes);
    }

    public void DoUpdateWatchTime(object sender, DestinationMarkerEventArgs e)
    {
        int additionalTime = UnityEngine.Random.Range(30, 40);
        UpdateWatchTime(additionalTime);
    }
}
