using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Weapon : MonoBehaviour
{
    public Camera playerCamera;
    private bool isShooting, readyToShoot;
    private bool allowReset = true;
    public float shootingDelay = 2f;
    public int bulletsPerBurst = 3;
    public int burstBulletsLeft;
    public float spreadIntensity;
    public GameObject muzzleEffect;
    public Vector3 spawnPosition;
    public Vector3 spawnRotation;
    public bool isActiveWeapon = true;
    public int weaponDamage;
    private bool _isReloading;


    public int maxNumberOfBullets = 7;
    private int _remainingBullets;
    

    public enum ShootingMode
    {
        Single,
        Burst,
        Auto
    }
    public enum WeaponModel
    {
        Pistol,M16
    }

    public WeaponModel weaponModel;

    public ShootingMode currentShootingMode;

    public void Awake()
    {
        readyToShoot = true;
        burstBulletsLeft = bulletsPerBurst;
    }

    private void Start()
    {
        _remainingBullets = maxNumberOfBullets;
    }

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletVelocity = 30f;
    public float bulletPrefabLifeTime = 3f;

    void Update()
    {
        if (isActiveWeapon)
        {

            if (currentShootingMode == ShootingMode.Auto)
            {
                isShooting = Input.GetKey(KeyCode.Mouse0);
            }
            else if (currentShootingMode == ShootingMode.Burst || currentShootingMode == ShootingMode.Single)
            {
                isShooting = Input.GetKeyDown(KeyCode.Mouse0);
            }

            if (readyToShoot && isShooting)
            {
                burstBulletsLeft = bulletsPerBurst;
                FireWeapon();
            }
        }
    }

    private void FireWeapon()
    {
        if (_isReloading) return;
        if (_remainingBullets == 0)
        {
            ReloadBullets();
            return;
        }
        _remainingBullets--;
        
        if (muzzleEffect)
            muzzleEffect.GetComponent<ParticleSystem>().Play();
        SoundManager.Instance.PlayShootSound(weaponModel);
        
        readyToShoot = false;
        var shootingDirection = CalculateDirectionAndSpread().normalized;
        var bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);

        var bul = bullet.GetComponent<BulletScript>();
        bul.bulletDamage = weaponDamage;
        bullet.transform.forward = shootingDirection;

        bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * bulletVelocity, ForceMode.Impulse);
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefabLifeTime));
        if (allowReset)
        {
            Invoke("ResetShot", shootingDelay);
            allowReset = false;
        }

        if (currentShootingMode == ShootingMode.Burst && burstBulletsLeft > 1)
        {
            burstBulletsLeft--;
            Invoke("FireWeapon", shootingDelay);
        }
    }

    private void ReloadBullets()
    {
        _isReloading = true;
        
        SoundManager.Instance.PlayReloadSound(weaponModel);
        _remainingBullets = maxNumberOfBullets;
        _isReloading = false;
    }

    public Vector3 CalculateDirectionAndSpread()
    {
        var ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, .5f, 0f));
        RaycastHit hit;
        Vector3 target;
        if (Physics.Raycast(ray, out hit))
        {
            target = hit.point;
        }
        else
        {
            target = ray.GetPoint(100);
        }

        var direction = target - bulletSpawn.position;
        var x = Random.Range(-spreadIntensity, spreadIntensity);
        var y = Random.Range(-spreadIntensity, spreadIntensity);
        return direction + new Vector3(x, y, 0);
    }

    public void ResetShot()
    {
        readyToShoot = true;
        allowReset = true;
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }
}