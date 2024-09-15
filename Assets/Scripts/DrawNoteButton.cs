using System;
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

    public GameObject note;

    private Participant _participant;

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

        var participantsWebResponse = _apiHelper.GetParticipants();

        if (GlobalVariables.Me.SantaForId != null) //If the current player already has a value in SantaForId, remove all other participants so that we can only get that one
        {
            participantsWebResponse.Participants = participantsWebResponse.Participants.Where(p => p.Id == GlobalVariables.Me.SantaForId).ToList();
        }

        var participants = participantsWebResponse.Participants;

        do
        {
            var randomIndex = UnityEngine.Random.Range(0, participants.Count);

            _participant = new Participant
            {
                Id = participants[randomIndex].Id,
                Name = participants[randomIndex].Name,
                Partner = participants[randomIndex].Partner,
                SantaForId = participants[randomIndex].SantaForId
            };

            Debug.Log($"Got {_participant.Name}");
        } while (_participant.Id == GlobalVariables.Me.Id || _participant.Id == GlobalVariables.Me.Partner);

        note.GetComponent<Note>().SetNoteText(_participant.Name);
        note.GetComponent<Note>().InitiateNoteMovement();

        GlobalVariables.Me.SantaForId = _participant.Id;

        var response = _apiHelper.UpdateParticipant(GlobalVariables.Me);

        //UPdate record in database
        //var response = _apiHelper.DeleteParticipantWithDelay(participants.First().Id, participants.First().Name, participants.First().Partner, participants.First().SantaForId);
    }
}
