using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    private void Start()
    {
        Invoke("Next", 5f);
    }

    public void Next()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
