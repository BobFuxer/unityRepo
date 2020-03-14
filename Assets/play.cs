using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class play : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject anim;

    void Start()
    {
        anim.GetComponent<Animation>().Play();
        Debug.Log(anim.GetComponent<Animation>().Play());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
