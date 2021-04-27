using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


class FindBallState : State {
    DogBrain brain;
    
    public override void Enter()
    {
        brain = owner.GetComponent<DogBrain>();
        GameObject[] balls = GameObject.FindGameObjectsWithTag("ball");

        brain.targetBall = null;

        foreach (GameObject ball in balls) {
            float distanceToBall = Vector3.Distance(ball.transform.position, owner.transform.position);
            float distanceToPlayer = Vector3.Distance(ball.transform.position, brain.player.transform.position);
            if(distanceToPlayer > brain.dropRange + 1) {
                if (distanceToBall < brain.findRange){
                    brain.targetBall = ball;
                } 
            }
        }
    }

    public override void Think()
    {
        if (brain.targetBall != null)
        {
            brain.src.Play();
            owner.GetComponent<StateMachine>().ChangeState(new GetBallState());
        } else {
            brain.transform.LookAt(brain.player.transform);
            owner.GetComponent<StateMachine>().ChangeState(new FindBallState());
        }
    }
}

class GetBallState : State {
    DogBrain brain;
    
    public override void Enter()
    {
        brain = owner.GetComponent<DogBrain>();
        owner.GetComponent<Arrive>().targetPosition = brain.targetBall.transform.position;
        owner.GetComponent<Arrive>().enabled = true;   
    }

    public override void Think()
    {
        if (Vector3.Distance(brain.targetBall.transform.position, owner.transform.position) > 1.2f)
        {
            owner.GetComponent<StateMachine>().ChangeState(new GetBallState());
        } else {
            brain.hasBall = true;
            brain.targetBall.transform.parent = owner.transform;
            brain.targetBall.transform.position = brain.ballAttach.transform.position;
            brain.targetBall.GetComponent<Rigidbody>().isKinematic = true;
            
            owner.GetComponent<StateMachine>().ChangeState(new ReturnBallState());
        }
    }
}

class ReturnBallState : State {
    DogBrain brain;
    
    public override void Enter()
    {
        brain = owner.GetComponent<DogBrain>();

        owner.GetComponent<Arrive>().targetPosition = brain.player.transform.position;
    }

    public override void Think()
    {
        // Change condition
        if (Vector3.Distance(brain.targetBall.transform.position, brain.player.transform.position) <= brain.dropRange)
        {
            owner.GetComponent<Arrive>().boid.velocity = Vector3.zero;
            owner.transform.LookAt(brain.player.transform);
            brain.targetBall.transform.parent = null;
            brain.targetBall.GetComponent<Rigidbody>().isKinematic = false;
            brain.targetBall = null;
            brain.src.Play();
            
            owner.GetComponent<StateMachine>().ChangeState(new FindBallState());
        } else {
            owner.GetComponent<StateMachine>().ChangeState(new ReturnBallState());
        }
    }
}

public class DogBrain : MonoBehaviour
{
    public GameObject ballAttach;
    public GameObject targetBall;
    public GameObject player;
    public float findRange = 40;
    public float dropRange = 10;
    public bool hasBall = false;
    public AudioSource src;
    // Start is called before the first frame update
    void Start()
    {
        src = GetComponent<AudioSource>();
        
        GetComponent<Arrive>().enabled = false;   
        GetComponent<StateMachine>().ChangeState(new FindBallState());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
