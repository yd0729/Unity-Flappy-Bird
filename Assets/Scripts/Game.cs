using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject PanelReady;
    public GameObject PanelRunning;
    public GameObject PanelEnd;

    public PipelineManager pipelineManager;

    public Player player;

    public void Set()
    {
        Time.timeScale = 1;
        PanelReady.SetActive(true);
        _Status = GameStatus.Ready;
        player.Set();
        pipelineManager.Set();
    }

    public void Run()
    {
        _Status = GameStatus.Running;
        pipelineManager.Run();
        player.Run();
    }

    enum GameStatus
    {
        Ready,
        Running,
        End,
    }

    GameStatus _status;

    GameStatus _Status
    {
        get => _status;
        set
        {
            _status = value;
            PanelReady.SetActive(_status == GameStatus.Ready);
            PanelRunning.SetActive(_status == GameStatus.Running);
            PanelEnd.SetActive(_status == GameStatus.End);
        }
    }

    void Start()
    {
        Set();
        player.OnDie += OnPlayerDie;
    }

    void Update()
    {

    }

    void OnPlayerDie()
    {
        _Status = GameStatus.End;
        pipelineManager.Stop();
        Time.timeScale = 0;
    }
}
