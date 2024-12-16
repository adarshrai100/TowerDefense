using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private StateMachine stateMachine;
    private NavMeshAgent agent;

    public NavMeshAgent Agent { get => agent; }
    public GameObject Player { get => player; }
    [SerializeField]
    private string currentState;
    public Path path;

    private GameObject player;
    private Vector3 lastKnowPos;

    public Vector3 LastKnowPos { get => lastKnowPos; set => lastKnowPos = value; }

    [Header("Sight Values")]
    public float sightDistance=20f;
    public float fieldOFView=85f;
    public float eyeHeight;

    [Header("Weapon Values")]
    public Transform gunBarrel;
    [Range(0.1f, 10f)]
    public float fireRate;

    // Start is called before the first frame update
    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        stateMachine.Initialise();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        CanSeePlayer();
        currentState = stateMachine.activeState.ToString();
    }

    public bool CanSeePlayer()
    {
        if(player!=null)
        {
            //is the player close enough to be seen?
            if(Vector3.Distance(transform.position,player.transform.position) < sightDistance)
            {
                Vector3 targetDirection = player.transform.position - transform.position - (Vector3.up * eyeHeight);
                float angleToPlayer = Vector3.Angle(targetDirection, transform.forward);
                if (angleToPlayer >= -fieldOFView && angleToPlayer <= fieldOFView)
                {
                    Ray ray = new Ray(transform.position + (Vector3.up * eyeHeight), targetDirection);
                    RaycastHit hitInfo = new RaycastHit();
                    if(Physics.Raycast(ray,out hitInfo,sightDistance))
                    {
                        if(hitInfo.transform.gameObject==player)
                        {
                            Debug.DrawRay(ray.origin, ray.direction * sightDistance);
                            return true;
                        }
                    }
                    
                }
            }    
        }
        return false;
    }    
}
