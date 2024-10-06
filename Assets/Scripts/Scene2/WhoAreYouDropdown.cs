using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class WhoAreYouDropdown : MonoBehaviour
{
    TMPro.TMP_Dropdown _dropdown;

    private void Start()
    {
        _dropdown = GetComponent<TMPro.TMP_Dropdown>();

        StartCoroutine(GetParticipants());
    }

    private IEnumerator GetParticipants()
    {
        using UnityWebRequest request = UnityWebRequest.Get("https://wenzelapiman.azure-api.net/participants");
        
        yield return request.SendWebRequest();

        var apiResponse = JsonUtility.FromJson<ParticipantsApiResponse>(request.downloadHandler.text);

        _dropdown.options.Clear();    

        foreach (var participant in apiResponse.participants)
            _dropdown.options.Add(new(participant.name));

        _dropdown.RefreshShownValue();
    }
}
