﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCannon : Weapon
{
    override public string Name
    {
        get
        {
            weaponName = "AutoCannon";
            return weaponName;    
        }
    }

    override protected IEnumerator AnimationCoroutine(GridBlock targetBlock)
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();

        foreach (GameObject target in targetBlock.objectsOnBlock)
        {
            Hazard hazardTest = target.GetComponent<Hazard>();
            if (hazardTest != null)
            {
                var trigger = ps.trigger;
                trigger.enabled = true;

                trigger.SetCollider(0, target.GetComponent<Collider>());
                trigger.radiusScale = 0.5f;

                ps.Play();
                yield return new WaitForSeconds(2.0f);
                ps.Stop();

                break;
            }

        }
    }
}
