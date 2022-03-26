using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class TimerTick : MonoBehaviour
{
    public float time = 600f;
    private Text text;
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
        }
        else if (time < 0 )
        {
            time = 0;
        }

        text.text = math.floor(time / 60).ToString("F0") + ":"+(time % 60).ToString("F0");
    }
}
