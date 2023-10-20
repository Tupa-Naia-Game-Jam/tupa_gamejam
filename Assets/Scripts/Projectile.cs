using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    public float speed = 50;
    public float destroyDelay = 3.5f;
    public List<ParticleSystem> trailsVFX;
	public List<GameObject> detachables;

    private Rigidbody rb;
    private ParticleSystem.EmitParams emitParam;
    private bool stop;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine (Stop());
    }

    void FixedUpdate()
    {
        if (speed != 0 && rb != null)
			rb.position += (transform.forward) * (speed * Time.deltaTime); 

        RaycastHit hit;
        if(Physics.Raycast(transform.position, -Vector3.up, out hit))
        { 
            if (trailsVFX.Count>0 && !stop) 
            {
                for(int i = 0; i< trailsVFX.Count; i++)
                {
                    ParticleSystem.EmissionModule emissionModule = trailsVFX[i].emission;
                    emitParam.position = hit.point + Vector3.up*0.1f;
                    emitParam.rotation3D = Quaternion.LookRotation (hit.normal).eulerAngles;

                    emissionModule.enabled = true;
                    trailsVFX[i].Emit (emitParam, 1);
                }
            }
        } 
    }

    IEnumerator Stop ()
    {
        yield return new WaitForSeconds (destroyDelay-0.5f);
        stop = true;

        if (detachables.Count > 0)
        {
            for (int i = 0; i < detachables.Count; i++)
            {
                detachables[i].transform.parent = null;
                var ps = detachables[i].GetComponent<ParticleSystem>();
                if (ps != null)
                {
                    ps.Stop();
                    Destroy(ps.gameObject, ps.main.duration + ps.main.startLifetime.constantMax);
                }
            }
        }

        yield return new WaitForSeconds (0.5f);
        Destroy(gameObject);
    }
}
