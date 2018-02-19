using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class carPathFinding : MonoBehaviour {
    NavMeshAgent agent;
    public Transform[] cornerTransPos;
    Vector3[] cornerPos;
    Vector3 targetDis;
    public float rotationDir;
	// Use this for initialization
	void Start () {
        agent = this.GetComponent<NavMeshAgent>();
        if(cornerTransPos != null && cornerTransPos.Length > 0)
        {
            cornerPos = new Vector3[cornerTransPos.Length];
            for(int i = 0; i < cornerTransPos.Length; i++)
            {
                cornerPos[i] = cornerTransPos[i].position;
            }
        }
        motionorDestinationSetforCar(false);
    }

    float distance = 0;
    int corner = 0;
	// Update is called once per frame
	void Update () {
		if(agent)
        {
            distance = Vector3.Distance(transform.position, targetDis);
          //  Debug.Log("distance----"+distance);
            if(distance < 0.12f)
                motionorDestinationSetforCar ();
        }
	}

    void motionorDestinationSetforCar (bool rotate = true)
    {
        if (corner > 3)
            corner = 0;
        if(rotate)
            transform.Rotate(new Vector3(0, rotationDir , 0), Space.World);
        targetDis = cornerPos[corner];
        agent.SetDestination(targetDis);
        corner++;
    }

    void OnTriggerEnter(Collider other)
    {
      //  Debug.Log("on trigger... car"+ other.tag);
       if(other.tag.Equals ("trafficLight"))
        {
            if(other.GetComponent<trafficLightSystem>().redLightEnabled)
            {
                agent.enabled = false;
            }
        } 
    }

    void OnTriggerStay(Collider other)
    {
      //  Debug.Log("on trigger stay... car" + other.tag);
        if (other.tag.Equals("trafficLight"))
        {
            if (other.GetComponent<trafficLightSystem>().redLightEnabled)
            {
                agent.enabled = false;
            }
            else
            {
                agent.enabled = true;
                corner--;
                motionorDestinationSetforCar(false);
            }
        }

    }
}
