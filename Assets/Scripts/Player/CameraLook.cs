using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [Header("Components")]
    public Transform player;


    [Header("Status")]
    [HideInInspector] public bool canMove = true;
    [HideInInspector] public bool lockCursor = true;

    [Header("Sensitivity")]
    [Range(1, 10)]
    public float sensitivity = 5f;

    [Header("Limits")]
    public float verticalTopLimit = -90f;
    public float verticalBottomLimit = 90f;


    //[HideInInspector]
    [HideInInspector]public float verticalRot = 0;
    [HideInInspector] public float horizontalRot = 0;

    [HideInInspector]public Quaternion cameraNextRecoilPoint;
    [HideInInspector] public Quaternion playerNextRecoilPoint;


    // Start is called before the first frame update
    void Start()
    {
        horizontalRot = player.transform.rotation.y;

        sensitivity *= 20;
    }

    // Update is called once per frame
    void Update()
    {

        GetMouseMovement();


        if (lockCursor)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        UpdateRecoilPosition();
    }

    void UpdateRecoilPosition()
    {
        if (!hasReachedVerticalRecoilPosition)
        {

            if (verticalRot < cameraNextRecoilPoint.x - 0.04f && Input.GetButton("Fire1") || verticalRot > cameraNextRecoilPoint.x + 0.04f && Input.GetButton("Fire1"))
            {
                verticalRot = Mathf.Lerp(verticalRot, cameraNextRecoilPoint.x, recoilSpeedMovement * Time.deltaTime + (Input.GetAxis("Mouse Y") / 1.25f));
            }
            else
            {
                hasReachedVerticalRecoilPosition = true;
            }

        }

        if (!hasReachedHorizontalRecoilPosition)
        {

            if (horizontalRot < cameraNextRecoilPoint.y + 0.04f && Input.GetButton("Fire1") || horizontalRot > cameraNextRecoilPoint.y - 0.04f && Input.GetButton("Fire1"))
            {
                horizontalRot = Mathf.Lerp(horizontalRot, cameraNextRecoilPoint.y, recoilSpeedMovement * Time.deltaTime + (Input.GetAxis("Mouse X") / 1.25f));
            }
            else
            {
                hasReachedHorizontalRecoilPosition = true;
            }

        }

    }

    float x;
    float y;

    void GetMouseMovement()
    {
        if (!canMove)
            return;




        x = Input.GetAxisRaw("Mouse X") * sensitivity * Time.deltaTime;
        y = -Input.GetAxisRaw("Mouse Y") * sensitivity * Time.deltaTime;



        verticalRot += y;
        horizontalRot += x;



        verticalRot = Mathf.Clamp(verticalRot, verticalTopLimit, verticalBottomLimit);
        transform.localRotation = Quaternion.Euler(verticalRot, 0, 0);
        player.rotation = Quaternion.Euler(0, horizontalRot, 0);
    }

    public bool hasReachedVerticalRecoilPosition = true;
    public bool hasReachedHorizontalRecoilPosition = true;
    public float recoilSpeedMovement;

    public void RecoilCamera(float vertical, float horizontal)
    {
        float verticalRecoilPoint = Random.Range(0.75f, vertical);
        float horizontalRecoilPoint = Random.Range(-horizontal, horizontal);

        if (verticalRot > verticalTopLimit)
            cameraNextRecoilPoint = new Quaternion(verticalRot - verticalRecoilPoint, horizontalRot + horizontalRecoilPoint, transform.localRotation.z, 1);

        if (verticalRot < verticalTopLimit)
            cameraNextRecoilPoint = new Quaternion(verticalTopLimit, horizontalRot + horizontalRecoilPoint, transform.localRotation.z, 1);

        

        hasReachedVerticalRecoilPosition = false;
        hasReachedHorizontalRecoilPosition = false;

        Debug.LogWarning("Horizontal Recoil is : " + horizontalRecoilPoint);

        //player.Rotate(Vector3.up * Random.Range(-horizontal, horizontal));

    }

}
