using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direction : MonoBehaviour
{

    public GameObject directionIndicator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float playerYRotation = transform.eulerAngles.y;
        directionIndicator.transform.localRotation = Quaternion.Euler(0f, playerYRotation, 0f);
    }

}
