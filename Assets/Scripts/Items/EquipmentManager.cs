using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    public static EquipmentManager instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    public GameObject targetMesh;
    public Equipment[] DefaultItems;

    Equipment[] currentEquipment;
    GameObject[] currentMeshes;

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    Inventory inventroy;

    private Stitcher stitcher;
    private GameObject equipItem;

    private void Start()
    {
        stitcher = new Stitcher();

        // original setup
        inventroy = Inventory.instance;
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
        currentMeshes = new GameObject[numSlots];

        EquipDefaultItems();
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot;
        Equipment oldItem = Unequip(slotIndex);

        if(onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        currentEquipment[slotIndex] = newItem;

        GameObject newMesh = Instantiate<GameObject>(newItem.mesh);
        Wear(newMesh);
        currentMeshes[slotIndex] = newMesh;
    }

    public Equipment Unequip(int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            if (currentMeshes[slotIndex] != null)
            {
                DestroyImmediate(currentMeshes[slotIndex].gameObject, true);
            }
            Equipment oldItem = currentEquipment[slotIndex];
            inventroy.Add(oldItem);

            currentEquipment[slotIndex] = null;

            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }

            return oldItem;
        }
        return null;
    }

    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }
        EquipDefaultItems();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UnequipAll();
        }
    }

    bool isEquiping = false;
    void EquipDefaultItems()
    {
        if (!isEquiping)
        {
            StartCoroutine(CoEquipDefaultItems());
        }
    }
    IEnumerator CoEquipDefaultItems()
    {
        isEquiping = true;
        foreach (Equipment item in DefaultItems)
        {
            Equip(item);
            yield return new WaitForSeconds(.1f);
        }
        isEquiping = false;
    }

    private void Wear(GameObject clothing)
    {
        if (clothing == null)
            return;
        //clothing = Instantiate(clothing);
        clothing = stitcher.Stitch(clothing, targetMesh);
    }
}
