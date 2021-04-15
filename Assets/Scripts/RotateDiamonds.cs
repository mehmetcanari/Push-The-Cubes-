using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotateDiamonds : MonoBehaviour
{
    private float rotSpeed = 0.025f;

    void Update() 
    {
        transform.Rotate(new Vector3(0,0,10) * rotSpeed);
    }
}
