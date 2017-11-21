using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPointManager : PersistentSingleton<CheckPointManager>
{

    // Use this for initialization

    public CheckPoint[] checkPoints;
    public CheckPoint lastCheckpoint;

    private Vector3 respawnPoint;
    public GameObject player;
    private Animator animator;
    private int justDied;
    [SerializeField] private CameraFollowPlayer camera;

    void Start()
    {
        justDied = 0;
        checkPoints = GetComponentsInChildren<CheckPoint>();
        foreach (CheckPoint cp in checkPoints)
        {
            cp.player = player;
        }
        camera.player = player;
    }

    public void Sacrifice()
    {
        player.GetComponent<Animator>().SetBool("isDying", true);
        Application.LoadLevel(Application.loadedLevel);
        justDied = 4;
        respawnPoint = lastCheckpoint.transform.position;
    }

    public void Death()
    {
        player.GetComponent<Animator>().SetBool("isDying", true);
        Application.LoadLevel(Application.loadedLevel);
        justDied = 4;
        respawnPoint = checkPoints[0].transform.position;
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("PlayerFinal");
        if (justDied != 0)
        {
            player.transform.position = respawnPoint;
            justDied -= 1;
        }
        foreach (CheckPoint cp in checkPoints)
        {
            if (cp.isSet)
            {
                lastCheckpoint = cp;
            }
        }


        if (Input.GetButtonDown("Restart"))
        {
            Death();
        }

    }
}
