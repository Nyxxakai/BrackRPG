using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RigMeshOff : MonoBehaviour {
    public bool MeshIsOff = true;

	void Start ()
    {
        if(Application.isEditor)
        {
            Transform[] allParts = gameObject.GetComponentsInChildren<Transform>();
            foreach (Transform t in allParts)
            {
                MeshRenderer mr = t.gameObject.GetComponent<MeshRenderer>();
                if (mr != null && MeshIsOff)
                {
                    mr.enabled = false;
                }
                else if (mr != null && !MeshIsOff)
                {
                    mr.enabled = true;
                }
            }
        }
	}
	
}
