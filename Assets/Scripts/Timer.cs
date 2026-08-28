using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public int TasonNumero = 1;
    public bool timeRunning = false;
    private float timePassed = 0.0f;
    public float TargetTime = 5.0f;
    private void Start()
    {
        timeRunning = true;
    }
    private void Update()
    {
        if (timeRunning == true)
        {
            if (timePassed < TargetTime)
            {
                timePassed += Time.deltaTime;
            }
            if (timePassed >= TargetTime)
            {
                timeRunning = false;
                timePassed = 0.0f;
                Debug.Log("aika loppui");
                //SceneManager.LoadScene(TasonNumero);
            }
        }

    }
}
