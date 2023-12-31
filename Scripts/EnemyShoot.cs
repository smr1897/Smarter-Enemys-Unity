using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public Transform shootPoint;

    public Transform gunPoint;

    public LayerMask layerMask;

    [Header("Gun")]
    public Vector3 spread = new Vector3(0.06f, 0.06f, 0.06f);
    public TrailRenderer bulletTrail;
    public int ammo = 30;

    private EnemyReferences enemyReference;

    private int currentAmmo;

    private void Awake()
    {
        enemyReference = GetComponent<EnemyReferences>();
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void Shoot()
    {
        Vector3 direction = GetDirection();
        if(Physics.Raycast(shootPoint.position , direction , out RaycastHit hit , float.MaxValue , layerMask))
        {
            Debug.DrawLine(shootPoint.position, shootPoint.position + direction * 10f, Color.red, 1f);

            TrailRenderer trail = Instantiate(bulletTrail , gunPoint.position , Quaternion.identity);
            StartCoroutine(SpawnTrail(trail,hit));

            currentAmmo -= 1;
        }
    }

    public bool ShouldReload()
    {
        return currentAmmo <= 0;
    }

    public void Reload()
    {
        Debug.Log("Reloaded");
        currentAmmo = ammo;
    }

    private Vector3 GetDirection()
    {
        Vector3 direction = transform.forward;
        direction += new Vector3
            (
                Random.Range(-spread.x , spread.x),
                Random.Range(-spread.y , spread.y),
                Random.Range(-spread.z , spread.z)
            );
        direction.Normalize();
        return direction;
    }

    private IEnumerator SpawnTrail(TrailRenderer trail , RaycastHit hit)
    {
        float time = 0f;
        Vector3 startPosition = trail.transform.position;

        while(time < 1f) 
        {
            trail.transform.position = Vector3.Lerp(startPosition , hit.point , time);
            time += Time.deltaTime / trail.time;

            yield return null;
        }

        trail.transform.position = hit.point;

        Destroy(trail.gameObject, trail.time);
    }
}
