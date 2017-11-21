using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Obstacle
{

    public new void Start()
    {
    }

    public override void Animate()
    {
        this.gameObject.SetActive(false);
        //ChargeAnimation("isDying", true);
    }

    // Animations
    public AnimationClip onFroze;
    public AnimationClip onKill;


    public override bool isActivable(Action.ActionType actionType)
    {
        if (actionType == Action.ActionType.Shoot || actionType == Action.ActionType.Freeze)
            return true;
        return false;
    }
}
