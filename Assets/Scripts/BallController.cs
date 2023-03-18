using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public AudioSource takTak;
    private bool ignoreNextCollision;
    public Rigidbody rb;
    public float impulsForce = 0.5f;
    private Vector3 startPos;

    public int perfectPass = 0;
    public bool isSuperSpeedActive;
    // Start is called before the first frame update
    private void Start()
    {
        takTak = GetComponent<AudioSource>();
    }
    void Awake()
    {
        startPos = transform.position;
    }
    private void OnCollisionEnter(Collision collision)
    {
        takTak.Play();
        if (ignoreNextCollision)
            return;

        if(isSuperSpeedActive)
        {
            if(!collision.transform.GetComponent<Goal>())
            {
                Destroy(collision.transform.gameObject);
                Debug.Log("Part distroy");
            }
        }
        else
        {
            //adding restart level after death part
            DeathPart deathPart = collision.transform.GetComponent<DeathPart>();
            if (deathPart)
                deathPart.HitDeathPart();
        }
        
       // Debug.Log("Ball touched something");
        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * impulsForce, ForceMode.Impulse);
        ignoreNextCollision = true;
        Invoke("AllowCollision", .1f);

        perfectPass = 0;
        isSuperSpeedActive = false;
 
    }
    private void Update()
    {
        if(perfectPass >= 5 && isSuperSpeedActive)
        {
            isSuperSpeedActive = true;
            rb.AddForce(Vector3.down * 0.1f, ForceMode.Impulse);

        }
    }
    private void AllowCollision()
    {
        ignoreNextCollision = false;
    }
    public void ResetBall()
    {
        transform.position = startPos;
    }

}