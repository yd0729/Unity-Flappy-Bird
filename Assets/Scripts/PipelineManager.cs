using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipelineManager : MonoBehaviour
{
    public GameObject PipelinePrefab;
    public float gap;

    public void Set()
    {
        foreach (var pipeline in _pipelines)
        {
            pipeline.Set();
        }
    }

    public void Run()
    {
        _coroutine = StartCoroutine(RunPipelines());
    }

    public void Stop()
    {
        StopCoroutine(_coroutine);
    }

    List<Pipeline> _pipelines;
    Coroutine _coroutine = null;

    void Start()
    {
        _pipelines = new();

        for (int i = 0; i < 3; i += 1)
        {
            GameObject go = Instantiate(PipelinePrefab, transform);
            Pipeline pipeline = go.GetComponent<Pipeline>();
            _pipelines.Add(pipeline);
        }
    }

    IEnumerator RunPipelines()
    {
        foreach (var pipeline in _pipelines)
        {
            pipeline.Run();
            yield return new WaitForSeconds(gap);
        }
    }
}
