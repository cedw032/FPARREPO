using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartApp : MonoBehaviour {
    public void OnSplashClick()
    {
        SceneManager.LoadScene("Scenes/blank");
        Resources.UnloadUnusedAssets();
        SceneManager.LoadScene("Scenes/VR");
        State.vr = true;
    }
}
