using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTuto : MonoBehaviour {

    GameObject player;
    GameObject sprite;
    public Sprite spr;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("PlayerFinal");
        switch (this.gameObject.name)
        {
            case "TriggerTuto1":
                sprite = GameObject.Find("Tuto1");
                break;
            
        }
       sprite.GetComponent<SpriteRenderer>().sprite = null;
    }
	
	// Update is called once per frame
	void Update () {
        player = GameObject.Find("PlayerFinal");
        if (Mathf.Abs(player.transform.position.x - transform.position.x) < GetComponent<Collider2D>().bounds.size.x / 2)
        {
            if (Mathf.Abs(player.transform.position.y - transform.position.y) < GetComponent<Collider2D>().bounds.size.y / 2)
            {
                sprite.GetComponent<SpriteRenderer>().sprite = spr;
            } else
            {
                sprite.GetComponent<SpriteRenderer>().sprite = null;
            }
        } else
        {
            sprite.GetComponent<SpriteRenderer>().sprite = null;
        }
    }
}

