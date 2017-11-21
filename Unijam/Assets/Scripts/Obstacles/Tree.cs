using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : Obstacle
{
    public new void Start()
    {
    }

    public override void Animate() {
        //GetComponent<BoxCollider2D>().size = new Vector2(5, 1);
        //GetComponent<BoxCollider2D>().offset += new Vector2(2, -2);
        Destroy(GetComponent<BoxCollider2D>());
        ChargeAnimation("isCut", true);
    }

    // Animations

    public override bool isActivable(Action.ActionType actionType)
    {
        if (actionType == Action.ActionType.Destroy || actionType == Action.ActionType.Cut)
        {
            return true;
        }
        return false;
    }
}
