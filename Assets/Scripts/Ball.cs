using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public static Ball Instance;
    #region Properties
    public bool Pushed
    {
        get { return wasPushed; }
        set
        {
            if (wasPushed)
            {
                return;

            }
            wasPushed = value;
            StartCoroutine(PushClear());
        }
    }
    public bool IsSuperBall
    {
        get { return isSuperBall; }
        set
        {
            isSuperBall = value;
            if (isSuperBall)
            {
                ballMat.color = superColor;
                trailMat.color = superColor;
            }
            else
            {
                ballMat.color = normalColor;
                trailMat.color = normalColor;
            }
        }
    }
    #endregion

    #region Settings
    [SerializeField] Rigidbody rb;
    [SerializeField] float force = .4f;
    [SerializeField] float superSpeed = 40f;
    [SerializeField] Material ballMat;
    [SerializeField] Material trailMat;
    [SerializeField] Color normalColor;
    [SerializeField] Color superColor;
    #endregion

    #region Variables
    WaitForSeconds pushClearWait;
    Vector3 startingPosition;
    bool isSuperBall;
    bool wasPushed;
    #endregion

    #region MonoBehavior Methods
    private void Awake()
    {
        Instance = this;
        startingPosition = transform.position;
        pushClearWait = new WaitForSeconds(.1f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Final"))
        {
            //success!
            Debug.Log("Level pass!!");
            IsSuperBall = false;
            GameManager.Instance.LevelPass();
            return;
        }
        if (wasPushed)
        {
            return;
        }
        if (isSuperBall)
        {
            IsSuperBall = false;
            Pushed = true;
            rb.AddForce(-Physics.gravity * force, ForceMode.VelocityChange);
            collision.gameObject.GetComponentInParent<PlatformTile>().FallApart();
            return;
        }
        if (collision.gameObject.CompareTag("Fail"))
        {
            //Fail!!
            Debug.Log("Game Over!!");
            VFXManager.Instance.FailSplash(collision.contacts[0].point);
            GameManager.Instance.GameOver();
            return;
        }
        if (collision.gameObject.CompareTag("Safe"))
        {
            Pushed = true;
            rb.AddForce(-Physics.gravity * force, ForceMode.VelocityChange);
            VFXManager.Instance.Splash(collision.contacts[0].point, collision.transform);
        }
    }
    private void Update()
    {

        if (!isSuperBall && rb.velocity.magnitude > superSpeed)
        {
            IsSuperBall = true;
        }
    }
    #endregion

    #region Helper Methos
    public void Restart()
    {
        IsSuperBall = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = startingPosition;
    }
    IEnumerator PushClear()
    {
        yield return pushClearWait;
        wasPushed = false;
    }
    #endregion
}
