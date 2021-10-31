using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class AI : MonoBehaviour
{
    protected Animator anim;
    [HideInInspector] public Gun gun;
    public Transform gunHolder;
    [HideInInspector] public Health hp;
    [HideInInspector] public Stats stats;
    [HideInInspector] public List<GameObject> enemies = new List<GameObject>();
    [HideInInspector] public UnitStates state;
    GameObject targetEnemy;
    ShootInfo shootInfo;
    Movement mov;
    NavMeshAgent agent;

    public virtual void Awake()
    {
        state = UnitStates.idle;
        hp = GetComponent<Health>();
        stats = GetComponent<Stats>();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        mov = GetComponent<Movement>();
        gun = gunHolder.GetChild(0).GetComponent<Gun>();
        shootInfo = new ShootInfo(0, 0);
    }
    public virtual void Update()
    {
        switch (state)
        {
            case UnitStates.moving:
                Move();
                break;
            case UnitStates.aiming:
                Aim();
                break;
            case UnitStates.shooting:
                Shooting();
                break;
        }
    }
    #region Moving
    public void SetMovePoint(Vector2 point)
    {
        if (GM.ins.IsInsideMap(point))
        {
            mov.Move(point);
            SetState(UnitStates.moving);
        }
    }
    void Move()
    {
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    StopMovement();
                    return;
                }
            }
        }
        mov.SetRotation();
    }
    public void StopMovement()
    {
        mov.Stop();
        SetState(UnitStates.idle);
    }
    #endregion
    #region Aiming
    public void StartAiming(GameObject enemy)
    {
        targetEnemy = enemy;
        SetState(UnitStates.aiming);
        LookAt(targetEnemy.transform.position);
        gunHolder.localRotation = Quaternion.Euler(0, 0, 0);
    }
    void Aim()
    {
        if (targetEnemy == null || !IsVisible(targetEnemy, true))
        {
            StopAiming();
            return;
        }
        LookAt(targetEnemy.transform.position);
        if (UF.Chance(2 * 10f / (stats.gunHandling) * Time.deltaTime))
        {
            AimGun();
        }
        if (IWantToPullTrigger())
        {
            if (gun.ammoInGun > 0)
            {
                if (gun.type == GunType.automatic)
                {
                    int bulletsToShoot = Random.Range(1, 2 + (int)(stats.morale / 5));
                    bulletsToShoot = Mathf.Min(bulletsToShoot, gun.ammoInGun);
                    shootInfo.bulletsToShoot = bulletsToShoot;
                    PullTrigger();
                }
                else
                {
                    PullTrigger();
                }
            }
            else
            {
                StopAiming();
                Reload();
            }
            return;
        }
        if (UF.Chance(10f / (stats.morale + 100f) * Time.deltaTime))
        {
            StopAiming();
        }
    }
    void StopAiming()
    {
        SetState(UnitStates.idle);
        gunHolder.localRotation = Quaternion.Euler(0, 0, 0);
    }
    void AimGun(float angleFix = -90f)
    {
        float angle = UF.AngleToObject(targetEnemy.transform.position, gunHolder.transform.position);
        angle += Random.Range(-20f, 20f) * 20 / (1.5f * (stats.accuracy + stats.gunHandling));
        gunHolder.rotation = Quaternion.Euler(0, 0, angle + angleFix);
    }
    #endregion
    #region Shooting
    bool IWantToPullTrigger()
    {
        return UF.Chance((stats.morale + stats.accuracy) / 200f * Time.deltaTime);
    }
    public bool DoIWantToShoot()
    {
        return UF.Chance(0.1f * Time.deltaTime * stats.morale);
    }
    void PullTrigger()
    {
        SetState(UnitStates.fire);
        gun.Fire();
        shootInfo.shotTime = Time.time;
        shootInfo.bulletsToShoot--;
    }
    void Shooting()
    {
        if (shootInfo.bulletsToShoot > 0)
        {
            if (Time.time - shootInfo.shotTime >= 1f / ((Automatic)gun).fireRate)
            {
                if (gun.ammoInGun <= 0)
                {
                    StopShooting();
                    Reload();
                }
                else
                {
                    PullTrigger();
                }
            }
        }
        else
        {
            StopShooting();
        }
    }
    void StopShooting()
    {
        shootInfo.bulletsToShoot = 0;
        if (targetEnemy == null)
        {
            StopAiming();
        }
        else
        {
            SetState(UnitStates.aiming);
        }
    }
    public void ReturnFromFiringAnimation()
    {
        if (shootInfo.bulletsToShoot > 0)
        {
            SetState(UnitStates.shooting);
        }
        else
        {
            SetState(UnitStates.aiming);
        }
    }
    #endregion
    public bool IsVisible(GameObject enemy, bool checkIfBehindCover = false)
    {
        Vector2 dir = enemy.transform.position - transform.position;
        RaycastHit2D[] hits;
        if (checkIfBehindCover)
        {
            hits = Physics2D.RaycastAll(transform.position, dir, dir.magnitude, GM.ins.solidMask);
        }
        else
        {
            hits = Physics2D.RaycastAll(transform.position, dir, dir.magnitude, GM.ins.solidMask | LayerMask.GetMask("HardToSee"));
        }
        hits = hits.OrderBy(h => h.distance).ToArray();
        foreach (RaycastHit2D hit in hits)
        {
            if(hit.transform.gameObject == enemy)
            {
                return true;
            }
            else if (hit.transform.CompareTag("Solid"))
            {
                return false;
            }
            else if (hit.transform.CompareTag("HardToSee") && UF.Chance(0.5f))
            {
                return false;
            }
        }
        return false;
    }
    public List<GameObject> VisibleEnemies()
    {
        List<GameObject> visible = new List<GameObject>();
        foreach (GameObject enemy in enemies)
        {
            if (IsVisible(enemy))
            {
                visible.Add(enemy);
            }
        }
        return visible;
    }
    void Reload()
    {
        gun.Reload();
        anim.SetTrigger("Reload");
        SetState(UnitStates.reloading);
    }
    public void LookAt(Vector2 target, float angleFix = -90f)
    {
        float angle = UF.AngleToObject(target, transform.position);
        transform.rotation = Quaternion.Euler(0, 0, angle + angleFix);
    }
    public void ChangeGun(GameObject newGun)
    {
        GameObject gunInstance = Instantiate(newGun, gun.gameObject.transform.position, gun.gameObject.transform.rotation);
        gunInstance.transform.parent = gunHolder;
        Destroy(gun.gameObject);
        gun = gunInstance.GetComponent<Gun>();
        anim.runtimeAnimatorController = GM.ins.GunAnimation(gun.type);
        shootInfo.bulletsToShoot = 0;
    }
    public bool InPit()
    {
        Ray ray = new Ray(transform.position + new Vector3(0, 0, 10), Vector3.back);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, LayerMask.GetMask("Pit"));
        return hit.collider != null;
    }
    void SetState(UnitStates newState)
    {
        state = newState;
        anim.SetInteger("State", (int)newState);
    }
    public void ToIdle()
    {
        SetState(UnitStates.idle);
    }
    protected bool InForbbiddenState()
    {
        return state == UnitStates.fire || state == UnitStates.reloading;
    }
    public void EjectCasing()
    {
        ((SingleShot)gun).EjectCasing();
    }
}
public enum UnitStates { idle, moving, aiming, shooting, fire, reloading };
