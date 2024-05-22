using System;
using UnityEngine;

public class PlayerGrenade : MonoBehaviour
{
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private ParticleSystem _aimPoint;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Transform _grenadePrefab;
    [SerializeField] private Transform _throwPoint;
    [SerializeField] private float _throwForce = 10f;
    [SerializeField] private int _lineSegmentCount = 20;
    [SerializeField] private float _timeBetweenPoints = 0.1f;
    
    private PlayerController _playerController;
    private Vector3 _aimDirection;
    private bool _isAiming;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _aimPoint.Stop();
        _aimPoint.Clear();
    }

    void Update() => HandleAiming();

    private void HandleAiming()
    {
        Vector2 rightStickInput = -_playerController.GetShootAxis();

        if (rightStickInput.sqrMagnitude > 0.1f)
        {
            _isAiming = true;
            _aimDirection = new Vector3(rightStickInput.x, 0, rightStickInput.y);
            DrawTrajectory();
        }
        else if (_isAiming)
        {
            _aimPoint.Stop();
            _aimPoint.Clear();
            _isAiming = false;
            ThrowGrenade();
            _lineRenderer.positionCount = 0;
        }
    }

    private void DrawTrajectory()
    {
        Vector3[] linePoints = new Vector3[_lineSegmentCount];
        Vector3 startPosition = _throwPoint.position;
        Vector3 startVelocity = _aimDirection * _throwForce;
        
        _aimPoint.Play();

        for (int i = 0; i < _lineSegmentCount; i++)
        {
            float time = i * _timeBetweenPoints;
            Vector3 point = startPosition + startVelocity * time + 0.5f * Physics.gravity * time * time;
            linePoints[i] = point;

            if (i > 0 && Physics.Raycast(linePoints[i - 1], linePoints[i] - linePoints[i - 1],
                    out RaycastHit hit, (linePoints[i] - linePoints[i - 1]).magnitude, _groundMask))
            {
                linePoints[i] = hit.point;
                _lineRenderer.positionCount = i + 1;
                _lineRenderer.SetPositions(linePoints);
                _aimPoint.transform.position = hit.point;
                return;
            }
        }

        _lineRenderer.positionCount = _lineSegmentCount;
        _lineRenderer.SetPositions(linePoints);
    }

    private void ThrowGrenade()
    {
        Transform grenadeInstance = Instantiate(_grenadePrefab, _throwPoint.position, Quaternion.identity);
        Rigidbody rb = grenadeInstance.GetComponent<Rigidbody>();

        Vector3 throwVelocity = _aimDirection * _throwForce;
        rb.velocity = throwVelocity;
    }
}

