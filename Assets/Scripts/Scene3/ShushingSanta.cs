using UnityEngine;

public class ShushingSanta : MonoBehaviour
{
    private bool _santaShouldMove = false;
    private Vector3 _santaTargetPositionAfterClicked;

    private void Start()
    {
        _santaTargetPositionAfterClicked = new Vector3(2.26f, -1.87f, transform.position.z);
    }

    private void Update()
    {
        if (_santaShouldMove)
            transform.position = Vector3.Lerp(transform.position, _santaTargetPositionAfterClicked, 0.2f * Time.deltaTime);
    }

    public void InitiateSantaMovement() => _santaShouldMove = true;
}
