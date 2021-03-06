using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [HideInInspector] public bool isGameStarted;
    public bool isStart;

    private CubeSpawner[] spawners;
    private int spawnerIndex;
    private CubeSpawner currentSpawner;

    public override void Awake()
    {
        base.Awake();
        spawners = FindObjectsOfType<CubeSpawner>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") || isStart == true)
        {
            if (MovingCube.CurrentCube != null && MovingCube.LastCube != null )
            {
                MovingCube.CurrentCube.Stop();
            }

            spawnerIndex = spawnerIndex == 0 ? 1 : 0;

            currentSpawner = spawners[spawnerIndex];

            if (PlayerController.Instance.winGame == false)
            {
                currentSpawner.SpawnCube();
            }

            isStart = false;
        }
    }

    public void StartGame()
    {
        isGameStarted = true;
    }

    public void GameOver()
    {
        isGameStarted = false;
    }
}
