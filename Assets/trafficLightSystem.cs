using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trafficLightSystem : MonoBehaviour {

    public MeshRenderer redLight;
    public MeshRenderer orangeLight;
    public MeshRenderer greenLight;
    public GameObject pedestrianTowalk;
    public bool redLightEnabled = false;

    void Start ()
    {
        ChangesInTrafficsignal("GREEN");
    }

    void Update ()
    {
        if (redLightEnabled)
        {
            movePedestrianForwardOrReset();
        }
        else if (redLight.enabled)
        {
            movePedestrianForwardOrReset(true);
            ChangesInTrafficsignal("GREEN");
        }
    }

    void ChangesInTrafficsignal (string signal)
    {
        switch (signal)
        {
            case "GREEN":
                greenLight.enabled = true;
                redLight.enabled = false;
                orangeLight.enabled = false;
                break;
            case "RED":
                greenLight.enabled = false;
                redLight.enabled = true;
                orangeLight.enabled = false;
                break;
            case "ORANGE":
                greenLight.enabled = false;
                redLight.enabled = false;
                orangeLight.enabled = true;
                break;
        }
    }

    bool isTouchBlocked = false;
    public void enableAndDisableRedSignal ()
    {
        if (isTouchBlocked)
            return;

        isTouchBlocked = true;
        Invoke("resetTouch",0.5f);
        if(redLightEnabled)
        {
            redLightEnabled = false;
        } else
        {
            ChangesInTrafficsignal("RED");
            redLightEnabled = true;
        }
    }

    void resetTouch ()
    {
        isTouchBlocked = false;
    }

    void movePedestrianForwardOrReset (bool placeToOriginalPos = false)
    {
        Vector3 currentLoc = pedestrianTowalk.transform.position;
        if (currentLoc.z < 0)
            placeToOriginalPos = true;
        if (placeToOriginalPos)
        {
            currentLoc.z = 4.8f;
            pedestrianTowalk.transform.localPosition = currentLoc;
            return;
        }
        Vector3 nextPos = currentLoc;
        pedestrianTowalk.transform.Translate(Vector3.forward * Time.deltaTime);
        //pedestrianTowalk.transform.localPosition = Vector3.Lerp(transform.localPosition, nextPos, Time.deltaTime * 0.5f);
    }

}
