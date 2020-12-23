using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float scrollSpeed = 400.0f;

    [SerializeField]
    private float mouseScrollSpeed = 1000.0f;

    [SerializeField]
    private float zoomSpeed = 500.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            transform.position += getDirection() * scrollSpeed * Time.deltaTime;
        }

        if (Input.GetMouseButton(2))
        {
            transform.position += getMouseDirection() * mouseScrollSpeed * Time.deltaTime;
        }

        if (Input.mouseScrollDelta.y != 0)
        {
            transform.Translate(Vector3.forward * Input.mouseScrollDelta.y * zoomSpeed * Time.deltaTime);
        }
    }

    private Vector3 getMouseDirection()
    {
        float translationX = Input.GetAxis("Mouse X") * -1;
        float translationY = Input.GetAxis("Mouse Y") * -1;
        Vector3 vector3 = new Vector3(translationX, 0, translationY);
        return vector3;
    }

    private Vector3 getDirection()
    {
        Vector3 directionVector = Vector3.zero;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            directionVector += Vector3.right;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            directionVector += Vector3.right * -1;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            directionVector += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            directionVector += Vector3.forward * -1;
        }
        return directionVector;
    }
}
