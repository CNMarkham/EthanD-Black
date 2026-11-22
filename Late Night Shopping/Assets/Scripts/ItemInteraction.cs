using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : MonoBehaviour
{
    public Camera playerCamera;     
    public Transform holdPoint;     
    public float pickupRange = 3f;  

    private GameObject heldItem;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // 0 = Left Mouse Button
        if (Input.GetMouseButtonDown(0))
        {
            if (heldItem == null)
            {
                TryPickUp();
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (heldItem != null)
            {
                DropItem();
            }
        }
    }

    void TryPickUp()
    {
        // Creates an invisible laser beam from the exact center of the screen
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        // Shoots the beam forward up to 'pickupRange' meters
        if (Physics.Raycast(ray, out hit, pickupRange))
        {
            // Checks if the object hit has the "Item" tag
            if (hit.collider.CompareTag("Item"))
            {
                PickUpItem(hit.collider.gameObject);
            }
        }
    }

    void PickUpItem(GameObject item)
    {
        heldItem = item;

        // 1. Turn off gravity and physics so it stays floating in your hand
        Rigidbody rb = heldItem.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        // 2. Turn off its collider so it doesn't bump into your player body
        Collider col = heldItem.GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = false;
        }

        // 3. Parent the item to your hand and snap it to the exact spot
        heldItem.transform.SetParent(holdPoint);
        heldItem.transform.localPosition = Vector3.zero;
        heldItem.transform.localRotation = Quaternion.identity;
    }

    void DropItem()
    {
        if (heldItem == null)
        {
            return;
        }

        // 1. Turn off gravity and physics so it stays floating in your hand
        Rigidbody rb = heldItem.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
        }

        // 2. Turn off its collider so it doesn't bump into your player body
        Collider col = heldItem.GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = true;
        }

        // 3. Parent the item to your hand and snap it to the exact spot
        heldItem.transform.SetParent(null);

        heldItem = null;
    }
}
