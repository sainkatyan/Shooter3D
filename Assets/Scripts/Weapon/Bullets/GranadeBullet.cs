using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranadeBullet : MonoBehaviour
{
    private List<Vector3> _positions;
    private float speed = 10f;
    private float _radius = 5f;

    private DamageModel _damageModel;
    public void Shoot(List<Vector3> tempPositions, DamageModel damageModel)
    {
        this._damageModel = damageModel;
        this._positions = tempPositions;
        StartCoroutine(MoveGranade());
    }

    private IEnumerator MoveGranade()
    {
        transform.position = _positions[0];
        foreach (Vector3 _item in _positions)
        {
            Vector3 itemPos = _item;
            while (Vector3.Distance(transform.position, itemPos) > .0001)
            {
                transform.position = Vector3.MoveTowards(transform.position, itemPos, speed * Time.deltaTime);
                yield return null;
            }
        }
        ExplosionDamage();
        Destroy(this.gameObject);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

    public void ExplosionDamage()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _radius);
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
            float damage = GetDamage(distance);

            var rayDirection = (collider.bounds.center - transform.position).normalized;
            Ray ray = new Ray(transform.position, rayDirection);

            if (IsShooting(ray, collider))
            {
                healthComponent.TakeDamage(_damageModel, damage);
            }
        }
    }

    private float GetDamage(float tempDistance)
    {
        var damage = _damageModel.damage - _damageModel.damage / _radius * tempDistance;
        if (damage <= 0f)
        {
            damage = 0f;
        }
        return damage;
    }

    private bool IsShooting(Ray ray, Collider collider)
    {
        RaycastHit hit;
        var hitchek = Physics.Raycast(ray, out hit);
        if (hitchek)
        {
            if (hit.collider == collider)
            {
                //Debug.DrawLine(ray.origin, hit.point, Color.red, 3f);
                return true;
            }
            else
            {
                //Debug.DrawRay(ray.origin, ray.direction * 10f, Color.blue, 5f);
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}
