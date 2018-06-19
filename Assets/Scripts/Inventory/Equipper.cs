using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipper : MonoBehaviour {

    public GameObject avatar; //Drag human model to this slot, the whole model + Armature
    public GameObject wornClothing;

    private Stitcher stitcher;

    public void Awake()
    {
        stitcher = new Stitcher();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            SomeFunctionThatAddsEquipment();
        }
    }

    public void SomeFunctionThatAddsEquipment()
    {
        Wear(wornClothing); //put the gameobject you want spawned here);
    }

    public void SomeFunctionThatRemovesEquipment()
    {
        RemoveWorn();
    }

    private void RemoveWorn()
    {
        if (wornClothing == null)
            return;
        Destroy(wornClothing);
    }

    private void Wear(GameObject clothing)
    {
        if (clothing == null)
            return;
        clothing = Instantiate(clothing);
        wornClothing = stitcher.Stitch(clothing, avatar);
    }
}
