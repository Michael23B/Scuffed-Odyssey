using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [SerializeField] protected int health = 10;

    public abstract void HandleDamamge(GameObject bullet);
}
