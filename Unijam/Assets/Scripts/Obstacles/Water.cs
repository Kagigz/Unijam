using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : Obstacle
{
    public new void Start()
    {
    }

    public Sprite mySprite;

    public override void Animate()
    {
        foreach(SpriteRenderer rd in GetComponentsInChildren<SpriteRenderer>())
        {
            rd.transform.GetComponent<Animator>().enabled = false;
            rd.sprite = mySprite;
        }
        this.gameObject.layer = 0;
    }

    // Animations

    public override bool isActivable(Action.ActionType actionType)
    {
        if (actionType == Action.ActionType.Freeze)
        {
            return true;
        }
        return false;
    }
}
