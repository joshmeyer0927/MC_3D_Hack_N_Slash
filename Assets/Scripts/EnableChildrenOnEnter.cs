using UnityEngine;

public class EnableChildrenOnEnter : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Character>() != null)
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
    }

    void OnValidate()
    {
        Collider collider = GetComponent<Collider>();

        if(collider == null)
        {
            collider = gameObject.AddComponent<SphereCollider>();
            ((SphereCollider)collider).radius = 15f;
        }

        collider.isTrigger = true;
    }
}
