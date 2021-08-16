using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class FlagNavMeshController : MonoBehaviour
{
    public ObjectPointer flagPointer ; 
    public List<GameObject> checkPoints ; 
    public FlagConstants flagConstants ; 
    private float waitTime ; 
    public int currentCheckPoint = 4 ; 
    public int targetCheckPoint = 3 ; 
    public float speed ; 
    private bool bossPhase = false ; 
    private Vector3 destination ; 
    private bool move = true ; 
    //NavMeshAgent agent;
    

    // Start is called before the first frame update
    void Start()
    {
        //agent = GetComponent<NavMeshAgent>();
        //agent.updateRotation = false;
        //agent.updateUpAxis = false;
        waitTime = flagConstants.waitTime ; 
        //StartCoroutine(wait()) ; 
        StartCoroutine(waitNoNavMesh()) ; 
    }
    
    void Update(){
        moveFlag() ; 
        flagPointer.flagTransform = (Transform) this.gameObject.transform ; 
       
    }

    public void onBossAppearResponse(){
        bossPhase = true ; 
    }

    void moveFlag(){
        if (move) { 
            Vector3 distance = destination - this.transform.position ; 
            if (distance.magnitude>0.01){//((Vector2) distance!=Vector2.zero){
                transform.position = Vector2.MoveTowards(this.transform.position, destination, speed * Time.deltaTime);
                //this.transform.position = this.transform.position + (distance/distance.magnitude)*speed*Time.deltaTime ; 
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other){
        //Debug.Log("collision Detected") ; 
        if (other.tag == "CheckPoint")
        {
            //StartCoroutine(wait()) ; 
            StartCoroutine(waitNoNavMesh()) ; 
        }
    }


    IEnumerator waitNoNavMesh(){
        if (!bossPhase){
            move = false ; 
            yield return new WaitForSeconds(waitTime) ; 
            move = true ; 
            currentCheckPoint = targetCheckPoint ; 
            while(currentCheckPoint==targetCheckPoint) {
                
                targetCheckPoint = Random.Range(0,checkPoints.Count) ; 
            }
            destination = checkPoints[targetCheckPoint].transform.position;
        } else{
            destination = checkPoints[3].transform.position ; 
        }
    }
}