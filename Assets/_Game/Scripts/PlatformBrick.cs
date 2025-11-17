using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlarformBrick : MonoBehaviour
{
    //[SerializeField] private GameObject goBrickPrefab;
    [SerializeField] private MeshRenderer meshRenderer;
    private bool isCollected = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isCollected)
        {
           
            isCollected = true;
            //goBrickPrefab.SetActive(false);
            meshRenderer.enabled = false;
            other.GetComponent<Player>().AddBrick();
        }
    }
}

