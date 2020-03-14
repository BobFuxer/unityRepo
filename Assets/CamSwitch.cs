using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSwitch : MonoBehaviour
{
    public GameObject cam1;
    public GameObject cam2;
    public GameObject cam3;
    public GameObject cam4;
    public int cWaitLower = 1;
    public int cWaitUper = 6;
    private float timer = 0;
    private float timerMax = 0;
    private bool rCycle = false;
    private int cWait = 2;

    // what are comments?
    void Update()
    {
        if (Input.GetButtonDown("1Key"))
        {
            rCycle = false;
            goCam1();
        }

        if (Input.GetButtonDown("2Key"))
        {
            rCycle = false;
            goCam2();
        }

        if (Input.GetButtonDown("3Key"))
        {
            rCycle = false;
            goCam3();
        }

        if (Input.GetButtonDown("4Key"))
        {
            rCycle = true;
            cWait = Random.Range(cWaitLower, cWaitUper);
            Invoke("goRand", cWait);
        }
    }

    public void goCam1()
    {
        cam1.SetActive(true);
        cam2.SetActive(false);
        cam3.SetActive(false);

    }

    public void goCam2()
    {
        cam1.SetActive(false);
        cam2.SetActive(true);
        cam3.SetActive(false);

    }

    public void goCam3()
    {
        cam1.SetActive(false);
        cam2.SetActive(false);
        cam3.SetActive(true);

    }

    public void goRand()
    {
        if (rCycle)
        {
            var cTemp = Random.Range(0, 3);

            if(cTemp == 0)
            {
                goCam1();
            }
            if (cTemp == 1)
            {
                goCam2();
            }
            if (cTemp == 2)
            {
                goCam3();
            }

            cWait = Random.Range(cWaitLower, cWaitUper);
            Invoke("goRand", cWait);

        }
    }



}
