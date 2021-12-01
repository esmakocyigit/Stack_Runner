using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : Singleton<PlayerController>
{
    #region Variables
    [Header("Movement Settings")]
    public float forwardSpeed;
    private float firstSpeed;

    [Header("Ground Check Elements")]
    [SerializeField] LayerMask ground;
    [SerializeField] LayerMask groundFinal;
    [SerializeField] Transform rayPos;

    [Header("Animation Settings")]
    [SerializeField] Animator m_Animator;

    [HideInInspector] public bool winGame = false;
    bool canPlayRunningAnimation;
    #endregion

    #region MonoBehaviour Callbacks

    public override void Awake()
    {
        base.Awake();
        firstSpeed = forwardSpeed;
    }

    private void Update()
    {
        if (GameManager.Instance.isGameStarted)
        {
            if (!canPlayRunningAnimation)
            {
                canPlayRunningAnimation = true;
                m_Animator.SetTrigger("start");
            }
            Running();
        }
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
        if (other.gameObject.tag == "Final")
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
        yield return new WaitForSeconds(.3f);
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
