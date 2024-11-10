using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseRotation : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.Rotate(0,0.8f,0);
    }
}
