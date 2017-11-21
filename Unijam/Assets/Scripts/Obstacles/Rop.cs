using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rop : Obstacle { 

    public bool isDestroyed;

    public new void Start()
    {
    isDestroyed = false;
    }

    // Animation

    public override void Animate()
    {
        this.gameObject.SetActive(false);
        isDestroyed = true;
        //ChargeAnimation("isDestroyed");
    }

    public override bool isActivable(Action.ActionType actionType)
    {
        if (actionType == Action.ActionType.Cut || actionType == Action.ActionType.Shoot)
        {
            return true;
        }
        return false;
    }
}
