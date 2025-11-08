using System.Collections.Generic;
using UnityEngine;

public class ObjectSlow : MonoBehaviour
{
    [Header("Slow Settings")]
    [SerializeField] private float outerRadius = 6f;
    [SerializeField] private float innerRadius = 2f;
    [SerializeField] private LayerMask freezeLayer;

    private HashSet<GameObject> objectsInRange = new HashSet<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckForObjects();
    }

    // Check for objects within the slow radius
    private void CheckForObjects()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, outerRadius, freezeLayer);
        HashSet<GameObject> currentHits = new HashSet<GameObject>();

        if (hits.Length > 0)
        {
            foreach (var hit in hits)
            {
                currentHits.Add(hit.gameObject);

                float distance = Vector3.Distance(transform.position, hit.transform.position);
                if (distance <= innerRadius)
                {
                    FreezeObject(hit.gameObject);
                }
                else
                {
                    SlowObject(hit.gameObject);
                }
            }
        }

        foreach (var obj in objectsInRange)
        {
            if (!currentHits.Contains(obj))
            {
                RestoreSpeed(obj);
            }
        }

        objectsInRange = currentHits;
    }

    // Apply slowing effect to the object
    private void SlowObject(GameObject obj)
    {
        if (obj.CompareTag("Sawblade"))
        {
            if (!obj.GetComponentInParent<SawMovement>().IsFreeze())
            {
                obj.GetComponentInParent<SawMovement>().SetSlow();
            }
        }
        else if (obj.CompareTag("LaserWall"))
        {
            if (!obj.GetComponentInParent<WallMovement>().IsFreeze())
            {
                obj.GetComponentInParent<WallMovement>().SetSlow();
            }
        }
    }

    // Apply freezing effect to the object
    private void FreezeObject(GameObject obj)
    {
        if (obj.CompareTag("Sawblade"))
        {
            if (!obj.GetComponentInParent<SawMovement>().IsFreeze())
            {
                obj.GetComponentInParent<SawMovement>().SetFreeze();
            }
        }
        else if (obj.CompareTag("LaserWall"))
        {
            if (!obj.GetComponentInParent<WallMovement>().IsFreeze())
            {
                obj.GetComponentInParent<WallMovement>().SetFreeze();
            }
        }
    }

    // Restore the object's speed to normal
    private void RestoreSpeed(GameObject obj)
    {
        if (obj.CompareTag("Sawblade"))
        {
            if (!obj.GetComponentInParent<SawMovement>().IsFreeze())
            {
                obj.GetComponentInParent<SawMovement>().ResetSpeed();
            }
        }
        else if (obj.CompareTag("LaserWall"))
        {
            if (!obj.GetComponentInParent<WallMovement>().IsFreeze())
            {
                obj.GetComponentInParent<WallMovement>().ResetSpeed();
            }
        }
    }
}
