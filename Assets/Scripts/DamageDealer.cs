using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DamageDealer : MonoBehaviour
{
    [FormerlySerializedAs("damage")] [SerializeField]
    private int _damage = 100;

    public int Damage
    {
        get { return _damage; }
    }

    public void DestroyOnHit()
    {
        Destroy(gameObject);
    }
}