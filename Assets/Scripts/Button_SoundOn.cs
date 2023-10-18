using UnityEngine;
using UnityEngine.UI;

public class Button_SoundOn : MonoBehaviour
{
    public GameObject UiCanvas_SoundOn;
    public GameObject UiCanvas_WhoAreYou;
    public Button Button;

    // Start is called before the first frame update
    void Start()
    {
        Button.onClick.AddListener(() => OnClick());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnClick()
    {
        UiCanvas_SoundOn.SetActive(false);
        UiCanvas_WhoAreYou.SetActive(true);
    }
}
