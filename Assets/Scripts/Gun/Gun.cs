using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Gun : MonoBehaviour
{
    [HideInInspector] public GunType type;
    public float damage;
    public float accuracy;
    public float recoil;
    public float reloadSpeed;
    public int magazineSize;
    public int maxAmmo;
    protected Transform gunHolder;
    protected Transform gunTip;
    Animator anim;
    Stats unitStats;
    [HideInInspector] public int ammo;
    [HideInInspector] public int ammoInGun;
    GunSounds sounds;

    public virtual void Start()
    {
        ammo = maxAmmo;
        ammoInGun = magazineSize;
        gunHolder = transform.parent;
        gunTip = transform.GetChild(0);
        anim = GetComponent<Animator>();
        unitStats = transform.root.GetComponent<Stats>();
        sounds = GetComponent<GunSounds>();
    }

    public void Reload()
    {
        int ammoMissing = magazineSize - ammoInGun;
        if (ammo >= ammoMissing)
        {
            ammo -= ammoMissing;
            ammoInGun = magazineSize;
        }
        else
        {
            ammoInGun += ammo;
            ammo = 0;
        }
    }
    public void RefillAmmo()
    {
        ammo = maxAmmo;
    }
    public virtual void Fire()
    {
        ammoInGun--;
        //sounds.PlayFireSound();
        anim.SetTrigger("Fire");
        Recoil();
    }
    protected float CheckForHits(RaycastHit2D[] hits, Vector2 bulletDir)
    {
        foreach(RaycastHit2D hit in hits)
        {
            if (hit.transform.CompareTag("Solid"))
            {
                return (hit.point - (Vector2)gunTip.position).magnitude;
            }
            else if (hit.transform.CompareTag("HalfCover") && UF.Chance(hit.transform.position.z))
            {
                return (hit.point - (Vector2)gunTip.position).magnitude;
            }
            else if(hit.transform.CompareTag("Unit"))
            {
                bool isUnitInPit = UF.InPit(hit.transform.position);
                if (!isUnitInPit || (isUnitInPit && UF.Chance(0.3f + 0.7f * unitStats.accuracy / 100f)))
                {
                    Health hitHealth = hit.transform.GetComponent<Health>();
                    unitStats.GiveExp(hitHealth.TakeDamage(damage));
                    hitHealth.OnHitEffects(hit.point, bulletDir, hit.collider.gameObject.transform.position);
                    return (hit.point - (Vector2)gunTip.position).magnitude;
                }
            }
        }
        return 50f;
    }
    void Recoil()
    {
        float angle = recoil * Random.Range(-60f, 60f) / unitStats.gunHandling;
        gunHolder.Rotate(Vector3.forward, angle);
    }
}
public enum GunType { semi, automatic, shotgun, pistol, boltAction };
