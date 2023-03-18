using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassCheck : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        GameManager.singleton.AddScore(5);

        FindObjectOfType<BallController>().perfectPass++;
        Debug.Log("Perfect pass is increased");
    }
}