using UnityEngine;

public class EdgeCollider : MonoBehaviour
{
    public enum ColliderType
    {
        Type1 = 0,
        Type2 = 1,
        Type3 = 2,
        Type4 = 3 // Add more types if needed
    }

    public bool isConnected = false;
    public ColliderType colliderType;
    private Node parentNode;

    void Start()
    {
        parentNode = GetComponentInParent<Node>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        EdgeCollider otherEdgeCollider = other.GetComponent<EdgeCollider>();
        if (otherEdgeCollider != null && otherEdgeCollider.colliderType == colliderType)
        {
            isConnected = true;
            if (parentNode!=null)
            {
                parentNode.CheckConnections();
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        EdgeCollider otherEdgeCollider = other.GetComponent<EdgeCollider>();
        if (otherEdgeCollider != null && otherEdgeCollider.colliderType == colliderType)
        {
            isConnected = false;
            if (parentNode != null)
            {
                parentNode.CheckConnections();
            }
        }
    }
}
