using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transporters_Manager : MonoBehaviour
{
    public float transporters_speed;
    void Start()
    {
        Transponter_Controller.transporters_parent = this.transform;
        Transponter_Controller.manager = this;
    }
}
