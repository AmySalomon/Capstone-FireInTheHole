using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[CreateAssetMenu(menuName = "PowerUps/Scattershot")]
public class scr_PU_scattershot : scr_powerUpEffect
{
    public bool active;
    public GameObject puItem;
    public GameObject obj_PUscattershot;
    public float spawnOffset = 0.5f;

    public override void ApplyEffect(GameObject player)
    {
        AddChild(obj_PUscattershot, player);
        
    }

    public override void RemoveEffect(GameObject player)
    {
        RemoveChild(obj_PUscattershot);
    }


    void AddChild(GameObject child, GameObject player)
    {
        puItem = Instantiate(child, player.transform);
       // puItem.GetComponent<scr_scattershotChild>().player = player;
        puItem.gameObject.SetActive(true);
    }

    void RemoveChild(GameObject child)
    {
        child.gameObject.SetActive(false);
    }
}

/*
 * Grab direction of force
 * instantiate balls on an angle of that force (15^ right, 15^ left)
 * Apply force in direction plus angle changed (apply force in opposite direction of player)
 * 
 * Bug? idk, the script disables the power up object and creates a new one i dont wanna deal with it rn just get the physics done lol
 */