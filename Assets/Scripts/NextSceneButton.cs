using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextScene : MonoBehaviour
{
    [SerializeField]
    private int _delay = 0;

    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(() => OnClick());
    }

    private void OnClick()
    {
        StartCoroutine(GoToNextScene());
    }

    private IEnumerator GoToNextScene()
    {
        yield return new WaitForSeconds(_delay);

        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
