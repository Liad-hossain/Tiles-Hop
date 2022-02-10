using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    // Start is called before the first frame update
    private float CameraSpeed;

    private void Start() {
        CameraSpeed =0.1f;
    }

    private void Update() {
        Vector3 moveCamera = new Vector3(0f,0f,CameraSpeed);

        transform.position += moveCamera;
    }
}
