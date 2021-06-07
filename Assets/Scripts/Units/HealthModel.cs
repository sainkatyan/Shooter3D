using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthModel
{
    private float _currentHealth;
    private float _maxHealth;
    public float CurrentHealth
    {
        get
        {
            return _currentHealth;
        }
    }
    public float MaxHealth
    {
        get
        {
            return _maxHealth;
        }
    }

    public HealthModel(float currentHealth, float maxHealth)
    {
        _currentHealth = currentHealth;
        _maxHealth = maxHealth;
    }
}
