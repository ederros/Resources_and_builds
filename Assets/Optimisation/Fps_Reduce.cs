using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fps_Reduce : MonoBehaviour
{
    public int reduce_to;
    void Start()
    {
        Application.targetFrameRate = reduce_to;
        Destroy(this.gameObject);
    }

}
