using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Storage_Controller : Storage_Controller
{
    protected override void On_Awake(){
        base.On_Awake();
        transform.position += (transform.parent.up*(Width_Height.y)-transform.parent.forward*(Width_Height.x))*res.transform.lossyScale.x/2;
    }
    
    
    
}
