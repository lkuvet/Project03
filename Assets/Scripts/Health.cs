using JetBrains.Annotations;
using System;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] public int _currentHealth = 100;
    [SerializeField] private ParticleSystem _deathParticles = null;
    [SerializeField] private int _healthMax = 100;

    [SerializeField] AudioClip _damagedSound = null;

    [SerializeField] AudioClip _killSound = null;
    public UnityEvent OnKilled;

    public event Action<float> OnPlayerDamaged = delegate { };

    public event Action<float> OnPlayerHealed = delegate { };
    public void TakeDamage(int amount)
    {
        _currentHealth -= amount;

        if (TryGetComponent<PlayerCharacter>(out var player))
        {
            OnPlayerDamaged?.Invoke(amount);
        }


        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            Kill();
        }
    }

    public void Kill()
    {
        Debug.Log("Death!");
        OnKilled?.Invoke();

        Destroy(gameObject);

        if (_deathParticles != null)
        {
            Instantiate(_deathParticles, transform.position, transform.rotation);
        }
    }
}
