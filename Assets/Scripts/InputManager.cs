using System;
using UnityEngine;
using UnityEngine.Events;
public class InputManager : MonoBehaviour
 {
    public UnityEvent OnSpacePressed = new UnityEvent();
    public UnityEvent<Vector3> OnMove = new UnityEvent<Vector3>();
    public UnityEvent<Vector3> OnDash = new UnityEvent<Vector3>();
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnSpacePressed?.Invoke();
        }
        

        //Movement
        Vector3 input = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            input += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            input += Vector3.left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            input += Vector3.back;
        }
        if (Input.GetKey(KeyCode.D))
        {
            input += Vector3.right;
        }
        OnMove?.Invoke(input);

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            OnDash?.Invoke(input);
        }
    }
 }