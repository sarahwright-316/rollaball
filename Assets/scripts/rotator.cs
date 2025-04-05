using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class rotator : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime); // multiplication for smoothness

        
    }
}
