using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    private void Start()
    {
        Invoke(nameof(Next), 5f);
    }

    public void Next()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        Game.loopCount += 1;
    }
}
