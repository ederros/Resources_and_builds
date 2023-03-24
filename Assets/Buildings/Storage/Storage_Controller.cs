using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public abstract class Storage_Controller : MonoBehaviour
{
    public Building_Controller my_build;
    public void Set_Target_Build(Building_Controller build){
        my_build = build;
    }
    public Vector2Int Width_Height;
    public GameObject res;
    public int free_space{get; protected set;}
    public int total_space{get; protected set;}
    public float padding;
    [SerializeField] float y_pos;

    #region Resource Managment
        protected List<Resource_controller> resources;
        public bool is_mathing(string name){
            return resources.Exists(a => a.name == name);
        }
        public Transponter_Controller Sub_Res(Storage_Controller target, Vector3 destination = new Vector3()){
            
            if (resources.Count == 0) return null;
            return Sub_Res(resources[resources.Count-1], target, destination);

        }
        public Transponter_Controller Sub_Res(string name, Storage_Controller target, Vector3 destination = new Vector3()){
            
            Resource_controller last = resources.FindLast(r => r.name == name);
            if(last == null) return null;
            return Sub_Res(last, target, destination);

        }

        void Move_Down(int index){//offsets each object after the index to the left by 1 
            for(int i = index+1;i < total_space - free_space;i++){
                Transponter_Controller.Create(resources[i].transform, this, i-1,transform);
            }
        }

        public Transponter_Controller Sub_Res(Resource_controller resource, Storage_Controller target, Vector3 destination = new Vector3()){
            if(free_space>=total_space) {
                //if(my_build != null) my_build.
                return null;
            }
            Transponter_Controller ret = null;
            Move_Down(resources.IndexOf(resource));
            if(target!=null){
                
                ret = target.Add_Res(resource);
                if((ret==null)) return null;
            }
            int index = total_space - free_space - 1;
            resources.Remove(resource);
            free_space++;
            if(destination != Vector3.zero) return Transponter_Controller.Create(resource.transform,null, index,transform,destination);
            //Vector3Int pos = new Vector3Int(index % Width_Height.x, 0, index / Width_Height.x);
            return ret;
        }

        public Vector3 Destination_Calculate(int index){
            
            Vector3Int pos = new Vector3Int(index % Width_Height.x, 0, index / Width_Height.x);
            return transform.rotation*
                (pos - new Vector3(Width_Height.x-1,0,Width_Height.y-1)/2.0f)*padding*res.transform.localScale.x + transform.up*y_pos + transform.position;
        
        }

        public Transponter_Controller Add_Res(Resource_controller resource){
            if(free_space<=0) return null; 
        

            resources.Add(resource);
            //resource.transform.localPosition = 
            int index = total_space - free_space;
            Transponter_Controller tc = Transponter_Controller.Create(resource.transform,this, index,transform);
            free_space--;
            return tc;
        }
        #endregion
    
    #region Trigger Managment

        bool is_player_in = false;
        IEnumerator Player_Stay(){
            On_Player_In();
            yield return new WaitForSeconds(Player_Controller.instance.Res_Transport_delay);
            if(is_player_in) StartCoroutine(Player_Stay());
        }

        private void OnTriggerEnter(Collider other) {
            if(other.tag != "Player") return;
            is_player_in = true;
            StartCoroutine(Player_Stay());
        }

        private void OnTriggerExit(Collider other) {
            if(other.tag != "Player") return;
            is_player_in = false;
        }
        protected virtual void On_Player_In(){
            Debug.LogError("player in undefined storage");
        }

    #endregion
    protected virtual void On_Awake(){
        resources = new List<Resource_controller>();
        free_space = Width_Height.x*Width_Height.y;
        total_space = free_space;
    }
    private void Awake() {
        On_Awake();
    }
    
    //protected virtual void OnClick(){}
    // Update is called once per frame

}
