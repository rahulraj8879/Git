using UnityEngine;

public class Node : MonoBehaviour
{
    public int rotationState = 0;
    public int maxRotationState = 3;

    private EdgeCollider[] edgeColliders;
    private SpriteRenderer spriteRenderer;
    private LevelManager levelManager;

    // New boolean to track if the node is green
    public bool isGreen = false;
    public bool disabletouch = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        edgeColliders = GetComponentsInChildren<EdgeCollider>();
        levelManager = GameManager.instance.levelManager;
        CheckConnections();
    }

    void OnMouseDown()
    {
        if (!disabletouch)
        {
            RotateNode();
            CheckConnections();
        }
    }

    void RotateNode()
    {
        rotationState = (rotationState + 1) % (maxRotationState + 1);
        transform.Rotate(0, 0, 90);
    }

    public void CheckConnections()
    {
        bool isAnyColliderConnected = IsAnyColliderConnectedToDifferentNode();
        bool isAllConnected = AreAllCollidersConnected();

        if (isAllConnected)
        {
           // spriteRenderer.color = Color.blue; // Color for level completion
            isGreen = true;
        }
        else
        {
            if (isAnyColliderConnected)
            {
                spriteRenderer.color = Color.green;
                isGreen = true;
            }
            else
            {
                spriteRenderer.color = Color.red;
                isGreen = false;
            }
        }

        // Notify the LevelManager of the node state change
        if (levelManager != null)
        {
            levelManager.NotifyNodeStateChange();
        }
    }

    bool IsAnyColliderConnectedToDifferentNode()
    {
        foreach (EdgeCollider edgeCollider in edgeColliders)
        {
            if (edgeCollider.isConnected)
            {
                return true; // Assume connection to another node if `isConnected` is true
            }
        }
        return false;
    }

    bool AreAllCollidersConnected()
    {
        foreach (EdgeCollider edgeCollider in edgeColliders)
        {
            if (!edgeCollider.isConnected)
            {
                return false; // Found at least one disconnected collider
            }
        }
        return true; // All colliders are connected
    }
}
