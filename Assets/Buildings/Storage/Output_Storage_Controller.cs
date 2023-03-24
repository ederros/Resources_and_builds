using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Output_Storage_Controller : Storage_Controller
{
    
    protected override void On_Player_In()
    {
        if(!Sub_Res(Player_Controller.instance.player_storage)) return;
    }
}
