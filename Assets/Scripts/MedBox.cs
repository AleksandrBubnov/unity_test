using UnityEngine;
using UnityEngine.UI;

public class MedBox : MonoBehaviour
{
    [SerializeField] int _capacity = 25;
    [SerializeField] string _nameAid = "low aid";
    [SerializeField] float _useRange = 3f;
    //[SerializeField] Text _canUseUI;

    //private Transform _playerTransform;

    //void Start()
    //{
    //    //_playerTransform = GameObject.FindObjectOfType<CharacterController>().transform;
    //}

    //void Update()
    //{
    //    //if (Vector3.Distance(transform.position, _playerTransform.position) < _useRange)
    //    //    CanUse();
    //    //else
    //    //    CanNotUse();
    //}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _useRange);
    }

    //public void CanNotUse()
    //{
    //    _canUseUI.gameObject.SetActive(false);
    //    _canUseUI.text = "Can Use(E)";
    //    _canUseUI.color = Color.gray;
    //}
    public void CanUse()
    {
        //_canUseUI.gameObject.SetActive(true);
        //_canUseUI.text = $"Use(E) [{_nameAid}]";
        //if (Player.Instance.Health >= 100)
        //    _canUseUI.color = Color.gray;
        //else
        //    _canUseUI.color = Color.white;

        if (Input.GetKeyDown(KeyCode.E) && Player.Instance.Health < Player.Instance.HealthMax)
        {
            Player.Instance.AddHealth(_capacity);
            if (Player.Instance.Health > Player.Instance.HealthMax) 
                Player.Instance.SetHealth(Player.Instance.HealthMax);
            //CanNotUse();
            Destroy(gameObject);
        }
    }
    public string GetName()
    {
        return _nameAid;
    }
}
