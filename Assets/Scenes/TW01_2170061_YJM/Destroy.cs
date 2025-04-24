using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        print($"{collision.gameObject.name} is destroyed");
        Destroy(collision.gameObject);
    }
}
