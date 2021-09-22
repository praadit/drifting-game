using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMan : MonoBehaviour {

    public static SceneMan intance { get; set; }
    
    void Awake()
    {
        if(intance == null)
        {
            intance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SceneManager.LoadScene(2, LoadSceneMode.Single);
        }
    }
}
