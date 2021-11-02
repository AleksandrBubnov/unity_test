using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Transform _playerTransform;

    //[SerializeField] ParticleSystem _particleSystem;
    [SerializeField] int _helth = 100;
    //[SerializeField] int _damage = 5;
    [SerializeField] float _sightRange = 20;
    [SerializeField] float _attackRange = 2;

    [SerializeField] LayerMask _playerLayer;

    private bool _isDead = false;
    private bool _inSightRange, _inAttackRange;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _playerTransform = GameObject.FindObjectOfType<CharacterController>().transform;
    }
    private void Update()
    {
        _inSightRange = Physics.CheckSphere(transform.position, _sightRange, _playerLayer);
        _inAttackRange = Physics.CheckSphere(transform.position, _attackRange, _playerLayer);

        if (_inSightRange && !_inAttackRange) GoToThePlayer();
        if (_inSightRange && _inAttackRange) AttackPlayer();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _sightRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }

    private void GoToThePlayer()
    {
        _agent.SetDestination(_playerTransform.position);
    }
    private void AttackPlayer()
    {
        _agent.SetDestination(transform.position);
        transform.LookAt(_playerTransform);
    }
    public void TakeDamage(int value)
    {
        _helth -= value;
        Debug.Log($"_helth: {_helth}");
        if (_helth <= 0 && !_isDead)
        {
            //_particleSystem.Play();
            Invoke("DestroyEnemy", 2f);
            _agent.speed = 0;
            _isDead = true;
        }
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
