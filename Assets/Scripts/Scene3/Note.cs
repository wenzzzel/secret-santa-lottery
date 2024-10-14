using UnityEngine;

public class Note : MonoBehaviour
{
    private bool _noteShouldMove = false;
    private bool _noteIsAtDestination = false;
    private Vector3 _noteTargetPositionAfterClicked;
    private ParticleSystem _particleSystem;

    private void Start()
    {
        _noteTargetPositionAfterClicked = new Vector3(transform.position.x, 1f, transform.position.z);
        _particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    private void Update()
    {
        if (_noteIsAtDestination) return;

        if (_noteShouldMove)
            transform.position = Vector3.Lerp(transform.position, _noteTargetPositionAfterClicked, 3f * Time.deltaTime);

        if (NoteIsCloseToDestination())
        {
            _noteIsAtDestination = true;
            _particleSystem.Play();
        }
    }

    public void InitiateNoteMovement() => _noteShouldMove = true;

    public void SetNoteText(string text) => GetComponentInChildren<TMPro.TextMeshPro>().text = text;

    private bool NoteIsCloseToDestination() => transform.position.y >= _noteTargetPositionAfterClicked.y - 0.1f && transform.position.x == _noteTargetPositionAfterClicked.x;
}
