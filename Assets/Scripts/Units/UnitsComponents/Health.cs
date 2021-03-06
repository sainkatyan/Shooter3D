using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Health : MonoBehaviour
{
    public event Action<float, float> HealthUpdate = delegate { };

    private Color neutralColor = Color.white;
    private Color playerColor = Color.blue;
    private Color enemyColor = Color.red;

    public Transform indicator;
    public Image ProgressBar;

    private UnitBase _unitBase;
    private Transform _mainCameraTrans;

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
        _unitBase = GetComponent<UnitBase>();
        SetProgressBarColor(_unitBase);
        if (_mainCameraTrans == null)
        {
            _mainCameraTrans = Camera.main.transform;
        }
        currentHealth = _startHealth;
    }

    private void SetProgressBarColor(UnitBase unitBase)
    {
        switch (unitBase.fraction)
        {
            case FractionUnit.Neutral:
                ProgressBar.color = neutralColor;
                break;
            case FractionUnit.Blue:
                ProgressBar.color = playerColor;
                break;
            case FractionUnit.Red:
                ProgressBar.color = enemyColor;
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        if (_mainCameraTrans != null)
        {
            indicator.transform.LookAt(_mainCameraTrans);
        }
    }

    public void FindMainCamera()
    {
        _mainCameraTrans = Camera.main.transform;
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
            if (_unitBase.IsDead == false)
            {
                _unitBase.IsDead = true;
                Kill(model);
            }
        }
    }
    //granade
    public void TakeDamage(DamageModel model, float damage)
    {
        float amount = damage;
        currentHealth -= amount;

        HealthUpdate?.Invoke(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            if (_unitBase.IsDead == false)
            {
                _unitBase.IsDead = true;
                Kill(model);
            }
        }
    }

    public void Kill(DamageModel damageModel)
    {
        Spawner.KillEnemyUnit(_unitBase, damageModel);

        EventFeed.instance.CreatePanel(damageModel.idAttacker, damageModel.idWeapon, _unitBase.UnitBaseName);
    }
}
