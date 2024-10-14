using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WhoAreYouButton : MonoBehaviour
{
    [SerializeField]
    private TMPro.TMP_Dropdown _whoAreYouDropdown;

    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(() => OnClick());
    }

    private void OnClick()
    {
        StartCoroutine(GetParticipants());
    }

    private IEnumerator GetParticipants()
    {
        using UnityWebRequest request = UnityWebRequest.Get("https://wenzelapiman.azure-api.net/participants");
        
        yield return request.SendWebRequest();

        var apiResponse = JsonUtility.FromJson<ParticipantsApiResponse>(request.downloadHandler.text);

        GlobalVariables.Me = apiResponse.participants.Find(p => p.name == _whoAreYouDropdown.options[_whoAreYouDropdown.value].text);
    }
}