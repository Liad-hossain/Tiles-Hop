using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "diamond")
        {
            Destroy(other.gameObject);
        }

        if(other.gameObject.tag == "5star")
        {
            Destroy(other.gameObject);
        }
    }

}
