using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    [SerializeField] private float delay; //This implies a delay of x seconds.

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    private void Update()
    {
        Destroy(gameObject, delay);
    }
}
