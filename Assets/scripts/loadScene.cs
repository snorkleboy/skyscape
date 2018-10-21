using UnityEngine.SceneManagement;
using UnityEngine;

public class loadScene : MonoBehaviour {

    public void LoadByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
