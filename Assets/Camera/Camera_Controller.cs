using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    [SerializeField] bool based_on_start_pos;
    public Transform target;

    [Range(0,1)]
    public float speed;

    Vector3 start_pos = Vector3.zero;
    void Start()
    {
        if(based_on_start_pos) start_pos = transform.position;
    }

    private void FixedUpdate() {
        transform.position = Vector3.Lerp(transform.position,target.position+start_pos,speed);
    }
}
