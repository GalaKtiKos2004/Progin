using System;
using System.Collections;
using UnityEngine;

public class PlayerFighter : Fighter
{
    [SerializeField] private EqupimentManager _equipmentManager;
    [SerializeField] private SceneController _sceneController;
    [SerializeField] private Inventory _inventory; // временно
    [SerializeField] private float _delay = 3f;

    private WaitForSeconds _waitForSeconds;

    public event Action Attacked;
    public event Action Died;

    private void Awake()
    {
        _waitForSeconds = new WaitForSeconds(_delay);
    }

    private void Update()
    {
        if (IsCooldown)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attacked?.Invoke();

            if (InTrigger && CollidedObject.TryGetComponent(out EnemyFighter enemyFighter))
            {
                Attack(enemyFighter);
            }
        }
    }

    protected override void OnDied()
    {
        Died?.Invoke();
        StartCoroutine(DeathDelay());
    }

    protected override float GetTotalDamage()
    {
        float baseDamage = base.GetTotalDamage();
        float weaponBonus = _equipmentManager.Weapon != null ? _equipmentManager.Weapon.Damage : 0;
        return baseDamage + weaponBonus;
    }

    protected override float GetTotalDefense()
    {
        float baseDefense = base.GetTotalDefense();
        float armorBonus = _equipmentManager.Armor != null ? _equipmentManager.Armor.Def : 0;
        float shieldBonus = _equipmentManager.Shield != null ? _equipmentManager.Shield.Def : 0;
        float helmetBonus = _equipmentManager.Helmet != null ? _equipmentManager.Helmet.Def : 0;
        return baseDefense + armorBonus + shieldBonus + helmetBonus;
    }

    private IEnumerator DeathDelay()
    {
        yield return _waitForSeconds;
        
        _equipmentManager.Clear();
        _sceneController.LoadScene("SampleScene", _inventory); // временно
    }
}
