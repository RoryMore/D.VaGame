using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    // Modifyable
    // Multiple tag support
    [SerializeField] List<string> tagsToScanFor = null;
    [SerializeField] float scanSize = 5.0f;
    [SerializeField] Color triggeredColor = Color.yellow;
    [SerializeField] Color standardColor = Color.green;

    List<GameObject> objectsInRange = new List<GameObject>();
    List<GameObject> invalidObjects = new List<GameObject>();

    SphereCollider scanArea;
    Vector3 lastKnownObjectLocation;


    public Vector3 LastKnownObjectLocation { get => lastKnownObjectLocation; }
    public List<GameObject> ObjectsInRange { get => objectsInRange; }

    public void SetupScanner(string tagToScanFor, float radius)
    {
        // Add it to the existing string values



        tagsToScanFor = new List<string>();
        tagsToScanFor.Add(tagToScanFor);

        scanArea = gameObject.AddComponent<SphereCollider>();

        scanArea.isTrigger = true;
        scanSize = radius;
        scanArea.radius = radius;
    }

    private void Awake()
    {
        


    }

    public GameObject GetClosestTargetInRange()
    {
        // If there are no zombies in range, return nothing
        if (objectsInRange.Count == 0)
            return null;

        // Remove invalid objects from the range
        RemoveInvalidObjects();

        // If there are no zombies in range, return nothing
        if (objectsInRange.Count == 0)
            return null;

        GameObject closestObject = objectsInRange[0];

        // Find The closest zombie out of the zombies that are in range
        foreach (GameObject objectInRange in objectsInRange)
        {
            if (Vector3.Distance(objectInRange.transform.position, transform.position) <
                Vector3.Distance(closestObject.transform.position, transform.position))
            {
                // Store it as the closest Zombie
                closestObject = objectInRange;
            }
        }

        return closestObject;
    }

    public void RemoveInvalidObjects()
    {
        // Find the objects that need to be deleted
        foreach (GameObject objectInRange in objectsInRange)
        {
            if (!objectInRange)
            {
                invalidObjects.Add(objectInRange);
            }
        }

        // Remove the invalid objects
        foreach (GameObject invalidObject in invalidObjects)
        {
            objectsInRange.Remove(invalidObject);
        }

        invalidObjects.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check for all tags
        foreach (string tagToScanFor in tagsToScanFor)
        {
            if (other.gameObject.CompareTag(tagToScanFor))
            {
                // Store it as the last known object
                if (objectsInRange.Count == 0)
                {
                    lastKnownObjectLocation = other.gameObject.transform.position;
                }

                objectsInRange.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check for all tags
        foreach (string tagToScanFor in tagsToScanFor)
        {
            if (other.gameObject.CompareTag(tagToScanFor))
            {
                // Store it as the last known object
                if (objectsInRange.Count == 1)
                {
                    lastKnownObjectLocation = other.gameObject.transform.position;
                }

                objectsInRange.Remove(other.gameObject);
            }
        }
        
    }

    private void OnDrawGizmos()
    {
        
        // Make the sphere yellow when there are zombies around
        if (objectsInRange.Count > 0)
        {
            Gizmos.color = triggeredColor;
        }
        else
        {
            Gizmos.color = standardColor;
        }

        Gizmos.DrawWireSphere(transform.position, scanSize);
    }
}
