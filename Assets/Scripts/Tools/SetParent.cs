using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetParent : MonoBehaviour {

    public string ParentName;
    public Transform GoParent;

	void Start ()
    {
        Transform[] allParts = transform.parent.GetComponentsInChildren<Transform>();

        if(GoParent != null)
        {
            transform.SetParent(GoParent);
        }
        else
        {
            foreach (Transform t in allParts)
            {
                if(t.name.Contains(ParentName))
                {
                    GoParent = t;
                    transform.SetParent(t);
                }
            }
        }
	}
}
