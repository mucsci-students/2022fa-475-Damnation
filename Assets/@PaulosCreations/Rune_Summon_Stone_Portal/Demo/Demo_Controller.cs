using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo_Controller : MonoBehaviour
{
    public Runestone_Controller[] runestoneScript;
    public Portal_Controller[] portalScripts;
    public Transform camBaseTF;
    public Animator demoAnimator;
    private bool onOffR, runningR, onOffP, runningP, rotateCam;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            ToggleRuneStones();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePortals();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            rotateCam = !rotateCam;

            if (rotateCam)
            demoAnimator.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            demoAnimator.enabled = true;
        }

        if (rotateCam)
        {
            camBaseTF.Rotate(Vector3.up, Time.deltaTime * -10f);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void ToggleRuneStones()
    {
        if (runningR)
            return;

        onOffR = !onOffR;
        runningR = true;
        StartCoroutine(StartRunestones(onOffR));
    }

    public void TogglePortals()
    {
        if (runningP)
            return;

        onOffP = !onOffP;
        runningP = true;
        StartCoroutine(StartPortals(onOffP));
    }

    IEnumerator StartRunestones(bool _activate)
    {
        int count = 0;
        while (count < 5)
        {
            runestoneScript[count].ToggleRuneStone(_activate);
            count += 1;
            yield return new WaitForSeconds(1f);
        }

        runningR = false;
    }

    IEnumerator StartPortals(bool _activate)
    {
        int count = 0;
        while (count < 5)
        {
            portalScripts[count].TogglePortal(_activate);
            count += 1;
            yield return new WaitForSeconds(1f);
        }

        runningP = false;
    }
}
