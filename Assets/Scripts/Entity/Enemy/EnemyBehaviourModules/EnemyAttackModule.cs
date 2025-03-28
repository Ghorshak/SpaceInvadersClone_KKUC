using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SpaceInvadersClone.DesignPatterns.ObjectPooling;
using UnityEngine;

namespace SpaceInvadersClone.Entities.Enemies
{
    public class EnemyAttackModule
    {
        private AttackBehaviour Data { get; set; }

        private Dictionary<int, List<Enemy>> EnemiesByColumnIndex;
        private ObjectPooler ProjectilePooler;

        private float timer;
        private float currentAttackRateMin;
        private float currentAttackRateMax;
        private float minDelayBetweenAttacks;

        private Coroutine coroutine;
        private System.Random random = new();

        public void Initialize(AttackBehaviour data, Dictionary<int, List<Enemy>> enemiesByColumnIndex, ObjectPooler projectilePooler)
        {
            Data = data;
            EnemiesByColumnIndex = enemiesByColumnIndex;
            ProjectilePooler = projectilePooler;

            currentAttackRateMin = Data.AttackRateMin;
            currentAttackRateMax = Data.AttackRateMax;
            minDelayBetweenAttacks = Data.MinDelayBetweenAttacks;
        }

        public void UpdateState()
        {
            if (minDelayBetweenAttacks > 0 && coroutine == null)
                minDelayBetweenAttacks -= Time.deltaTime;

            if (EnemiesByColumnIndex.Count > 0 && minDelayBetweenAttacks <= 0)
            {
                ProcessAttacks();
            }

            ProcessAttackRateIncrease();
        }

        private void ProcessAttackRateIncrease()
        {
            if (timer > Data.AttackRateIncreaseInterval)
            {
                timer = 0;
                currentAttackRateMin *= Data.AttackRateIncreaseMultiplier;
                currentAttackRateMax *= Data.AttackRateIncreaseMultiplier;
            }
        }

        private void ProcessAttacks()
        {
            minDelayBetweenAttacks = Data.MinDelayBetweenAttacks;

            ProjectilePooler.StartCoroutine(Attack());
        }

        private IEnumerator Attack()
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(currentAttackRateMin, currentAttackRateMax));

            if (EnemiesByColumnIndex.Count > 0)
            {
                Enemy enemy = GetRandomEnemy();
                if (enemy != null)
                {
                    ProjectilePooler.SpawnObjectFromPool(enemy.GunTransform.position);
                    coroutine = null;
                }
            }
        }

        private Enemy GetRandomEnemy()
        {
            var randomKey = EnemiesByColumnIndex.Keys.ElementAt(random.Next(EnemiesByColumnIndex.Count));
            if (EnemiesByColumnIndex.TryGetValue(randomKey, out var value) && value.Count > 0)
            {
                return value[random.Next(value.Count)];
            }

            return null;
        }
    }
}
