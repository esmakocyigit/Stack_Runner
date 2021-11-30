using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    #region Variables

    public static PlayerController Instance;

    public float forwardSpeed;
    private float firstSpeed;
    [SerializeField] LayerMask ground;
    [SerializeField] LayerMask groundFinal;
    [SerializeField] Transform rayPos;
    [SerializeField] Animator m_Animator;

    public bool winGame =false;
    #endregion

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        Instance = this;
        firstSpeed = forwardSpeed;
    }

    private void Update()
    {
        if (GameManager.Instance.isGameStarted)
            Running();
    }

    #endregion

    #region Other Methods

    private void Running()
    {
        transform.Translate(transform.forward * forwardSpeed * Time.deltaTime, Space.World);
        GroundCheck();
    }

    private void GroundCheck()
    {
        RaycastHit hit;

        if (Physics.Raycast(rayPos.position, transform.TransformDirection(Vector3.down), out hit, 100, ground))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.red);
        }
       
        else
        {
          
            GameManager.Instance.GameOver();
            StartCoroutine(nameof(Fail));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag =="Final")
        {
            Win();
        }
        if (other.gameObject.tag == "Respawn")
        {
            winGame = true;
            
        }


    }
    public void Game()
    {
        winGame = false;
        
      
        StartCoroutine(nameof(Speed));
    }

    private void Win()
    {
        forwardSpeed = 0;
        firstSpeed += .1f;
        m_Animator.SetTrigger("win");
    }

    IEnumerator Fail()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
    }

    IEnumerator Speed()
    {
        yield return new WaitForSeconds(.1f);
        m_Animator.SetTrigger("run");
        forwardSpeed = firstSpeed;
    }
    #endregion
}
