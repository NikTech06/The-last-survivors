using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunHandIkCopyTransform : MonoBehaviour
{
    public bool leftHand = false;
    
    void Update()
    {
        Transform ikTarget;

        if(leftHand)
        {
            ikTarget = GameObject.FindGameObjectWithTag("IkLeftHand").transform;
        }
        else
        {
			ikTarget = GameObject.FindGameObjectWithTag("IkRightHand").transform;
		}

        transform.position = ikTarget.position;
        transform.rotation = ikTarget.rotation;
        transform.localScale = ikTarget.localScale;
    }
}
