using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLvl : MonoBehaviour
{

    Scene scn;
    SceneManager sc;
    public string namescene;
    public GameObject player;
    // Use this for initialization
    void Start()
    {
        scn = SceneManager.GetActiveScene();
        sc = new SceneManager();
        namescene = scn.name;
    }

    private void load()
    {
        switch (namescene)
        {
            case "Lvl0":
                SceneManager.LoadScene("Lvl1");
                break;
            case "Lvl1":
                SceneManager.LoadScene("Lvl2");
                break;
            case "Lvl2":
                SceneManager.LoadScene("Lvl3");
                break;
            case "Lvl3":
                SceneManager.LoadScene("Lvl4");
                break;
            case "Lvl4":
                SceneManager.LoadScene("Lvl5");
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("PlayerFinal");
        if (Mathf.Abs(player.transform.position.x - transform.position.x) < GetComponent<Collider2D>().bounds.size.x / 2)
        {
            if (Mathf.Abs(player.transform.position.y - transform.position.y) < GetComponent<Collider2D>().bounds.size.y / 2)
            {
                Destroy(GameObject.Find("CheckPoint Mngr"));
                load();
            }
        }
    }
}
