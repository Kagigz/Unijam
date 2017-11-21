using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

    public bool isSet;
    public GameObject player;
    private Animator animator;
    [SerializeField]
    GameObject child;

    private void Start()
    {
        animator = child.GetComponent<Animator>();        
    }

    public void activateCheckPoint()
    {
        isSet = true;
        if (animator) animator.SetBool("isActivated", true);
        GetComponent<BoxCollider2D>().enabled = true;
    }

    public void shutdownCheckPoint()
    {
        isSet = false;
        GetComponent<BoxCollider2D>().enabled = false;
    }

    void Update () {
        player = GameObject.Find("PlayerFinal");
        if (player.transform.position.x > transform.position.x)
        {
            activateCheckPoint();    
        }
	}
}
