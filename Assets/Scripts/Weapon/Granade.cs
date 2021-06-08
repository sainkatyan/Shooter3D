using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granade : MonoBehaviour
{
    private float _radius = 5f;
    private DamageModel _damageModel;
    public DamageModel SetDamageModel
    {
        set
        {
            _damageModel = value;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

    public void ExplosionDamage(Vector3 center)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, _radius);
        foreach (var hitCollider in hitColliders)
        {
            CheckIUnit(hitCollider);
        }
    }
    private void CheckIUnit(Collider collider)
    {
        Health healthComponent = null;
        collider.transform.TryGetComponent<Health>(out healthComponent);
        if (healthComponent != null)
        {
            float distance = Vector3.Distance(transform.position, collider.transform.position);
            float damage = _damageModel.damage - _damageModel.damage / _radius * distance;
            if (damage <= 0f)
            {
                damage = 0f;
            }
            Debug.Log("DAMAGE " + damage + " DISTANCE " + distance);
            healthComponent.TakeDamage(_damageModel, damage);

            //Ray ray = new Ray(transform.position, collider.bounds.center);
            //if (IsShooting(ray, collider))
            //{
            //    Debug.Log("DAMAGE " + damage + " DISTANCE " + distance);
            //    healthComponent.TakeDamage(_damageModel, damage);
            //}
        }
    }
    private bool IsShooting(Ray ray, Collider collider)
    {
        RaycastHit hit;
        var hitchek = Physics.Raycast(ray, out hit);
        return hitchek;
    }
}
