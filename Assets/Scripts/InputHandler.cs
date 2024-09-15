using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Networking;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{
    private Camera _mainCamera;
    private bool _hatClicked = false;
    private Vector2 _noteTargetPositionAfterClicked;
    private ParticipantsApiResponse _webResponse;
    private int _participantId;
    private string _participantName;
    private bool _requestDone = false;
    private bool _noteDelivered = false;

    public GameObject santasHat;
    public TextMeshPro noteText;
    public GameObject note;
    public TMP_Dropdown whoAreYou;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        _noteTargetPositionAfterClicked = new Vector2(note.transform.position.x, 350f);
    }

    // Update is called once per frame
    void Update()
    {
        if (_requestDone)
        {
            note.transform.position = Vector2.Lerp(note.transform.position, _noteTargetPositionAfterClicked, 3f * Time.deltaTime);

            if(_noteDelivered) return;

            _noteDelivered = true;

            do
            {
                var randomIndex = Random.Range(0, _webResponse.Participants.Count);
                _participantId = int.Parse(_webResponse.Participants[randomIndex].Id);
                _participantName = _webResponse.Participants[randomIndex].Name;

                Debug.Log($"Get {_participantName}");
            } while (_participantId == whoAreYou.value);

            noteText.text = _participantName; 
        }
    }

    public void OnClick(InputAction.CallbackContext callbackContext)
    {
        if (!callbackContext.started) return;

        RaycastHit2D rayHit;

        if (Touchscreen.current is not null) //Use touchscreen
        {
            rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Touchscreen.current.position.ReadValue()));
        }
        else //Use mouse
        {
            rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        }

        if (!rayHit.collider) return;

        if (_hatClicked) return;

        _hatClicked = true;

        StartCoroutine(GetParticipants());

        StartCoroutine(DeleteParticipantWithDelay());
    }

    private IEnumerator GetParticipants()
    {
        using UnityWebRequest request = UnityWebRequest.Get("https://wenzelapiman.azure-api.net/participant");
        
        yield return request.SendWebRequest();

        _webResponse = JsonUtility.FromJson<ParticipantsApiResponse>(request.downloadHandler.text);

        if (request.isDone) _requestDone = true;
    }

    private IEnumerator DeleteParticipantWithDelay()
    {
        yield return new WaitForSeconds(3f);
        
        using UnityWebRequest request = UnityWebRequest.Delete($"https://wenzelapiman.azure-api.net/participant?id={_participantId}&name={_participantName}");
        
        Debug.Log($"Participant to delete has id: {_participantId} and name: {_participantName}.");
        
        yield return request.SendWebRequest();
    }
}