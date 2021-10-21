using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float sensitivity = 3;
    public float handsSpeed = 4;

    [SerializeField] Transform hands;
    [SerializeField] Transform eyes;
    [SerializeField] Vector3 restingHandOffset = new Vector3(0, -0.5f, 1);
    [SerializeField] float verticalAngleLimit = 80f;

    private Vector3 rotation;
    private bool didStart = false;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rotation = transform.rotation.eulerAngles;
        StartCoroutine(StartDelay());
    }

    // Update is called once per frame
    void Update()
    {
        if (didStart)
        {
            rotation.y += Input.GetAxis("Mouse X") * sensitivity;
            rotation.x -= Input.GetAxis("Mouse Y") * sensitivity;
            rotation.x = Mathf.Clamp(rotation.x, -verticalAngleLimit, verticalAngleLimit);

            transform.eulerAngles = new Vector3(0, rotation.y, 0);
            eyes.localRotation = Quaternion.Euler(rotation.x, 0, 0);

            // Keep the hand position independent of the eyes if looking down
            // Rotate vector around pivot
            Vector3 handOffset;

            if (rotation.x < 0)
            {
                handOffset = eyes.rotation * restingHandOffset;
                // Calculate the rotated vector
            }
            else
            {
                handOffset = Quaternion.Euler(0, eyes.rotation.eulerAngles.y, 0) * restingHandOffset;
            }

            Vector3 handsDestination = handOffset + transform.position;
            hands.position = Vector3.Lerp(hands.position, handsDestination, handsSpeed * Time.deltaTime);
        }
    }
    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(2f);
        didStart = true;
    }
}