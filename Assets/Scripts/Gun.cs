using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    [SerializeField] int _damage = 5;
    [SerializeField] int _gunCapacity = 30;
    [SerializeField] float _fireRange = 50f;
    [SerializeField] float _reloadTime = 1f;
    [SerializeField] float _fireRate = 1200f;
    [SerializeField] float _shootForce = 30f;
    [SerializeField] LayerMask _enemyLayer;

    [SerializeField] ParticleSystem _partSystemBullets;
    [SerializeField] ParticleSystem _muzzleFlash;
    [SerializeField] GameObject _impactEffect;

    private float _nextTimeToFire = 0f;
    private Camera _fppCamera;
    private AudioSource[] _shootSource;

    [SerializeField] LayerMask _goalLayer;
    [SerializeField] float _useRange = 3f;
    [SerializeField] Text _canUseUI;
    [SerializeField] Text _reloadUI;
    [SerializeField] Slider _slider;
    private Text _sliderText;
    private int _bulletsNow = 30;

    private void Start()
    {
        _shootSource = GetComponents<AudioSource>();
        _fppCamera = Camera.main;
        _slider.maxValue = _gunCapacity;
        _slider.value = _gunCapacity;
        _bulletsNow = _gunCapacity;
        _sliderText = _slider.GetComponentInChildren<Text>();
        _sliderText.text = $"{_bulletsNow} / {_gunCapacity}";
    }
    private void Update()
    {
        //if (Input.GetButton("Fire2"))
        //{
        //    Ray ray = _fppCamera.ScreenPointToRay(Input.mousePosition);
        //    _fppCamera.transform.Translate(ray.direction * 1.2f, Space.World);
        //}

        if (_bulletsNow == _gunCapacity) _reloadUI.gameObject.SetActive(false);
        else _reloadUI.gameObject.SetActive(true);

        if (Input.GetKeyDown(KeyCode.R) && _bulletsNow < _gunCapacity)
        {
            _shootSource[1].Play();
            Invoke("ReloadGun", _reloadTime);
        }

        _sliderText.text = $"{_bulletsNow} / {_gunCapacity}";

        RaycastHit hit;
        if (Physics.Raycast(_fppCamera.transform.position, _fppCamera.transform.forward,
            out hit, _useRange, _goalLayer))
        {
            MedBox medBox = hit.transform.GetComponent<MedBox>();
            if (medBox != null) CanUse(medBox);
        }
        else CanNotUse();
    }

    private void FixedUpdate()
    {
        if (Input.GetButton("Fire1") && _bulletsNow <= 0)
        {
            _shootSource[2].Play();
            //Invoke("ReloadGun", _reloadTime);
        }
        if (Input.GetButton("Fire1") && Time.time >= _nextTimeToFire && _bulletsNow > 0)
        {
            _nextTimeToFire = Time.time + 60f / _fireRate;
            Shoot();
        }
    }

    private void CanUse(MedBox medBox)
    {
        _canUseUI.gameObject.SetActive(true);
        _canUseUI.text = $"Use(E) [{medBox.GetName()}]";
        if (Player.Instance.Health >= Player.Instance.HealthMax)
            _canUseUI.color = Color.gray;
        else
            _canUseUI.color = Color.white;

        medBox.CanUse();
    }
    public void CanNotUse()
    {
        _canUseUI.gameObject.SetActive(false);
        _canUseUI.text = "Can Use(E)";
        _canUseUI.color = Color.gray;
    }
    private void Shoot()
    {
        _partSystemBullets.Play();
        _muzzleFlash.Play();
        _bulletsNow--;
        _slider.value = _bulletsNow;

        RaycastHit hit;
        if (Physics.Raycast(_fppCamera.transform.position, _fppCamera.transform.forward,
            out hit, _fireRange
            //, _enemyLayer
            ))
        {
            EnemyAI enemy = hit.transform.GetComponent<EnemyAI>();
            if (enemy != null) enemy.TakeDamage(_damage);

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * _shootForce);
            }

            GameObject gameObject = Instantiate(_impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(gameObject, 2f);
        }
        _shootSource[0].Play();
    }

    public void ReloadGun()
    {
        _bulletsNow = _gunCapacity;
        _slider.value = _bulletsNow;
    }
}