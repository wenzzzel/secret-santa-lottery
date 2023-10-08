using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Networking;

[System.Serializable]
public class WebResponse 
{
    public List<Participant> participants;
}

[System.Serializable]
public class Participant 
{
    public int id;
    public string name;
}

public class InputHandler : MonoBehaviour
{
    private Camera _mainCamera;
    private bool _hatClicked = false;
    private Vector2 _targetPosition;
    private WebResponse _webResponse;
    private bool _requestDone = false;
    private bool _noteDelivered = false;

    public GameObject santasHat;
    public TextMeshPro noteText;
    public GameObject note;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        _targetPosition = new Vector2(note.transform.position.x, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (_requestDone)
        {
            note.transform.position = Vector2.Lerp(note.transform.position, _targetPosition, 3f * Time.deltaTime);

            if(_noteDelivered) return;

            _noteDelivered = true;

            var randomIndex = Random.Range(0, _webResponse.participants.Count);
            noteText.text = _webResponse.participants[randomIndex].name; 

        }
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;

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
    }

    private IEnumerator GetParticipants()
    {
        using (UnityWebRequest request = UnityWebRequest.Get("http://secretsantalotteryapi.hzbcd0gmg8bbfhfb.northeurope.azurecontainer.io/Participant"))
        // using (UnityWebRequest request = UnityWebRequest.Get("http://localhost:5238/Participant"))
        {
            yield return request.SendWebRequest();

            _webResponse = JsonUtility.FromJson<WebResponse>(request.downloadHandler.text);

            if (request.isDone) _requestDone = true;
        }
    }
}
