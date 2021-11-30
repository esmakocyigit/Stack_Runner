using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private MovingCube cubePrefab;
    [SerializeField] private MoveDirection moveDirection;
   

    public void SpawnCube()
    {
        var cube = Instantiate(cubePrefab);

        if (MovingCube.LastCube != null)
        {
            if (moveDirection== MoveDirection.X)
                 cube.transform.position = new Vector3(transform.position.x-2, MovingCube.LastCube.transform.position.y, MovingCube.LastCube.transform.position.z + 2.67322f);
            else
                cube.transform.position = new Vector3(transform.position.x + 2, MovingCube.LastCube.transform.position.y, MovingCube.LastCube.transform.position.z + 2.67322f);



        }

        cube.MoveDirection = moveDirection;
    }

}

public enum MoveDirection
{
    X,
    NegativeX
}
