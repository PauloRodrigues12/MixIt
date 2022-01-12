using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageLookAtPlayer : MonoBehaviour
{
    void Update()
    {
        transform.rotation = Quaternion.Euler(35, 0, 0);
    }
}
