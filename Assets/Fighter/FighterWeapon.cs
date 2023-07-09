using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterWeapon : MonoBehaviour
{
    public Sprite[] weaponSprites;
    public enum Weapon {Pistol, Rifle, SawnOff, Shotgun, SMG, Sniper};

    public Weapon weapon = Weapon.Pistol;
    public SpriteRenderer heldWeaponSprite;

    public void Start()
    {
        heldWeaponSprite.sprite = weaponSprites[(int)weapon];
    }

    public void Shoot()
    {

    }
}

public class Rifle
{

}
