using UnityEngine;

public class Note : MonoBehaviour
{
    private bool _noteShouldMove = false;
    private Vector3 _noteTargetPositionAfterClicked;

    private void Start()
    {
        _noteTargetPositionAfterClicked = new Vector3(transform.position.x, 350f, transform.position.z);
    }

    private void Update()
    {
        if (_noteShouldMove)
            transform.position = Vector3.Lerp(transform.position, _noteTargetPositionAfterClicked, 3f * Time.deltaTime);
    }

    public void InitiateNoteMovement() => _noteShouldMove = true;

    public void SetNoteText(string text) => GetComponentInChildren<TMPro.TextMeshPro>().text = text;
}
