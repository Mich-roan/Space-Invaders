using UnityEngine.Events;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneController : MonoBehaviour
{
  [SerializeField]
  private UnityEvent onSceneStart;
  [SerializeField]
  private Animator fadeAnimator;
    private void Start()
    {
      onSceneStart?.Invoke();
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void LoadSceneWithFade(string sceneName)
    {
        StartCoroutine(FadeCoroutine(sceneName));
    }
    private IEnumerator FadeCoroutine(string sceneName)
    {
        fadeAnimator.Play("FadeOut", 0, 0f);
        yield return null;
        yield return new WaitForSeconds(fadeAnimator.GetCurrentAnimatorStateInfo(0).length);
        SceneManager.LoadScene(sceneName);
    }
}
