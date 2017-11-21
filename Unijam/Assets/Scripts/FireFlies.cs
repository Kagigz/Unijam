using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlies : MonoBehaviour {

    public int activeActionIndex;

    public bool superJump;
    
    [SerializeField]  public List<GameObject> Fireflies;
    public Vector3[] FirefliesPositions;

    public float speedRepositioning;

    bool canTurn;

    private Action[] actionsTemp;
    private List<Action> actions;

    // Use this for initialization
    void Start()
    {
        superJump = false;
        canTurn = true;
        activeActionIndex = 0;
        FirefliesPositions = new Vector3[Fireflies.Count];
        for (int i=0; i< Fireflies.Count; i++)
        {
            FirefliesPositions[i] = Fireflies[i].transform.position - this.transform.position;
        }
        /*
        actions = new List<Action>();
        actionsTemp = GetComponentsInChildren<Action>();
        for (int i = 0; i<actionsTemp.Length; i++)
        {
            actions.Add(actionsTemp[i]);
        }
        */

    }

    // Update is called once per frame
    void Update()
    {
        
        canTurn = true;

        for (int i = 0; i < Fireflies.Count; i++)
        {
            Action action = Fireflies[i].GetComponent<Action>();
            if (!action.hasObjectif)
            {
                Vector2 distance = new Vector2 (FirefliesPositions[i].x + transform.position.x - action.transform.position.x,
                                    FirefliesPositions[i].y + transform.position.y - action.transform.position.y);

                
                //if (distance.magnitude > 0.1f)
                //{
                //    canTurn = false;
                //    float movement = speedRepositioning * Time.deltaTime;
                //    float signX = distance / Mathf.Abs(distance);
                //    action.transform.position += new Vector3(signX * movement, 0, 0);
                //}
                if (distance.magnitude > 0.1f)
                {
                    float movement = speedRepositioning * Time.deltaTime;
                    float percentX = Mathf.Abs(distance.x) / distance.magnitude;
                    float signX = distance.x / Mathf.Abs(distance.x);
                    float signY;
                    if (distance.y != 0) signY = distance.y / Mathf.Abs(distance.y);
                    else signY = 0;
 
                    action.transform.position += new Vector3(signX * movement * percentX, signY * movement * (1 - percentX), 0);
                }
                else  // The Soul touched the obstacle
                {
                    if (action.isTurning)
                    {
                        if (transform.lossyScale.x < 0 && action.transform.localScale.x * transform.localScale.x > 0)
                        {
                            action.transform.localScale = new Vector3(- action.transform.localScale.x, action.transform.localScale.y);
                        }
                        if (transform.lossyScale.x > 0 && action.transform.localScale.x * transform.localScale.x < 0)
                        {
                            action.transform.localScale = new Vector3(-action.transform.localScale.x, action.transform.localScale.y);
                        }

                        action.isTurning = false;
                    }
                }
            }
        }

        if (Input.GetButtonDown("Switch"))
        {
            ChangeActive();
            TurnFireflies();
        }
        
        if (Input.GetAxis("Switch")!= 0)
        {
            ChangeActive();
            TurnFireflies();
        }
        
        if (Input.GetButtonDown("Fire"))
        {
            Debug.Log(activeActionIndex);
            TriggerActive();
        }
        if (Input.GetButtonDown("Jump") && superJump)
        {
            superJump = false;
            GetComponent<Engine>().powerJump /= 2f;
        }
    }

    // Move fly touched the player, he can now jump really high for 2 seconds
    public void ActivateMove()
    {
        superJump = true;
        GetComponent<Engine>().powerJump *= 2f;
    }

    void TriggerActive()
    {
        if (Fireflies[activeActionIndex].GetComponent<Action>().type == Action.ActionType.Move)
        {
            ThrowFireFly(this.transform.position);
            return;
        }
        if (Fireflies[activeActionIndex].GetComponent<Action>().type == Action.ActionType.Shoot)
        {
            int sign = (int)this.transform.localScale.x;
            ThrowFireFly(sign);
            return;
        }
        
        Transform player = GetComponent<Transform>();
        Action test = Fireflies[activeActionIndex].GetComponent<Action>();
        Obstacle obstacle = Fireflies[activeActionIndex].GetComponent<Action>().Activate(player.position);
        if (obstacle)
        {
            if (Fireflies[activeActionIndex].GetComponent<Action>().type == Action.ActionType.Cut
                || Fireflies[activeActionIndex].GetComponent<Action>().type == Action.ActionType.Destroy
                || Fireflies[activeActionIndex].GetComponent<Action>().type == Action.ActionType.Freeze)
            {
                ThrowFireFly(obstacle);
                return;
            }
            
        }
    }

    void ThrowFireFly(Obstacle obstacle)
    {
        Fireflies[activeActionIndex].GetComponent<Action>().SetObjective(obstacle);
    }
    
    public void ThrowFireFly(Vector3 position)
    {
        Fireflies[activeActionIndex].GetComponent<Action>().SetObjective(position);
    }

    public void ThrowFireFly(int direction)
    {
        Fireflies[activeActionIndex].GetComponent<Action>().SetDirection(direction);
        GameObject shootCopy = Instantiate(Fireflies[activeActionIndex]);
        shootCopy.GetComponent<Action>().SetDirection(direction);
        shootCopy.transform.position += new Vector3(this.transform.position.x, this.transform.position.y, 0);
        shootCopy.transform.localScale = new Vector3(this.transform.localScale.x, 1, 1);
        Fireflies[activeActionIndex].gameObject.SetActive(false);
        Fireflies.RemoveAt(activeActionIndex);
        //actions.RemoveAt(activeActionIndex);
    }
    
    public void DestroyCurrentFireFlies()
    {
        Fireflies[activeActionIndex].gameObject.SetActive(false);
        //Fireflies.RemoveAt(activeActionIndex);
        //actions.RemoveAt(activeActionIndex);
        //ChangeActive();
        //TurnFireflies();

    }

    void ChangeActive()
    {
        
        activeActionIndex++;
        if (activeActionIndex >= Fireflies.Count)
        {
            activeActionIndex = 0;
        }
   

    }

    void TurnFireflies()
    {
        //int pos = 0;
        //for (int i = activeactionindex; i < fireflies.count; i++)
        //{
        //    fireflies[i].transform.position = firefliespositions[pos] + this.transform.position;
        //    pos++;
        //}

        //for (int i = 0; i < activeactionindex; i++)
        //{
        //    fireflies[i].transform.position = firefliespositions[pos] + this.transform.position;
        //    pos++;
        //}

        Vector3 temp = FirefliesPositions[Fireflies.Count - 1];
        for (int i = Fireflies.Count - 1; i >=0; i--)
        {
            if (i != 0) FirefliesPositions[i] = FirefliesPositions[i - 1];
            else FirefliesPositions[0] = temp;
        }

        //Action temp2 = actions[Fireflies.Count - 1];
        //for (int i = Fireflies.Count - 1; i >= 0; i--)
        //{
        //    if (i != 0) actions[i] = actions[i - 1];
        //    else actions[0] = temp2;
        //}

        //Action temp2 = actions[Fireflies.Count - 1];
        //for (int i = 0; i < Fireflies.Count; i++)
        //{

        //    if (i == 0) actions[Fireflies.Count - 1] = actions[0];
        //    else if (i == Fireflies.Count - 1) actions[Fireflies.Count - 2] = temp2;
        //    else actions[i - 1] = actions[i];
        //}
    }

    public void FlipPositions()
    {
        for (int i = 0; i < FirefliesPositions.Length; i++)
        {
            FirefliesPositions[i].x = -FirefliesPositions[i].x;
        }
    }
   
}