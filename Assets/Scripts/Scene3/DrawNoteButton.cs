using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class DrawNoteButton : MonoBehaviour
{
    private bool _hatClicked = false;
    
    [SerializeField]
    private GameObject _note;

    [SerializeField]
    private GameObject _hushingSanta;

    [SerializeField]
    private List<WiggleScript> _wigglesToStopWhenClicked;

    private IApiHelper _apiHelper;

    [Inject]
    public void Init(IApiHelper apiHelper) //Can this be private?
    {
        _apiHelper = apiHelper;
    }

    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(() => OnClick());
    }

    private void Update()
    {

    }

    private void OnClick()
    {
        if (_hatClicked) return;
        
        _hatClicked = true;

        foreach (var wiggle in _wigglesToStopWhenClicked)
            wiggle._wiggle = false;


        if (GlobalVariables.Me.SantaFor != null) //If the current player already has a value in SantaForId, just use that one
        {
            _note.GetComponent<Note>().SetNoteText(GlobalVariables.Me.SantaFor);
            _note.GetComponent<Note>().InitiateNoteMovement();
            _hushingSanta.GetComponent<ShushingSanta>().InitiateSantaMovement();
            return;
        }

        var participants = _apiHelper.GetParticipants().Participants; // Try to get await stuff working

        var randomParticipant = PickRandomParticipant(participants);

        _note.GetComponent<Note>().SetNoteText(randomParticipant.Name);
        _note.GetComponent<Note>().InitiateNoteMovement();
        _hushingSanta.GetComponent<ShushingSanta>().InitiateSantaMovement();

        GlobalVariables.Me.SantaFor = randomParticipant.Name;
        var updateMyselfResponse = _apiHelper.UpdateParticipant(GlobalVariables.Me);

        randomParticipant.AlreadyTaken = true;
        var updateDrawnParticipantResponse = _apiHelper.UpdateParticipant(randomParticipant);
    }

    private Participant PickRandomParticipant(List<Participant> participants)
    {
        Participant randomParticipant;

        do
        {
            var randomIndex = Random.Range(0, participants.Count);

            randomParticipant = new Participant
            {
                Id = participants[randomIndex].Id,
                Name = participants[randomIndex].Name,
                Partner = participants[randomIndex].Partner,
                SantaFor = participants[randomIndex].SantaFor,
                AlreadyTaken = participants[randomIndex].AlreadyTaken
            };

        } while (InvalidParticipant(randomParticipant));

        return randomParticipant;
    }

    private bool InvalidParticipant(Participant participant) =>
        participant.Id == GlobalVariables.Me.Id || 
        participant.Id == GlobalVariables.Me.Partner ||
        participant.AlreadyTaken;
}
