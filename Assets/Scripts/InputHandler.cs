using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private Camera _mainCamera;
    private List<string> _names;
    private bool _hatClicked = false;
    private Vector2 _targetPosition;
    private Vector2 _currentPosition;

    public GameObject santasHat;
    public TextMeshPro noteText;
    public GameObject note;

    private void Awake()
    {
        _mainCamera = Camera.main;

        _names = new List<string>()
        {
            "Erik",
            "Erika",
        };
    }

    // Start is called before the first frame update
    void Start()
    {
        _targetPosition = new Vector2(note.transform.position.x, 0f);
        // _currentPosition = note.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (_hatClicked)
        {
            note.transform.position = Vector2.Lerp(note.transform.position, _targetPosition, 3f * Time.deltaTime);
        }
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        RaycastHit2D rayHit;

        if (Touchscreen.current is not null) //Use touch
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

        var randomIndex = Random.Range(0, _names.Count);
        noteText.text = _names[randomIndex]; 

    }
}
