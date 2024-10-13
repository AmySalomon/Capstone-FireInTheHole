using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WeaponClass/Weapon")]
[System.Serializable]
public class WeaponClass : ScriptableObject
{
    public string gunType; //name of the Gun
    public int bulletRange, ammoCount, magazineCount; //How far the bullet travels, how many bullets are in each magazine, how many magazines each gun comes with
    public float shootDelay, reloadSpeed, launchForce; //time in between shots, time it takes to reload magazine, how fast the bullet travels
    public Sprite gunSprite; //gun visual

}
