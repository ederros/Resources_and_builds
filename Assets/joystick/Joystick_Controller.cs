using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Joystick_Controller : MonoBehaviour
{
    
    [SerializeField] GameObject handler;
    [SerializeField] GameObject background;

    public Vector2 direction {get; private set;}
    public Vector2 clamped_offset {get; private set;}
    float size;
    Touch my_touch;
    private void Awake()
    {
        my_rect_transform = GetComponent<RectTransform>();
        size = my_rect_transform.rect.size.x/2f;
        handler_rect_transform = handler.GetComponent<RectTransform>();
        back_rect_transform = background.GetComponent<RectTransform>();
    }
    RectTransform my_rect_transform = null;
    RectTransform handler_rect_transform = null;
    RectTransform back_rect_transform = null;
    // Update is called once per frame
    bool Activisation(){ // activates if touches count > 0
        if(Input.touchCount<=0) {
            handler.SetActive(false);
            background.SetActive(false);
            return false; 
        }
        handler.SetActive(true);
        background.SetActive(true);
        return true;
    }
    void Touch_Check(int index){
        if(Input.touches[index].phase == TouchPhase.Began) {
            //Debug.Log(Input.touches[index].position);
            my_rect_transform.position = Input.touches[index].position;
            handler_rect_transform.localPosition = Vector3.zero;
            //my_rect_transform.position -= Vector3.forward* my_rect_transform.position.z;
        }
        
        if(Input.touches[index].phase == TouchPhase.Moved) {
            Vector3 shift = ((Vector3)Input.touches[index].position-(Vector3)my_rect_transform.position);
            direction = shift.normalized;
            shift = shift.magnitude<size?shift:shift.normalized*size;
            clamped_offset = shift/size;
            handler_rect_transform.localPosition = ((Vector2)shift);
        }

        if(Input.touches[index].phase == TouchPhase.Ended) {

            direction = Vector2.zero;
            clamped_offset = Vector2.zero;
        }
    }
    void Update()
    {
        if(!Activisation()) return;
        Touch_Check(0);
    }
}
