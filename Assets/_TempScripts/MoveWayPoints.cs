using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWayPoints : MonoBehaviour
{
    [SerializeField] private Transform _point00 = null;
    [SerializeField] private Transform _point01 = null;
    [SerializeField] private Transform _currentTarget = null;

    [SerializeField] private float speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentTarget == null)
            return;

        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, _currentTarget.position, step);
        if (Vector3.Distance(transform.position, _currentTarget.position) < 0.001f)
        {
            if (_currentTarget == _point00)
            {
                _currentTarget = _point01;
            }
            else
            {
                _currentTarget = _point00;
            }
        }
    }
}
