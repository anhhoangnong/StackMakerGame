using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 18f;
    Vector3 offset; // Khoảng cách giữa camera và đối tượng mục tiêu

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.position; // Tính toán offset ban đầu giữa camera và đối tượng mục tiêu
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * speed); 
    }
}
