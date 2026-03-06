using System;
using UnityEngine;

public class TestTijuas : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    [SerializeField] private string _animIdle = "";
    [SerializeField] private string _animFall = "";

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _animator.Play(_animFall);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            _animator.Play(_animIdle);
        }
        
        if (Input.GetKey(KeyCode.W))
        {
            Debug .Log("W Pressed");
            transform.position += Vector3.forward * Time.deltaTime * 5f;
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            Debug.Log("S Pressed");
            transform.position += Vector3.back * Time.deltaTime * 5f;
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("A Pressed");
            
            transform.position += Vector3.left * Time.deltaTime * 5f;
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("D Pressed");
            transform.position += Vector3.right * Time.deltaTime * 5f;
        }
    }
    
}
