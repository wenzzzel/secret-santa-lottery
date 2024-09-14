using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class DrawNoteButton : MonoBehaviour
{
    IApiHelper _apiHelper;

    bool _hatClicked = false;

    [Inject]
    public void Init(IApiHelper apiHelper)
    {
        _apiHelper = apiHelper;
    }

    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(() => OnClick());
    }

    void Update()
    {
        
    }

    void OnClick()
    {
        if (_hatClicked) return;
        
        _hatClicked = true;


        var webResponse = _apiHelper.GetParticipants();

        var participants = webResponse.Participants;

        foreach (var participant in participants)
        {
            Debug.Log($"Participant: {participant.Name}");
        }

        // Do something with the participants


        var response = _apiHelper.DeleteParticipantWithDelay(participants.First().Id, participants.First().Name, participants.First().Partner, participants.First().SantaForId);

        Debug.Log($"Response: {response}");
    }
}
