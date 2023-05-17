using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KnifeThrower : MonoBehaviour
{
    Vector2 startMousePos;
    Vector2 endMousePos;
    Vector2 throwDir;

    public float throwPower;
    public float throwTorque;

    Rigidbody rb;

    float timeSinceWeFly;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            startMousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        }

        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            endMousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            throwDir = endMousePos - startMousePos;
            ThrowKnife();
        }
    }

    void ThrowKnife()
    {
        timeSinceWeFly = Time.time;
        rb.useGravity = true;

        rb.AddForce(throwDir * throwPower, ForceMode.Impulse);
        rb.AddTorque(0, 0, throwTorque, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(Time.time - timeSinceWeFly < 0.1f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if(other.CompareTag("Platform"))
        {
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
