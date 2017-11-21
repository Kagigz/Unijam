using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle : MonoBehaviour {



    public void Start()
    {
    }
    
    public abstract bool isActivable(Action.ActionType actionType);

    public abstract void Animate();


    public void ChargeAnimation(string name, bool b)
    {
        Animator animatorHandler = GetComponent<Animator>();
        animatorHandler.SetBool(name, b);
    }
    
}
