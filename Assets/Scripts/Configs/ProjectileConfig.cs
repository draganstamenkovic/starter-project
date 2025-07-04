using System;
using System.Collections.Generic;
using Gameplay;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "ProjectileConfig", menuName = "Configs/ProjectileConfig")]
    public class ProjectileConfig : ScriptableObject
    {
        public List<ProjectileData> projectilePrefabs;
        public int defaultCapacity = 10;
        public int maxCapacity = 10;
        public bool collectionChecks = true;
        
        public Projectile GetProjectilePrefab(string projectileId)
        {
            var projectileData = projectilePrefabs.Find(x => x.ID == projectileId);
            if (projectileData == null)
            {
                Debug.LogWarning($"No projectile found with ID {projectileId}");
                Debug.Log("We will use default one");
                return projectilePrefabs[0].projectile;
            }

            return projectileData.projectile;
        }

        [Serializable]
        public class ProjectileData
        {
            public Projectile projectile;
            public string ID;
        }
    }
}