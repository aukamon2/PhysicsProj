using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunSystem : MonoBehaviour
{
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShot;
    public int magazineSize, bulletPerTabs;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;

    bool shooting, readytoShoot, reloading;

    //ref
    public Camera  fpsCamera;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask isEnemy,isWall,Grounded;
    public GameObject muzzleFlash, bulletHoleGraphic,bulletHoleGraphicWall;
    public TextMeshProUGUI ammoDisplay;
    
    
    void Start()
    {
        bulletsLeft = magazineSize;
        readytoShoot = true;
        
    }

    // Update is called once per frame
    private void Update()
    {
        MyInput();

        
       ammoDisplay.SetText(bulletsLeft/bulletPerTabs+"/"+magazineSize/bulletPerTabs);
    }
    private void MyInput()
    {
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();
        
        if (readytoShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletPerTabs;
            Shoot();
        }
        
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }
    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
    public void Shoot()
    {
        readytoShoot = false;
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        Vector3 direction = fpsCamera.transform.forward + new Vector3(x, y, 0);
        //RayCast
        if (Physics.Raycast(fpsCamera.transform.position, direction ,out rayHit, range, isEnemy))
        {
            Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.LookRotation(rayHit.normal));
            
           
            if (rayHit.collider.CompareTag("Enemies"))
            {
                Debug.Log("Hit");
                AiBehavior enemy = rayHit.collider.GetComponent<AiBehavior>();
                if(enemy != null)
                {
                    enemy.TakeDamage(damage);
                }

            }
        }
        if (Physics.Raycast(fpsCamera.transform.position, direction, out rayHit, range, isWall) || Physics.Raycast(fpsCamera.transform.position, direction, out rayHit, range, Grounded))
        {
            Instantiate(bulletHoleGraphicWall, rayHit.point, Quaternion.LookRotation(rayHit.normal));
        }

            //graphic
            Instantiate(muzzleFlash, attackPoint.transform.position, Quaternion.identity);

        bulletsLeft--;
        bulletsShot--;
        Invoke("ResetShoot",timeBetweenShooting);
        
        if(bulletsShot > 0 && bulletsLeft > 0)
        Invoke("Shoot", timeBetweenShot);
;    }
    private void ResetShoot()
    {
        readytoShoot = true;
    }
    
}
