using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_Storage_Controller : Storage_Controller
{
    int ing_ind = 0;// index of ingedients
    protected override void On_Player_In()
    {
        for(int i =0;i<my_build.ingredients.Count;i++){
            
            if(!Player_Controller.instance.player_storage.is_mathing(my_build.ingredients[i].name)) continue;
            Player_Controller.instance.player_storage.Sub_Res(my_build.ingredients[i].name,this);
            break;
        }
    }
}
