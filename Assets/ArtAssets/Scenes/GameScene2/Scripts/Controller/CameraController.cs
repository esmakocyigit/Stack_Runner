using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject playerParent;
 
    private void Update()
    {

        if (PlayerController.Instance.forwardSpeed == 0)
        {
            transform.Rotate(0, 60 * Time.deltaTime, 0);
            player.transform.rotation = Quaternion.Euler(0, 0, 0);
            playerParent.transform.rotation = Quaternion.Euler(0, 0, 0);
            playerParent.transform.position = new Vector3(0, 0, playerParent.transform.position.z);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            player.transform.rotation = Quaternion.Euler(0, 0, 0);
            playerParent.transform.rotation = Quaternion.Euler(0, 0, 0);
            playerParent.transform.position = new Vector3(0, 0, playerParent.transform.position.z);

        }
    }
}
