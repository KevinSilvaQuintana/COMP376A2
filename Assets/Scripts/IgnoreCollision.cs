using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IgnoreCollision : MonoBehaviour
{
    [SerializeField]
    private List<string> tagsToIgnore; 

    // Use this for initialization
    void Start() {
        foreach (string s in tagsToIgnore)
        {
            GameObject[] objectsToignore = GameObject.FindGameObjectsWithTag(s);
            foreach (GameObject obj in objectsToignore)
            {
                Collider2D otherCollider = obj.GetComponentInChildren<Collider2D>();
                Collider2D thisCollider = gameObject.GetComponentInChildren<Collider2D>();

                Physics2D.IgnoreCollision(thisCollider, otherCollider);
            }
        }     
	}
}
