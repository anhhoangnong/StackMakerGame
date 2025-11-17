using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{

    [SerializeField] private Transform tfStartPoint;


    public Transform TfStartPoint => tfStartPoint;

    public void Awake()
    {
        tfStartPoint.position = new Vector3(tfStartPoint.position.x, 0.25f, tfStartPoint.position.z);
    }    

}
