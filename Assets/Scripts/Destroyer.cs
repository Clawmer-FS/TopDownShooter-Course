 using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public float limeTime;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, limeTime);
    }

}
