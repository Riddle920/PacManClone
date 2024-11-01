using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel1()
    {
        DontDestroyOnLoad(this);
        SceneManager.LoadSceneAsync(1);
    }
    
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 1)
        {
            GameObject.FindWithTag("ScaredTimer").GetComponent<Text>().enabled = false;
            GameObject.FindWithTag("ExitButton").GetComponent<Button>().onClick.AddListener(ExitLevel);
        }
    }

    public void ExitLevel()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
