using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientManagement : MonoBehaviour
{
    [SerializeField]
    private bool limitFramerate;
    [SerializeField]
    private int maxFrameRate = 300;

    void Start()
    {
        if(limitFramerate)
        {
			Application.targetFrameRate = maxFrameRate;
		}
    }

    void Update()
    {
        
    }
}
