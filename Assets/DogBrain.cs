using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class FindBallState : State {
    DogBrain brain;
    
    public override void Enter()
    {
        brain = owner.GetComponent<DogBrain>();
        GameObject[] balls = GameObject.FindGameObjectsWithTag("ball");

        foreach (GameObject ball in balls) {
            float distanceToBall = Vector3.Distance(ball.transform.position, owner.transform.position);
            float distanceToPlayer = Vector3.Distance(ball.transform.position, brain.player.transform.position);
            if(distanceToPlayer > brain.dropRange) {
                if (distanceToBall < brain.findRange){
                    brain.targetBall = ball;
                }
            }

            //Debug.Log(owner.GetComponent<Arrive>().targetGameObject);
        }
    }

    public override void Think()
    {
        if (brain.targetBall != null)
        {
            owner.GetComponent<StateMachine>().ChangeState(new GetBallState());
        } else {
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

        Debug.Log(brain.targetBall.transform.position);
    
    }
    public override void Exit()
    {
        owner.GetComponent<Arrive>().enabled = false;
    }

    public override void Think()
    {
        // Change condition
        if (brain.targetBall != null)
        {
            owner.GetComponent<StateMachine>().ChangeState(new GetBallState());
        } 
    }
}

class ReturnBallState : State {

}

public class DogBrain : MonoBehaviour
{
    public GameObject targetBall;
    public GameObject player;
    public float findRange = 40;
    public float dropRange = 10;
    public bool hasBall = false;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<StateMachine>().ChangeState(new FindBallState());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
