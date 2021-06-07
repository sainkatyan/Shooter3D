using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Health : MonoBehaviour
{
    public event Action<float, float> HealthUpdate = delegate { };

    private UnitBase unitBase;
    public Transform indicator;
    private Transform _mainCameraTrans;
    private bool _isDead = false;

    private float _currentHealth;
    [SerializeField] private float _startHealth = 100;
    [SerializeField] private float _maxHealth = 100;
    public float startHealth
    {
        get
        {
            return _startHealth;
        }
    }
    public float currentHealth
    {
        get
        {
            return _currentHealth;
        }
        set
        {
            if (value > _maxHealth)
            {
                value = _maxHealth;
            }
            _currentHealth = value;
        }
    }
    public float maxHealth
    {
        get
        {
            return _maxHealth;
        }
    }

    private void Start()
    {
        unitBase = GetComponent<UnitBase>();
        _mainCameraTrans = Camera.main.transform;

        currentHealth = _startHealth;
        _isDead = false;
    }

    private void Update()
    {
        indicator.transform.LookAt(_mainCameraTrans);
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        HealthUpdate?.Invoke(currentHealth, maxHealth);
    }

    public void TakeDamage(DamageModel model)
    {
        float amount = model.damage;
        currentHealth -= amount;

        HealthUpdate?.Invoke(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            if (_isDead == false)
            {
                _isDead = true;
                Kill(model);
            }
        }
    }

    public void Kill(DamageModel damageModel)
    {
        Spawner.KillEnemyUnit(unitBase);
    }
}
