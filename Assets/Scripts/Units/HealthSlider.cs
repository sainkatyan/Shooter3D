using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Health))]
public class HealthSlider : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider = null;
    public Health Health { get; private set; }
    private void OnEnable()
    {
        Health.HealthUpdate += OnTakeHP;
    }

    private void Awake()
    {
        Health = GetComponent<Health>();

        _healthSlider.maxValue = Health.maxHealth;
        _healthSlider.value = Health.startHealth;
    }


    private void OnTakeHP(float currentHealth, float maxHealth)
    {
        _healthSlider.value = currentHealth;
        _healthSlider.maxValue = maxHealth;
    }

    private void OnDisable()
    {
        Health.HealthUpdate -= OnTakeHP;
    }
}
