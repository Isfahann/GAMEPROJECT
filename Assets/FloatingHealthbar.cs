using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FloatingHealthbar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Camera uiCamera;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
   

    public void UpdateHealthbar(float currentValue, float maxValue)
    {
        slider.value = currentValue/maxValue;
    }

    void Start()
    {
        Debug.Log("Camera assigned: " + (uiCamera != null));
    }

    void Awake()
    {
        if (uiCamera == null)
        {
            uiCamera = Camera.main; // Assign the main camera if not already set
        }
    }
    void Update()
    {
        transform.rotation = uiCamera.transform.rotation;
        transform.position = target.position + offset;
    }
}
