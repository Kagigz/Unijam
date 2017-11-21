using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : Obstacle
{
    public new void Start()
    {
    }
    

    public override void Animate()
    {
        ChargeAnimation("isDestroyed", true);
    }

    public override bool isActivable(Action.ActionType actionType)
    {
        if (actionType == Action.ActionType.Destroy)
        {
            return true;
        }
        return false;
    }
}
