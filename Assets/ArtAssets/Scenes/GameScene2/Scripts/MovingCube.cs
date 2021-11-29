using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MovingCube : MonoBehaviour
{
    public static MovingCube CurrentCube { get; private set; }
    public static MovingCube LastCube { get; private set; }
    public MoveDirection MoveDirection { get;  set; }

    [SerializeField] private float moveSpeed = 1f;


    private void OnEnable()
    {
        if (LastCube == null)
        {
            LastCube = GameObject.Find("Start").GetComponent<MovingCube>();

        }

        CurrentCube = this;
        GetComponent<Renderer>().material.color = GetRandomCokor();


        transform.localScale = new Vector3(LastCube.transform.localScale.x, LastCube.transform.localScale.y, LastCube.transform.localScale.z);

    }

    private Color GetRandomCokor()
    {
        return new Color(UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f));
    }

    internal void Stop()
    {
        moveSpeed = 0;

        float hangover = transform.position.x - LastCube.transform.position.x;

        if (Mathf.Abs(hangover) >= LastCube.transform.localScale.x)
        {
            LastCube = null;
            CurrentCube = null;
            SceneManager.LoadScene(0);
        }

        float direction = hangover > 0 ? 1f : -1f;

        SplitCubeOnX(hangover, direction);
        LastCube = this;

    }

    private void SplitCubeOnX(float hangover, float direction)
    {
        if (LastCube != null)
        {

            float newXPosition = transform.position.x - (hangover / 2);
            float newSize = LastCube.transform.localScale.x - Math.Abs(hangover);
            float fallingBlockSize = transform.localScale.x - newSize;

            transform.localScale = new Vector3(newSize, transform.localScale.y, transform.localScale.z);
            transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);


            float cubeEdge = transform.position.x + (newSize / 2f * direction);
            float fallingBlockXPosition = cubeEdge + fallingBlockSize / 2f * direction;

            SpawnDropCube(fallingBlockXPosition, fallingBlockSize);
        }


    }

    private void SpawnDropCube(float fallingBlockXPosition, float fallingBlockSize)
    {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.localScale = new Vector3(fallingBlockSize, transform.localScale.y, transform.localScale.z);
        cube.transform.position = new Vector3(fallingBlockXPosition, transform.position.y, transform.position.z);

        cube.AddComponent<Rigidbody>();
        cube.GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;
        Destroy(cube.gameObject, 1f);
    }

    private void Update()
    {
        if (MoveDirection == MoveDirection.X)
            transform.position += transform.right * Time.deltaTime * moveSpeed;
        else
            transform.position -= transform.right * Time.deltaTime * moveSpeed;
     
    }
}
