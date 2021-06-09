using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireWeapon : FireWeapon
{
    public override void StartShoot()
    {
        _isShooting = true;
    }
    public override void StopShoot()
    {
        _isShooting = false;
    }
}
