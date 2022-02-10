using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePLatform : MonoBehaviour
{
    // Start is called before the first frame update    private float PlayerSpeed;
    private float PlatformSpeed;
    private void Start() {
        PlatformSpeed =0.1f;
    }

    private void Update() {
        Vector3 movePLatform = new Vector3(0f,0f,PlatformSpeed);

        transform.position += movePLatform;
    }
}
