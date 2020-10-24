using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour

{
    public GameObject player;
    public float interpSpeed;
    private Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        targetPos = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, targetPos, interpSpeed);
    }
}

