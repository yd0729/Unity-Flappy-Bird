using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pipeline : MonoBehaviour
{
    public float Speed;
    public float MinY, MaxY;
    public float CycleTime;

    public void Set()
    {
        time = 0;
        float y = Random.Range(MinY, MaxY);
        transform.localPosition = new(0, y, 0);
        running = false;
    }

    public void Run()
    {
        running = true;
    }

    //public void Stop()
    //{
    //    running = false;
    //}

    float time;
    bool running;

    void Update()
    {
        if (running)
        {
            transform.Translate(new(-Speed * Time.deltaTime, 0, 0));
            time += Time.deltaTime;
            if (time > CycleTime)
            {
                time = 0;
                Set();
                Run();
            }
        }
    }
}
