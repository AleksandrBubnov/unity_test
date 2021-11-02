using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] int _damage = 5;
    [SerializeField] float _fireRange = 50f;
    [SerializeField] float _fireRate = 20f;
    [SerializeField] float _shootForce = 10f;
    [SerializeField] LayerMask _enemyLayer;

    private float _nextTimeToFire = 0.01f;
    private Camera fppCamera;

    private void Start()
    {
        fppCamera = Camera.main;
    }
    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= _nextTimeToFire)
        {
            _nextTimeToFire = Time.time + 1f / _fireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(fppCamera.transform.position, fppCamera.transform.forward,
            out hit, _fireRange, _enemyLayer))
        {
            EnemyAI enemy = hit.transform.GetComponent<EnemyAI>();
            if (enemy != null)
            {
                enemy.TakeDamage(_damage);
            }
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * _shootForce);
            }
        }
    }
}
