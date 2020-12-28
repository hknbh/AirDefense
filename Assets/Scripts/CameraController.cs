using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float scrollSpeed = 400.0f;

    [SerializeField]
    private float mouseScrollSpeed = 1000.0f;

    [SerializeField]
    private float zoomSpeed = 500.0f;


    [SerializeField]
    private Camera radarCamera;

    [SerializeField]
    private GameObject radarImage;

    private RectTransform radarImageRectTransform;


    // Start is called before the first frame update
    void Start()
    {
        radarImageRectTransform = radarImage.GetComponent<RectTransform>();
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

    public void minimapClicked(BaseEventData data)
    {
        PointerEventData eventData = (PointerEventData)data;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(radarImageRectTransform, eventData.position, null, out Vector2 localClick);
        //local x&y has minus values therefore move them to positive values
        localClick.x += radarImageRectTransform.rect.width / 2;
        localClick.y += radarImageRectTransform.rect.height / 2;
        Vector2 viewportClick = new Vector2(localClick.x / radarImageRectTransform.rect.width, localClick.y / radarImageRectTransform.rect.height);
        Ray ray = radarCamera.ViewportPointToRay(new Vector3(viewportClick.x, viewportClick.y, 0));
        //if there is a hit in minimap
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            //find the center point of main camera to calculate z diff
            bool cameraHit = Physics.Raycast(transform.position, transform.forward, out RaycastHit raycastHit);
            if (cameraHit)
            {
                float zDiff = raycastHit.point.z - transform.position.z;
                transform.position = new Vector3(hit.point.x, transform.position.y, hit.point.z - zDiff);
            }
        }
        else
        {
            Debug.LogError("Minimap click miss");
        }

    }
}
