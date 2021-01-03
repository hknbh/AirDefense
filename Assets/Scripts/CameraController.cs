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
    private float mouseMoveSpeed = 1000.0f;

    [SerializeField]
    private float zoomSpeed = 500.0f;

    [SerializeField]
    private float rotationSpeed = 40.0f;


    [SerializeField]
    private Camera radarCamera;

    [SerializeField]
    private GameObject radarImage;

    [SerializeField]
    private GameObject radarLineRenderer;
    private LineRenderer lineRenderer;

    private RectTransform radarImageRectTransform;


    // Start is called before the first frame update
    void Start()
    {
        radarImageRectTransform = radarImage.GetComponent<RectTransform>();
        lineRenderer = radarLineRenderer.GetComponent<LineRenderer>();
        lineRenderer.sortingOrder = 1;
        lineRenderer.startWidth = 20;
        lineRenderer.endWidth = 20;
        lineRenderer.positionCount = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Vector3 p = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, Camera.main.farClipPlane));
            bool isHit = Physics.Raycast(transform.position, p - transform.position, out RaycastHit rayCastHit);
            if (isHit)
            {
                float xChange = Input.GetAxis("Mouse X") * -1;
                RaycastHit[] rayCastHits = new RaycastHit[4];
                Quaternion oldRotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
                transform.RotateAround(rayCastHit.point, Vector3.up, xChange * rotationSpeed * Time.deltaTime);
                if (!testNewFrameWithPosition(transform.position, rayCastHits))
                {
                    transform.rotation = oldRotation;
                }
                else
                {
                    updateMiniMapFrame(rayCastHits);
                }
            }

        }
        else if (Input.anyKey || Input.mouseScrollDelta.y != 0)
        {
            Vector3 positionChange = new Vector3();
            if (Input.anyKey)
            {
                positionChange = getDirection() * scrollSpeed * Time.deltaTime;
            }

            if (Input.GetMouseButton(2))
            {
                positionChange = getMouseDirection() * mouseMoveSpeed * Time.deltaTime;
            }

            if (Input.mouseScrollDelta.y != 0)
            {
                positionChange = Vector3.forward * Input.mouseScrollDelta.y * zoomSpeed * Time.deltaTime;
            }
            RaycastHit[] rayCastHits = new RaycastHit[4];
            Vector3 oldPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            transform.Translate(positionChange);
            if (!testNewFrameWithPosition(transform.position, rayCastHits))
            {
                transform.position = oldPosition;
            }
            else
            {
                updateMiniMapFrame(rayCastHits);
            }
        }
    }

    private Vector3 getMouseDirection()
    {
        float translationX = Input.GetAxis("Mouse X") * -1;
        float translationY = Input.GetAxis("Mouse Y") * -1;
        Vector3 vector3 = new Vector3(translationX, translationY, translationY);
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
            directionVector += Vector3.up + Vector3.forward;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            directionVector += (Vector3.up + Vector3.forward) * -1;
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
                Vector3 newPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z - zDiff);
                RaycastHit[] rayCastHits = new RaycastHit[4];
                if (testNewFrameWithPosition(newPosition, rayCastHits))
                {
                    transform.position = newPosition;
                    updateMiniMapFrame(rayCastHits);
                }
            }
        }
        else
        {
            Debug.LogError("Minimap click miss");
        }
    }

    //Updates the view frame in the minimap view
    private void updateMiniMapFrame(RaycastHit[] rayCasts)
    {
        Vector3[] positions = new Vector3[5];
        positions[0] = rayCasts[0].point;
        positions[4] = rayCasts[0].point;
        positions[1] = rayCasts[1].point;
        positions[2] = rayCasts[2].point;
        positions[3] = rayCasts[3].point;
        lineRenderer.SetPositions(positions);
    }

    //returns false if the viewport is displaying out of the terrain
    //by testing 4 corners of the viewport and if the rays are still colliding with the terrain
    private bool testNewFrameWithPosition(Vector3 position, RaycastHit[] rayCasts)
    {
        Vector3 p = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.farClipPlane));
        bool isHit = Physics.Raycast(position, p - position, out rayCasts[0]);
        p = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, Camera.main.farClipPlane));
        isHit &= Physics.Raycast(position, p - position, out rayCasts[1]);
        p = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.farClipPlane));
        isHit &= Physics.Raycast(position, p - position, out rayCasts[2]);
        p = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, Camera.main.farClipPlane));
        isHit &= Physics.Raycast(position, p - position, out rayCasts[3]);
        return isHit;
    }

}
