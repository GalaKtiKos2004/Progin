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
    private bool _canPress = true;

    public event Action Attacked;
    public event Action Died;

    private void Awake()
    {
        _waitForSeconds = new WaitForSeconds(_delay);
    }

    private void Update()
    {
        if (IsCooldown || _canPress == false)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attacked?.Invoke();
            _canPress = false;
        }
    }

    protected override void OnDied()
    {
        Died?.Invoke();
        StartCoroutine(DeathDelay());
    }

    protected override float GetTotalDamage()
    {
        Debug.Log("Attack");
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

    private void PreformAttack()
    {
        Debug.Log("Preform");
        
        if (InTrigger && CollidedObject.TryGetComponent(out EnemyFighter enemyFighter))
        {
            Debug.Log("Preform Attack");
            Attack(enemyFighter);
        }

        _canPress = true;
    }

    private IEnumerator DeathDelay()
    {
        yield return _waitForSeconds;

        _equipmentManager.Clear();
        _sceneController.LoadScene("SampleScene", _inventory); // временно
    }
}
