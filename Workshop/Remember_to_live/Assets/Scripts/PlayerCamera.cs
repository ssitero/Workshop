using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private string xInput, yInput;
    [SerializeField] private Transform playerBody;

    [SerializeField] private float minimumX = -90f;
    [SerializeField] private float maximumX = 90f;

    private float xClamp;

    public float sensitivity = 2f;


    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        xClamp = 0.0f;
    }

    public void Update()
    {
        CameraRotate();
    }

    private void CameraRotate()
    {
        float xRot = Input.GetAxis(xInput) * sensitivity * Time.deltaTime;
        float yRot = Input.GetAxis(yInput) * sensitivity * Time.deltaTime;

        xClamp += yRot;

        if (xClamp > maximumX)
        {
            xClamp = 90f;
            yRot = 0.0f;
            //
        }
        else if (xClamp < minimumX)
        {
            xClamp = -90f;
            yRot = 0.0f;
            //
        }

        transform.Rotate(Vector3.left * yRot);
        playerBody.Rotate(Vector3.up * xRot);
    }

    private void XClampToValue(float val)
    {
        Vector3 eulerRotation = transform.eulerAngles;
        eulerRotation.x = val;
        transform.eulerAngles = eulerRotation;
    }
}
