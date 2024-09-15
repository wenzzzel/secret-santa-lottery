using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    bool _noteShouldMove = false;
    Vector3 _noteTargetPositionAfterClicked;

    // Start is called before the first frame update
    void Start()
    {
        _noteTargetPositionAfterClicked = new Vector3(transform.position.x, 350f, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (_noteShouldMove)
            transform.position = Vector3.Lerp(transform.position, _noteTargetPositionAfterClicked, 3f * Time.deltaTime);

    }

    public void InitiateNoteMovement() => _noteShouldMove = true;

    public void SetNoteText(string text) => GetComponentInChildren<TMPro.TextMeshPro>().text = text;
}
