using UnityEngine;

public class ZombieFight : MonoBehaviour
{
    [SerializeField] int _damage = 10;

    private void Hit()
    {
        if (Player.Instance._isDead) return;

        Player.Instance.TakeDamage(_damage);
    }
}