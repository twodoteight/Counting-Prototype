using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool isCounted = false;
    public bool isHeld = false;

    private Rigidbody projectileRb;

    // Start is called before the first frame update
    void Start()
    {
        projectileRb = GetComponent<Rigidbody>();
        StartCoroutine(LifeTime());
    }

    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(15f);
        if (!isCounted && !isHeld)
        {
            Destroy(gameObject);
        }
    }
}
