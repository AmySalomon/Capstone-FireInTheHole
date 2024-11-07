using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WeaponClass/Weapon")]
[System.Serializable]
public class WeaponClass : ScriptableObject
{
    public string gunType; //name of the Gun
    public int bulletRange, ammoMax, magazineCount; //How far the bullet travels, how many bullets are in each magazine, how many magazines each gun comes with
    //time in between shots, time it takes to reload magazine, how fast the bullet travels, how hard the screen shakes when a bullet is fired
    public float shootDelay, reloadSpeed, launchForce, screenShake;
    public bool automatic; //whether the player can hold down the shoot button to keep firing
    public bool shotgun; //Remind me to make less hamfisted code later. checks if it should fire like a shotgun
    public AudioClip gunshot; //sound made when player fires

    
    public Sprite gunSprite; //gun visual
    
}
