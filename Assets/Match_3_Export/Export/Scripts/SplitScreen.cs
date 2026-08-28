using UnityEngine;
using UnityEngine.SceneManagement;

public class SplitScreen : MonoBehaviour

{
    SceneManager.LoadSceneAsync("SceneB", LoadSceneMode.Additive);
}
