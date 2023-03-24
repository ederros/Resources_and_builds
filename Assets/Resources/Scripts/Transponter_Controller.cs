using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transponter_Controller : MonoBehaviour
{
    public static Transporters_Manager manager;
    public static Transform transporters_parent;
    //public static float speed = 0.1f;
    public Vector3 destination; 
    public Transform new_parent;
    float born_time;
    int index;
    public Storage_Controller target;
    public void Reborn(){
        born_time = Time.time;
        Destination_Recalculate(s => s!=null); // recalculates destination if target was exists
    }
    public static Transponter_Controller Create(Transform contained, Storage_Controller target, int index, Transform parent, Vector3 destination = new Vector3()){
        GameObject new_trans = contained.gameObject;
        Transponter_Controller TC = contained.GetComponent<Transponter_Controller>();
        if(TC == null) {
            new_trans.transform.SetParent(transporters_parent);
            TC = new_trans.AddComponent<Transponter_Controller>();
        }
        new_trans.transform.position = contained.position;
        new_trans.transform.rotation = contained.rotation;
        
        contained.parent = new_trans.transform;
        TC.index = index;
        TC.target = target;
        TC.destination = destination;
        
        //Debug.Log(destination+ " "+target);
        TC.new_parent = parent;
        TC.Reborn();
        return TC;
    }

    void Destination_Recalculate(System.Predicate<Storage_Controller> condition){ // recalculates a point of destination if condition is true
        if(condition(target)) destination = target.Destination_Calculate(index);
    }
    void move_to_dest(float speed){ // moves towards destination point
        Destination_Recalculate(s => s!=null&&!s.gameObject.isStatic); //recalculates destination every time if target not static
        transform.position = Vector3.Lerp(transform.position, destination, (Time.time - born_time)*speed);
        transform.rotation = Quaternion.Lerp(transform.rotation, new_parent.rotation, (Time.time - born_time)*speed);
    }

    void dest_check(){
        if(destination-transform.position == Vector3.zero){
            if(transform == new_parent) {
                Destroy(this.gameObject);
            }else{
              transform.parent = new_parent;
              Destroy(this);
            }
        }
    }

    void FixedUpdate()
    {
        move_to_dest(manager.transporters_speed);
        dest_check();
    }
}
