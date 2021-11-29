using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    

    private void Update()
    {

        if (PlayerController.Instance.forwardSpeed==0)
        {
            transform.Rotate(0,   60 * Time.deltaTime, 0);
        }
    }
}
