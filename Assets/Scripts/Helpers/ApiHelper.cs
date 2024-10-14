using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ApiHelper 
{
    private ParticipantsApiResponse _participantsApiResponse;
    private bool _requestDone = false;

    public void UpdateParticipant(Participant participant)
    {
        var json = JsonUtility.ToJson(participant);

        var body = new System.Text.UTF8Encoding().GetBytes(json);
        
        using UnityWebRequest request = UnityWebRequest.Put($"https://wenzelapiman.azure-api.net/participants", body);
        
       request.SendWebRequest();

       return;
    }
}
