using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public enum EnemyStates { Patrolling, Chasing, Attacking }
public class EnemyController : MonoBehaviour
{
    [HideInInspector] public Animator Animator;
    private NavMeshAgent _agent;
    private Transform _player;   

    [Header("Patrol")]
    [SerializeField] private float _walkPointRange;
    [SerializeField] private Vector2 _restingTimeRange = new Vector2(0, 2);
    [SerializeField] private Vector2 _worldLimitX = new Vector2(0, 1);
    [SerializeField] private Vector2 _worldLimitZ = new Vector2(0, 1);
    private Vector3 _walkPoint;
    private bool _walkPointSet;
    private bool _resting;

    [Header("Attack")]
    [SerializeField] private float _attackSpeed = 8;
    private bool _alreadyAttacked;
    public bool IsShooting;
    
    [Header("Chase")]
    [SerializeField] private LayerMask _playerLayer;

    [Header("States")]
    [SerializeField] private EnemyStates _currentState;
    [SerializeField] private float _sightRange, _attackRange;
    [SerializeField] private bool _playerInSightRange, _playerInAttackRange;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        Animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        _player = GameManager.Instance.Player.transform;
        _currentState = EnemyStates.Patrolling;
    }

    private void Update()
    {
        _playerInSightRange = Physics.CheckSphere(transform.position, _sightRange, _playerLayer);
        _playerInAttackRange = Physics.CheckSphere(transform.position, _attackRange, _playerLayer);
        Animator.SetFloat("Speed", _agent.velocity.magnitude);
        
        HandleStates();
        switch (_currentState)
        {
            case EnemyStates.Patrolling: Patrolling(); break;
            case EnemyStates.Chasing: Chasing(); break;
            case EnemyStates.Attacking: Attacking(); break;
        }
    }

    private void HandleStates()
    {
        if (!_playerInSightRange && !_playerInAttackRange) _currentState = EnemyStates.Patrolling;
        if (_playerInSightRange && !_playerInAttackRange) _currentState = EnemyStates.Chasing;
        if (_playerInAttackRange && _playerInSightRange) _currentState = EnemyStates.Attacking;
    }

    private void Patrolling()  // Enemy will patrol around the world within a certain limit.
    {
        if (!_walkPointSet) SearchWalkPoint();
        if (_walkPointSet)
        {
            _agent.SetDestination(_walkPoint);
            Vector3 distanceToWalkPoint = transform.position - _walkPoint;
            float restingTime = Random.Range(_restingTimeRange.x, _restingTimeRange.y);
            if (distanceToWalkPoint.magnitude <= 0 && !_resting) StartCoroutine(RestBetweenPoints(restingTime));
        }
        IsShooting = false;
    }

    private IEnumerator RestBetweenPoints(float time) // Wait for a given time when arriving at patrol point
    {
        _resting = true;
        yield return new WaitForSeconds(time);
        _walkPointSet = false;
        _resting = false;
    }

    private void SearchWalkPoint() 
    {
        float xPos = Random.Range(-_walkPointRange, _walkPointRange) + transform.position.x;
        float zPos = Random.Range(-_walkPointRange, _walkPointRange) + transform.position.z;

        xPos = Mathf.Clamp(xPos, _worldLimitX.x, _worldLimitX.y);
        zPos = Mathf.Clamp(zPos, _worldLimitZ.x, _worldLimitZ.y);
        _walkPoint = new Vector3(xPos, transform.position.y, zPos);
        _walkPointSet = true;
    }

    private void Chasing()
    {
        _agent.SetDestination(_player.position);
        _walkPointSet = false;
        IsShooting = false;
    }

    private void Attacking() // Attack the player from a certain range
    {
        _agent.SetDestination(transform.position);
        transform.LookAt(_player);
        if (!_alreadyAttacked)
        {
            IsShooting = true;
            _alreadyAttacked = true;
            Invoke(nameof(ResetAttack), 1/_attackSpeed);
        }
    }

    private void ResetAttack() => _alreadyAttacked = false;
    
    private void OnDrawGizmosSelected() // Editor Debugging
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _sightRange);
    }
}
