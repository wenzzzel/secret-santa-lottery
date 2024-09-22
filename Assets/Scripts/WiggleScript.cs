using UnityEngine;

public class WiggleScript : MonoBehaviour
{
    private Quaternion _initialRotation;
    private Quaternion _targetRotation;
    [SerializeField]
    public bool _wiggle = true;
    [SerializeField]
    private float _wiggleRange = 0.2f;
    [SerializeField]
    private float _wiggleSpeed = 0.2f;
    [SerializeField]
    private bool _invertedDirection = false;

    private void Start()
    {
        _initialRotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w); //Remember the start rotation
        _targetRotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);

        if (_invertedDirection)
            _wiggleRange *= -1;
        
        InvokeRepeating("Wiggle", _wiggleSpeed, _wiggleSpeed);
    }

    private void FixedUpdate()
    {
        if (_wiggle)
            transform.rotation = Quaternion.Lerp(transform.rotation, _targetRotation, 0.1f);
        else
            transform.rotation = _initialRotation;
    }

    private void Wiggle()
    {
        _wiggleRange *= -1; // Invert the value. Example 0.2 -> -0.2
        _targetRotation = new Quaternion(_initialRotation.x, _initialRotation.y, _initialRotation.z + _wiggleRange, _initialRotation.w);
    }
}
