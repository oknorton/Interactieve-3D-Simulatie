using UnityEngine;

namespace DefaultNamespace
{
    public enum BulletPattern
    {
        Single,     // Single bullet
        Shotgun,    // Shotgun spread
        Burst,       // Burst fire
        Auto       // Burst fire
    }

    [CreateAssetMenu(fileName = "NewPotionType", menuName = "Potion Type")]
    public class PotionType : ScriptableObject
    {
        [Header("Potion Properties")]
        public GameObject bulletPrefab;     // The bullet GameObject fired by this potion type
        public float fireRate = 15f;        // The rate at which the potion can fire bullets (bullets per second)
        public float fireSpeed = 80f;       // The speed of the bullets fired by the potion
        public BulletPattern bulletPattern; // The pattern of bullet firing

        // Additional properties for specific patterns
        public int numBullets = 1;          // Number of bullets fired in each shot (for shotgun and burst)
        public float spreadAngle = 15f;     // Angle of spread for shotgun bullets (in degrees)
        public float burstInterval = 0.1f;  // Time interval between burst shots (in seconds)
    }
}