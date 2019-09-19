using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;

//The abstract keyword enables you to create classes and class members that are incomplete and must be implemented in a derived class.
public abstract class Interactable : MonoBehaviour
{
    public bool isMovementBlocker = false;

    protected virtual void Start ()
    {

    }
    public virtual void Interact ()
    {
        print("Interaction");
    }
}
