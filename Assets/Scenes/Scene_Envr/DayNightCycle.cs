using UnityEngine;
using System;
using TMPro;

public class DayNightCycle : MonoBehaviour
{
    public float time;
    public TimeSpan currenttime;
    public Transform SunTransform;
    public Light Sun;
    public TMP_Text timetext;
    public int days;

    public float intensity;
    public Color fogday = Color.gray;
    public Color fognight = Color.black;

    public int speed;

    private float magneticHeading;

    void Start()
    {
        Input.compass.enabled = true;
    }

    void Update()
    {
        ChangeTime();
    }

    public void ChangeTime()
    {
        time += Time.deltaTime * speed;
        if (time > 86400)
        {
            days += 1;
            time = 0;
        }

        currenttime = TimeSpan.FromSeconds(time);
        string[] temptime = currenttime.ToString().Split(":"[0]);
        timetext.text = temptime[0] + ":" + temptime[1];

        magneticHeading = Input.compass.trueHeading;

        float sunAngle = (time - 21600) / 86400 * 360;
        SunTransform.rotation = Quaternion.Euler(new Vector3(sunAngle, magneticHeading, 0));

        if (time > 43200)
            intensity = 1 - (43200 - time) / 43200;
        else
            intensity = 1 - ((43200 - time) / 43200 * -1);

        RenderSettings.fogColor = Color.Lerp(fognight, fogday, intensity * intensity);
        Sun.intensity = intensity;
    }
}
