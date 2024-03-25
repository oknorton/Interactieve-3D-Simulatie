using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "NewPotionType", menuName = "Potion Type")]
    public class PotionType : ScriptableObject
    {
        [Header("Potion Properties")]
        public GameObject bulletPrefab; // The bullet GameObject fired by this potion type
        public float fireRate = 15f;    // The rate at which the potion can fire bullets (bullets per second)
        public float fireSpeed = 80f;   // The speed of the bullets fired by the potion
    }
}