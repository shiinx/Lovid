using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshController : MonoBehaviour
{
    //bool moveToWP1 = false;
    float zpos = 0;
    Vector3 worldPosition;
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

    }   

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
            // Debug.Log(worldPosition);
            agent.destination = new Vector3(worldPosition.x, worldPosition.y, zpos);
        }

        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
    }
}
