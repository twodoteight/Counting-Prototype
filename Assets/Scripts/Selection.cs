using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selection : MonoBehaviour
{
    [SerializeField] Camera eyes;
    [SerializeField] Transform handPos;
    [SerializeField] bool charging;
    [SerializeField] float throwForce;
    [SerializeField] float baseThrowForce = 5f;
    [SerializeField] float maxThrowForce = 30f;
    [SerializeField] float chargeSpeed = 2f;

    public Slider strengthSlider;

    private GameObject selectedProjectile;
    private Vector2 screenCenter;

    // Start is called before the first frame update
    void Start()
    {
        // Get the center of the screen for ray casting
        screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
    }

    // Update is called once per frame
    void Update()
    {
        // Create a gaze ray pointing forward from the camera
        if (Input.GetMouseButtonDown(0))
        {
            if (selectedProjectile == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(eyes.ScreenPointToRay(screenCenter), out hit, 10) && hit.collider.tag == "Projectile")
                {
                    // If clicked on a projectile, set it as the selected object
                    Debug.Log("ITS A PROJECTILE!!");
                    selectedProjectile = hit.collider.gameObject;
                    selectedProjectile.GetComponent<Rigidbody>().isKinematic = true;
                    selectedProjectile.GetComponent<Projectile>().isHeld = true;
                    Debug.Log(hit.collider.gameObject);
                }
            }

            else
            {
                charging = true;
                strengthSlider.gameObject.SetActive(true);
            }
        }

        if(charging && selectedProjectile != null)
        {
            // As long as the player holds down the left click, increase the force
            if (Input.GetMouseButton(0) && throwForce < maxThrowForce)
            {
                throwForce += chargeSpeed * Time.deltaTime;
                strengthSlider.value = throwForce / maxThrowForce;
            }

            // Throw the projectile
            if (Input.GetMouseButtonUp(0))
            {
                selectedProjectile.GetComponent<Rigidbody>().isKinematic = false;
                selectedProjectile.GetComponent<Projectile>().isHeld = false;

                selectedProjectile.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * throwForce, ForceMode.Impulse);
                throwForce = baseThrowForce;

                selectedProjectile = null;
                charging = false;
                strengthSlider.gameObject.SetActive(false);
            }
        }

        // If there is a selected object, place and hold it at the proper position
        if (selectedProjectile != null)
        {
            selectedProjectile.transform.position = handPos.transform.position;
            float sphereRotX = eyes.transform.localEulerAngles.x;
            float sphereRotY = transform.localEulerAngles.y;
            selectedProjectile.transform.rotation = Quaternion.Euler(eyes.transform.eulerAngles - (Vector3.right * 30));
        }

        if (selectedProjectile == null && strengthSlider.gameObject.activeInHierarchy)
        {
        }
    }
}
