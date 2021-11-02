using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Renderer _renderer;
    [SerializeField] private float damage;
    [HideInInspector] public float health;
    public Vector3 _vector3;


    private GameManager _gameManager;
    private Rigidbody _rigidbody;

    [SerializeField] private float _forwardForce;
    [SerializeField] private float _rotateForce;

    void Start()
    {
        #region get/add component
        //if (GetComponent<Renderer>() == null) this.gameObject.AddComponent<Renderer>();
        //_renderer = GetComponent<Renderer>();

        //if (!TryGetComponent<Renderer>(out Renderer _renderer)) { this.gameObject.AddComponent<Renderer>(); }

        //_renderer = GetComponent<Renderer>();
        //_renderer.material.color = new Color(0, 0, 0);
        #endregion
        #region FindObjectOfType / ofTag
        //_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        //_gameManager = GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>();
        #endregion
        #region GetInstance
        _gameManager = GameManager.GetInstance();
        if (!TryGetComponent<Rigidbody>(out _rigidbody))
        {
            this.gameObject.AddComponent<Renderer>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        #endregion
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_gameManager != null)
            {
                //_gameManager.AddScore(1);
                Debug.Log($"score: {_gameManager.GetScore()}");
            }
        }
    }

    private void FixedUpdate()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        //_rigidbody.AddForce(_vector3);
        //_rigidbody.AddForce(new Vector3(0, 0, 0));
        //_rigidbody.AddForce(new Vector3(0, 0, vertical));
        //_rigidbody.AddForce(Vector3.forward * vertical * _forwardForce * Time.deltaTime,ForceMode.VelocityChange);
        ///
        /// Time.deltaTime - перемещение за единицу времени, без него за кадр
        /// transform.forward - используем для локальные координаты
        /// AddForce
        /// AddTorque
        /// 
        _rigidbody.AddForce(
            transform.forward * vertical * _forwardForce * Time.deltaTime,
            ForceMode.VelocityChange
            );
        _rigidbody.AddTorque(
            transform.up * horizontal * _rotateForce * Time.deltaTime,
            ForceMode.VelocityChange
            );
    }
}
