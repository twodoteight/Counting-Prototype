using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour {

    public Text CounterText;
    private int Count = 0;
    private Type.ColorType type;
    private void Start()
    {
        Count = 0;
        type = GetComponent<Type>().colorType;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Projectile") && other.GetComponent<Type>().colorType == type && !other.GetComponent<Projectile>().isCounted)
        {
            other.gameObject.GetComponent<Projectile>().isCounted = true;
            Count += 1;
            CounterText.text = type + ": " + Count;
        }
    }
}
