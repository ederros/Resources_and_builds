using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building_Controller : MonoBehaviour
{
    public float tick = 5f;
    public float production_time = 1;
    public Storage_Controller inp_storage, out_storage;
    
    [SerializeField] Text problem_text;
    public GameObject product;

    public Transform inp_point, out_point;
    Transform taken_one; // taken resource from input storage
    public List<Resource_controller> ingredients;

    #region Problems Managment
        public List<string> problems;
        void Add_Problem(string problem){
            if(problems.Contains(problem)) return;
            problems.Add(problem);
        }
        void Sub_Problem(string problem){
            problems.Remove(problem);
        }
        public void Predicate_Problem(bool predication, string problem){
            problem = "- " + problem;
            if(predication) Add_Problem(problem);
            else Sub_Problem(problem);
        }
        public void Show_Problems(){
            string s = "";
            problems.ForEach(p => s+=p+"\n");
            problem_text.text = s;
        }
        public void Calculate_Problems(){
            if(out_storage) Predicate_Problem(out_storage.free_space<=0,"output storage is full");
            foreach(Resource_controller i in ingredients){
                Predicate_Problem(!inp_storage.is_mathing(i.name),$"needs a \"{i.name}\"");
            }
            //if(inp_storage) Predicate_Problem(inp_storage.free_space>=inp_storage.total_space,"input storage is empty");
            Show_Problems();
        }
    #endregion
    
    #region Production Managment
        IEnumerator Production()
        {
            yield return new WaitForSeconds(production_time);
            GameObject new_resource =  Instantiate(product,transform);
            Resource_controller rc =  new_resource.GetComponent<Resource_controller>();
            rc.name = product.name;
            new_resource.transform.position = out_point.position;
            //Debug.Log(rc.name);
            out_storage.Add_Res(rc);
        }
        IEnumerator timer()
        {
            yield return new WaitForSeconds(tick);
            Calculate_Problems();
            if(out_storage.free_space>0) {
                if(!ingredients.TrueForAll(i => inp_storage.is_mathing(i.name))) {
                    StartCoroutine(timer());
                    yield break;
                }
                for(int i = 0 ;i < ingredients.Count;i++){
                    Transponter_Controller tc = inp_storage.Sub_Res(ingredients[i].name, null, inp_point.position);
                    if(tc!= null) {
                        taken_one = tc.transform;
                        tc.new_parent = tc.transform;
                        //if(taken_one!=null) 
                    }
                }
                StartCoroutine(Production());
            }

            StartCoroutine(timer());
        }
    #endregion
    void Start()
    {
        Calculate_Problems();
        StartCoroutine(timer());
        if(inp_storage) inp_storage.Set_Target_Build(this);
        if(out_storage) out_storage.Set_Target_Build(this);
    }

}
