using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float camSpeed;
    public Transform playerTrans;
    public float zAxisOffset;
    public float yAxisOffset;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 camPos = playerTrans.position;
        camPos.z = playerTrans.position.z + zAxisOffset;
        camPos.y = playerTrans.position.y + yAxisOffset;
        transform.position = Vector3.Lerp(transform.position, camPos, camSpeed * Time.deltaTime);
    }
}
