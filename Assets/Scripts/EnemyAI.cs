using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Transform _playerTransform;
    private Animator _animator;
    private Slider _slider;
    private Text _sliderText;

    [SerializeField] float _corpseLive = 10f;
    [SerializeField] float _walkSpeed = 1.75f;
    [SerializeField] float _runSpeed = 3.5f;

    [SerializeField] int _health = 100;
    private int _healthNow = 100;
    [SerializeField] float _sightRange = 15;
    [SerializeField] float _attackRange = 1.5f;
    public int _enemyCost = 10;

    //[SerializeField] int _damage = 10;
    //[SerializeField] float _fireRate = 60f;
    //[SerializeField] float _nextTimeToHit = 0f;
    //private bool _isHit = false;

    [SerializeField] LayerMask _playerLayer;
    [SerializeField] Transform[] _patrolPoints;
    int _patrolPointNow = 0;

    public bool _isDead = false;
    private bool _inSightRange, _inAttackRange;

    private void Start()
    {
        if (_isDead) DestroyObject();

        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _walkSpeed;
        _playerTransform = GameObject.FindObjectOfType<CharacterController>().transform;
        _animator = GetComponentInChildren<Animator>();
        _slider = GetComponentInChildren<Slider>();
        _sliderText = GetComponentInChildren<Text>();
        _healthNow = _health;
        _slider.maxValue = _healthNow;
        _slider.value = _healthNow;
        _sliderText.text = $"{_healthNow} / {_health}";
    }
    private void Update()
    {
        _inSightRange = Physics.CheckSphere(transform.position, _sightRange, _playerLayer);
        _inAttackRange = Physics.CheckSphere(transform.position, _attackRange, _playerLayer);
        _sliderText.text = $"{_healthNow} / {_health}";

        if (_inSightRange && !_inAttackRange) GoToThePlayer();
        if (_inSightRange && _inAttackRange) AttackPlayer();

        if (Vector3.Distance(transform.position, _patrolPoints[_patrolPointNow].position) < 0.1f)
            _patrolPointNow++;
        if (_patrolPointNow >= _patrolPoints.Length) _patrolPointNow = 0;
        if (!_inSightRange && !_inAttackRange) GoToPoint();

        //if (Time.time >= _nextTimeToHit)
        //{
        //    _nextTimeToHit = Time.time + 60f / _fireRate;
        //    _isHit = true;
        //}
        //else _isHit = false;
    }

    private void GoToPoint()
    {
        _agent.speed = _walkSpeed;
        _agent.SetDestination(_patrolPoints[_patrolPointNow].position);

        _animator.SetBool("Running", false);
        _animator.SetBool("Walking", true);
    }

    //private void Hit()
    //{
    //    if (Player.Instance._isDead) return;
    //    Player.Instance.TakeDamage(_damage);
    //}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _sightRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }

    private void GoToThePlayer()
    {
        _agent.speed = _runSpeed;
        _agent.SetDestination(_playerTransform.position);
        _animator.SetBool("Running", true);
        _animator.SetBool("Walking", false);
        _animator.SetBool("Attacking", false);
    }
    private void AttackPlayer()
    {
        _agent.SetDestination(transform.position);
        transform.LookAt(_playerTransform);
        _animator.SetBool("Attacking", true);

        //if (_isHit) Hit();
    }
    public void TakeDamage(int value)
    {
        if (_isDead) return;

        _healthNow -= value;
        _slider.value = _healthNow;
        if (_healthNow <= 0 && !_isDead)
        {
            _animator.SetBool("Attacking", false);
            _animator.SetBool("Running", false);
            _agent.speed = 0f;
            _animator.SetBool("IsDead", true);
            Invoke("DestroyObject", _corpseLive);
            _isDead = true;
            _animator.enabled = false;
            GetComponent<Enemy>().RagdollOn();
            Player.Instance.AddScore(_enemyCost);
        }
    }
    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}