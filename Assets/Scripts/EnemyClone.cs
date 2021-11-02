using System.Collections;
using UnityEngine;

public class EnemyClone : MonoBehaviour
{
    [SerializeField] EnemyAI _enemyAI;
    [SerializeField] float _timeNewClone = 30f;

    [SerializeField] float _minX = 0;
    [SerializeField] float _maxX = 10;
    [SerializeField] float _minZ = 0;
    [SerializeField] float _maxZ = 10;

    private IEnumerator _corotine;
    void Start()
    {
        _corotine = MakeClone(_timeNewClone);
        StartCoroutine(_corotine);
    }

    private IEnumerator MakeClone(float waitTime)
    {
        //Debug.Log($"_enemyAI: {_enemyAI.name}");
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            //print("MakeClone: " + Time.time);
            float x = Random.Range(_minX, _maxX);
            float z = Random.Range(_minZ, _maxZ);
            EnemyAI clone = Instantiate(_enemyAI,
                new Vector3(x, 0, z),
                transform.rotation);
        }
    }
}
