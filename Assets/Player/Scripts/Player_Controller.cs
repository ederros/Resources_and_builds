using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player_Controller : MonoBehaviour
{
    public Storage_Controller player_storage;
    [SerializeField] Joystick_Controller joystick;
    
    public float Res_Transport_delay = 1;

    Rigidbody rb;
    public static Player_Controller instance;
    public float movement_speed;
    public float rotation_speed;
    void Start()
    {
        instance = this;
        rb = GetComponent<Rigidbody>();
    }

    void Movement(float move_speed){
        Vector3 move = new Vector3(joystick.clamped_offset.x,0,joystick.clamped_offset.y);
        if(move==Vector3.zero) {
            rb.velocity = Vector3.zero;
            return;
        }
        rb.velocity = move*move_speed;

        transform.forward = Vector3.Lerp(transform.forward,move+new Vector3(Random.value,0,Random.value)/100,rotation_speed);
    }
    void Update()
    {
        Movement(movement_speed);
    }
}
