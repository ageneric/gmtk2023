using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterWeapon : MonoBehaviour
{
    public Sprite[] weaponSprites;
    public enum Weapon {Pistol, Rifle, SawnOff, Shotgun, SMG, Sniper};
    public SpriteRenderer heldWeaponSprite;
    private Fighter fighter;

    public static float[] damage =    { 1,     1.25f,  3,      2,      0.25f, 5 };
    public static float[] bulletSpd = { 1,     1.5f,   0.75f,  0.75f,  1,     3 };
    public static float[] clipSize =  { 3,     1,      2,      1,      20,    1 };
    public static float[] reload =    { 1,     1.5f,   1,      1,      1,     4 };
    public static float[] spread =    { 0.5f,  0.25f,  3,      2,      1.5f,  0.1f };

    // Start is called before the first frame update
    void Start()
    {
        fighter = gameObject.GetComponent<Fighter>();
        fighter.weapon = (Weapon)Random.Range(0, 6);
        heldWeaponSprite.sprite = weaponSprites[(int)fighter.weapon];
    }

    public void Shoot()
    {

    }
}
