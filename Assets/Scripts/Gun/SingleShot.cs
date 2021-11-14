using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SingleShot : Gun
{
    public GameObject gunLine;
    public GameObject gunCasing;
    public Transform ejectPosition;
    public bool ejectRight;
    protected void InstantiateGunline(float dist)
    {
        GameObject gunLineInst = Instantiate(gunLine, gunTip.position, gunTip.rotation);
        Vector3 scale = gunLineInst.transform.localScale;
        scale.y = dist;
        gunLineInst.transform.localScale = scale;
        Destroy(gunLineInst, 0.05f);
    }
    public override void Fire()
    {
        float gunAngle = transform.rotation.eulerAngles.z + 90;
        gunAngle += Random.Range(-accuracy, accuracy);
        Vector2 dir = new Vector2(Mathf.Cos(gunAngle * Mathf.Deg2Rad), Mathf.Sin(gunAngle * Mathf.Deg2Rad)).normalized;
        RaycastHit2D[] hits = Physics2D.RaycastAll(gunTip.transform.position, dir, Mathf.Infinity, GM.ins.shootableMask);
        hits = hits.OrderBy(h => h.distance).ToArray();

        ShotInfo shotInfo = new ShotInfo(damage, dir, transform.root);
        float dist = CheckForHits(hits, shotInfo);
        InstantiateGunline(dist);
        base.Fire();
    }
    public void EjectCasing()
    {
        GameObject gunCasingInstance = Instantiate(gunCasing, ejectPosition.position, transform.rotation);
        Rigidbody2D rb = gunCasingInstance.GetComponent<Rigidbody2D>();
        float ejectSpeed = Random.Range(0.2f, 1.8f);
        if (ejectRight)
        {
            rb.velocity = ejectSpeed * transform.right;
        }
        else
        {
            rb.velocity = -ejectSpeed * transform.right;
        }
        rb.angularVelocity = Random.Range(-360, 360);
    }
}
