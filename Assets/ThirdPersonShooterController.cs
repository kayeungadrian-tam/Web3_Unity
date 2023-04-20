using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.InputSystem;
using StarterAssets;




public class ThirdPersonShooterController : MonoBehaviour
{

    [SerializeField] private CinemachineVirtualCamera aimVCam;
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimSensitivity;
    [SerializeField] private LayerMask aimColliderLayerMask;

    [SerializeField] private Transform debugTransform;
    [SerializeField] private Transform bulletPrefab;
    [SerializeField] private Transform spawnBullet;

    [SerializeField] private AudioSource fireSound;
    [SerializeField] private GameObject crosshair;


    // Start is called before the first frame update

    // private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs _input;
    // private Animator animator;

    private bool canShoot = false;

    void Start()
    {
        // thirdPersonController = GetComponent<ThirdPersonController>();
        _input = GetComponent<StarterAssetsInputs>();
        // animator = GetComponent<Animator>();
    }

    void Update()
    {
        Vector3 mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);

        if (_input.shoot && !_input.aim) { _input.shoot = false; }

        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            debugTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
        }

        if (_input.aim)
        {
            crosshair.SetActive(true);
            canShoot = true;
            // thirdPersonController.SetSensitivity(aimSensitivity);
            aimVCam.gameObject.SetActive(true);

            // animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDir = (worldAimTarget - transform.position).normalized;
            transform.forward = Vector3.Lerp(transform.forward, aimDir, Time.deltaTime * 20f);
        }
        else
        {
            // animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
            // thirdPersonController.SetSensitivity(normalSensitivity);
            aimVCam.gameObject.SetActive(false);
            crosshair.SetActive(false);

        }

        if (_input.shoot && _input.aim && canShoot)
        {
            fireSound.Play();
            Vector3 aimDir = (mouseWorldPosition - spawnBullet.position).normalized;
            Instantiate(bulletPrefab, spawnBullet.position, Quaternion.LookRotation(aimDir, Vector3.up));
            _input.shoot = false;
        }
    }





}

