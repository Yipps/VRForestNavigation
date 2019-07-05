using System;
using UnityEngine;
using TMPro;
using VRTK;
using System.Collections;

public class WatchController : MonoBehaviour
{
    public float watchDelay = 3;

    private AudioSource watchSound;
    TextMeshPro timeText;

    // Start is called before the first frame update
    void Start()
    {
        timeText = GetComponentInChildren<TextMeshPro>();
        watchSound = GetComponent<AudioSource>();
        UpdateWatchTime(0);
    }

    private void OnEnable()
    {
        TimeSpan ts = TimeSpan.FromMinutes(PlayerManager.instance.currentTimeInMinutes);
        timeText.text = string.Format(" {0:00}\n:{1:00}", ts.TotalHours, ts.Minutes);
    }

    private void UpdateWatchTime(int additionalTime)
    {
        
        int currentHour = PlayerManager.instance.currentTimeInMinutes / 60;
        PlayerManager.instance.currentTimeInMinutes += additionalTime;

        if(currentHour != PlayerManager.instance.currentTimeInMinutes / 60 && watchSound != null)
        {

            StartCoroutine(DelayedPulse());
        }

        TimeSpan ts = TimeSpan.FromMinutes(PlayerManager.instance.currentTimeInMinutes);
        timeText.text = string.Format(" {0:00}\n:{1:00}", ts.TotalHours, ts.Minutes);

        PlayerManager.instance.UpdateDaylight();
    }

    public IEnumerator DelayedPulse()
    {
        yield return new WaitForSeconds(watchDelay);
        watchSound.Play();

        GameObject leftHand = VRTK_SDKManager.instance.scriptAliasLeftController;
        VRTK_ControllerHaptics.TriggerHapticPulse(VRTK.VRTK_ControllerReference.GetControllerReference(leftHand), watchSound.clip);

    }

    public void DoUpdateWatchTime(object sender, DestinationMarkerEventArgs e)
    {
        int additionalTime = UnityEngine.Random.Range(30, 40);
        UpdateWatchTime(additionalTime);
    }

    
}
