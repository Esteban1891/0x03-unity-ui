using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    Vector3 offset = new Vector3(0, 26, -10);

    void Start()
    {
        transform.Rotate(-10, 0, 0);
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = player.transform.position;
        transform.position = playerPos + offset;
    }
}
