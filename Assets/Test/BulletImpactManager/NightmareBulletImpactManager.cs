using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum HitMaterial
{ 
    Metal,
    Brick,
    Wood
}
[System.Serializable]
public class HitMaterialInfo
{
    public HitMaterial hitMaterial;
    public ParticleSystem hitImpact;

    public List<ParticleSystem> particles = new List<ParticleSystem>();
}


public class NightmareBulletImpactManager : MonoBehaviour
{

    public List<HitMaterialInfo> hitImpacts = new List<HitMaterialInfo>();
    [SerializeField]
    private int initialNumberOfList = 3;

    private static NightmareBulletImpactManager instance;

    public static NightmareBulletImpactManager Instance
    {
        get { return instance; }
        set { instance = value; }
    }


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("Multiple Bullet Impact Manager.");
            Destroy(this);
            return;
        }

        Instance = this;

        CreateParticlePooling(hitImpacts);
    }

    private void CreateParticlePooling(List<HitMaterialInfo> hitImpacts)
    {
        for (int i = 0; i < hitImpacts.Count; i++)
        {
            GameObject hitImpactContainer = new GameObject();
            hitImpactContainer.transform.parent = this.gameObject.transform;
            hitImpactContainer.name = hitImpacts[i].hitMaterial.ToString() + " Particles Pooling";

            for(int x = 0; x < initialNumberOfList; x++)
            {
                ParticleSystem particle = Instantiate(hitImpacts[i].hitImpact);
                particle.gameObject.SetActive(false);
                particle.transform.parent = hitImpactContainer.transform;
                hitImpacts[i].particles.Add(particle);
            }
        }
    }

    private ParticleSystem GetParticle(HitMaterial hitMaterial)
    {
        //Particle Object Pooling
        for(int i = 0; i < hitImpacts.Count; i++)
        {
            if(hitImpacts[i].hitMaterial == hitMaterial)
            {
                int index = 0;

                return hitImpacts[i].particles[index];
            }
        }

        return null;
    }


    private void DoSpawnParticle(HitMaterial hitMaterial, Vector3 pos, Vector3 forward)
    {
        var particle = GetParticle(hitMaterial);
        particle.transform.position = pos;
        particle.transform.forward = forward;
        particle.gameObject.SetActive(true);
    }
    
    public void SpawnBulletImpact(Vector3 pos, Vector3 forward, HitMaterialInfo materialInfo)
    {
        if(!hasHitMaterialInfo(materialInfo))
        {
            DoSpawnParticle(materialInfo.hitMaterial, pos, forward);
        }
    }


    private bool hasHitMaterialInfo(HitMaterialInfo materialInfo)
    {
        for(int i = 0; i < hitImpacts.Count; i++)
        {
            if(hitImpacts[i].hitMaterial.Equals(materialInfo.hitMaterial))
            {
                return true;
            }
        }
        return false;
    }
}
