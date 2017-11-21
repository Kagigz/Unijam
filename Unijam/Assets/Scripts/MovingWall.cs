using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWall : MonoBehaviour {

    [SerializeField] private Rop _rope;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float maxElevation;

    private Engine engine;

    private float loggedPositionY = -200;

	// Use this for initialization
	void Start () {
        engine = GetComponent<Engine>();
        engine.speed = new Vector3(0, maxSpeed);
    }
	
	// Update is called once per frame
	void Update () {
		if (_rope.isDestroyed)
        {
            if (loggedPositionY == -200) loggedPositionY = transform.position.y;
            if (transform.position.y - loggedPositionY <= maxElevation) engine.Move();
        }
	}
}
