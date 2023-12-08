using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour
{
    public int bouncePwr = 20;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Rigidbody rb = other.transform.GetComponent<Rigidbody>();
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(transform.up * bouncePwr, ForceMode.Impulse);
        }
    }
}
