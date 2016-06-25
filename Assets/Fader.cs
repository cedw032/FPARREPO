using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Fader : MonoBehaviour {

    public float fadeDuration;
    public CanvasGroup canvasGroup;

    public bool doRun;
    
    private float timeElapsed = 0f;

    void Awake()
    {
        if (canvasGroup)
        {
            canvasGroup.alpha = 1f;
        }
    }

	// Update is called once per frame
	void Update () {
        if (canvasGroup)
        {
            if (doRun)
            {
                timeElapsed += Time.deltaTime;
                canvasGroup.alpha = (Mathf.Min(1f, timeElapsed / fadeDuration));

                if (canvasGroup.alpha == 1f)
                {
                    if (State.vr)
                    {
                        State.vr = false;
                        SceneManager.LoadScene("Scenes/blank");
                        Resources.UnloadUnusedAssets();
                        SceneManager.LoadScene("Scenes/main");
                    }
                    else
                    {
                        State.vr = true;
                        SceneManager.LoadScene("Scenes/blank");
                        Resources.UnloadUnusedAssets();
                        SceneManager.LoadScene("Scenes/VR");

                    }
                }
            }
            else
            {
                if (canvasGroup.alpha != 0f)
                {
                    canvasGroup.alpha = Mathf.Max(0f, canvasGroup.alpha - Time.deltaTime);
                }
            }
        }
	}
}
