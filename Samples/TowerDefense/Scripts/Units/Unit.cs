using System.Collections;
using System.Collections.Generic;
using HexTecGames.Basics;
using UnityEngine;

namespace HexTecGames.UpgradeSystem.TowerDefense
{
    [System.Serializable]
    public class Unit : MonoBehaviour
    {
        [SerializeField] private UnitStats unitStats;

        private int secondsPerAttack = 1;
        private float attackTimer = 0;

        public PermissionGroup Enable
        {
            get
            {
                return enable;
            }
            private set
            {
                enable = value;
            }
        }
        private PermissionGroup enable = new PermissionGroup();


        protected virtual void FixedUpdate()
        {
            if (!enable.Allowed)
            {
                return;
            }

            attackTimer += Time.deltaTime * (unitStats.AttackSpeed / 100f);
            if (attackTimer > secondsPerAttack)
            {
                Attack();
                attackTimer = 0;
            }
        }

        public void Setup(UnitStats unitStats)
        {
            this.unitStats = unitStats;
        }

        public List<Upgrade> GetAllValidUpgrades()
        {
            List<Upgrade> validUpgrades = new List<Upgrade>();
            validUpgrades.AddRange(unitStats.GetAllValidUpgrades());
            return validUpgrades;
        }

        public void Attack()
        {
            AttackData attackData = unitStats.GetAttackData();
            //damageTextController.RequestDamageText(transform.position, attackData.damage, attackData.isCrit);
        } 
    }
}