using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        //paused
        Time.timeScale = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //start game
        if (Input.GetKey(KeyCode.Space))
        {
            if(Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
        }
        //restart game
        else if (Input.GetKey(KeyCode.R))
        {
            Reload();
        }
	}

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
