using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterWeapon : MonoBehaviour
{
    public Sprite[] weaponSprites;
    public enum Weapon {Pistol, Rifle, SawnOff, Shotgun, SMG, Sniper};
    public SpriteRenderer heldWeaponSprite;
    private Fighter fighter;

    public static float[] damage =    { 1,  1,  1,  1,  1,  1 };
    public static float[] bulletSpd = { 1,  1,  1,  1,  1,  1 };
    public static float[] clipSize =  { 1,  1,  1,  1,  1,  1 };
    public static float[] reload =    { 1,  1,  1,  1,  1,  1 };
    public static float[] spread =    { 1,  1,  1,  1,  1,  1 };

    // Start is called before the first frame update
    void Start()
    {
        fighter = gameObject.GetComponent<Fighter>();
        // heldWeaponSprite.sprite = weaponSprites[(int)fighter.weapon];
    }

    public void Shoot()
    {
        // public FighterWeapon.Weapon weapon = FighterWeapon.Weapon.Pistol;
    }
}
