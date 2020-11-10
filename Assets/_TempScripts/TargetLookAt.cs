using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLookAt : MonoBehaviour
{
    [SerializeField] private Transform _target = null;
    // Start is called before the first frame update
    private bool _isPress = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (null == _target)
            return;


        if (_isPress)
        {
            Vector3 targetDirection = _target.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            Quaternion lookAt = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * 1000.0f);
            //transform.rotation = lookAt;
            transform.rotation = targetRotation;
        }
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
    #region Implementation

    void OnGUI()
    {
        //return;
        Event e = Event.current;
        if (e.type == EventType.KeyDown)
        {
            if (e.keyCode == KeyCode.Z)
            {
                _isPress = true;
            }
        }

        if (e.type == EventType.KeyUp)
        {
            if (e.keyCode == KeyCode.Z)
            {
                _isPress = false;
            }
        }
    }

    #endregion
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
