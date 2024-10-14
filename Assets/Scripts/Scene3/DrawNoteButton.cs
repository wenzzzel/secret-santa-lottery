using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DrawNoteButton : MonoBehaviour
{
    private bool _hatClicked = false;
    
    [SerializeField]
    private GameObject _note;

    [SerializeField]
    private GameObject _hushingSanta;

    [SerializeField]
    private List<WiggleScript> _wigglesToStopWhenClicked;
    
    private bool _noteDrawn = false;
    private bool _dbUpdated = false;
    private Participant _randomParticipant;

    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(() => OnClick());
    }

    private void Update()
    {
        if (_noteDrawn && !_dbUpdated)
        {
            GlobalVariables.Me.santaFor = _randomParticipant.name;
            StartCoroutine(UpdateParticipant(GlobalVariables.Me));

            _randomParticipant.alreadyTaken = true;
            StartCoroutine(UpdateParticipant(_randomParticipant));

            _dbUpdated = true;
        }
    }

    private void OnClick()
    {
        if (_hatClicked) return;
        
        _hatClicked = true;

        foreach (var wiggle in _wigglesToStopWhenClicked)
            wiggle._wiggle = false;


        if (!string.IsNullOrEmpty(GlobalVariables.Me.santaFor)) //If the current player already has a value in SantaForId, just use that one
        {
            _note.GetComponent<Note>().SetNoteText(GlobalVariables.Me.santaFor);
            _note.GetComponent<Note>().InitiateNoteMovement();
            _hushingSanta.GetComponent<ShushingSanta>().InitiateSantaMovement();
            return;
        }

        StartCoroutine(GetParticipantsAndInitiateNoteMovement());
        
        _hushingSanta.GetComponent<ShushingSanta>().InitiateSantaMovement();
    }

    private IEnumerator GetParticipantsAndInitiateNoteMovement()
    {
        using UnityWebRequest request = UnityWebRequest.Get("https://wenzelapiman.azure-api.net/participants");
        
        yield return request.SendWebRequest();

        var apiResponse = JsonUtility.FromJson<ParticipantsApiResponse>(request.downloadHandler.text);

        var participants = apiResponse.participants;

        _randomParticipant = PickRandomParticipant(participants);

        _note.GetComponent<Note>().SetNoteText(_randomParticipant.name);
        _note.GetComponent<Note>().InitiateNoteMovement();

        if (request.isDone) _noteDrawn = true;
    }

    private IEnumerator UpdateParticipant(Participant participant)
    {
        var json = JsonUtility.ToJson(participant);

        var body = new System.Text.UTF8Encoding().GetBytes(json);
        
        using UnityWebRequest request = UnityWebRequest.Put($"https://wenzelapiman.azure-api.net/participants", body);
        
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();
    }

    private Participant PickRandomParticipant(List<Participant> participants)
    {
        Participant randomParticipant;

        do
        {
            var randomIndex = Random.Range(0, participants.Count);

            randomParticipant = new Participant
            {
                id = participants[randomIndex].id,
                name = participants[randomIndex].name,
                partner = participants[randomIndex].partner,
                santaFor = participants[randomIndex].santaFor,
                alreadyTaken = participants[randomIndex].alreadyTaken
            };

        } while (InvalidParticipant(randomParticipant));

        return randomParticipant;
    }

    private bool InvalidParticipant(Participant participant) =>
        participant.id == GlobalVariables.Me.id || 
        participant.id == GlobalVariables.Me.partner ||
        participant.alreadyTaken;


}