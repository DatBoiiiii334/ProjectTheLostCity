using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;
using UnityEngine.Animations.Rigging;

public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimSensitivity;
    [SerializeField] private LayerMask aimColliderMask = new LayerMask();
    [SerializeField] private  Transform debugTransform;
    [SerializeField] private RigBuilder myRigBuilder;

    private ThirdPersonController _thirdPersonController;
    private StarterAssetsInputs _starterAssetsInputs;
    private Animator _animator;
    public SimpleShoot _simpleShoot;
    

    public GameObject UItext;
    private bool isOn;

    [Header("Stealth")]
    public bool _crouch = false;
    public bool _nearCover = false;

    [Header("Gun Related")]
    public int Damage;

    [Header("Aiming")]
    [SerializeField] private GameObject RigFirstHand;
    [SerializeField] private GameObject RigSecondHand;

    private void Awake(){
        _thirdPersonController = GetComponent<ThirdPersonController>();
        _starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        myRigBuilder = GetComponent<RigBuilder>();
        _animator = GetComponent<Animator>();

        RigFirstHand.SetActive(false);
        RigSecondHand.SetActive(false);
    }

    private void Update() {
        DevCommands();
        AimAndShoot();
    
        Crough();
        
    }

    void DevCommands(){
        //For pausing the game vieuw
        if(Input.GetKey(KeyCode.P)){
          Debug.Break();
        }
    }

    void AimAndShoot(){
        //
        // For finding center of screen
        //
        Vector3 mouseWorldPosition = Vector3.zero;
        
        Vector2 screenCenterPoint = new Vector2(Screen.width /2f, Screen.height /2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        Transform hitTransform = null;
        if(Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderMask)){
            debugTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
            hitTransform = raycastHit.transform;
        }

        //
        // For Aiming
        //
        if(_starterAssetsInputs.aim){
            print("AIM");
            //myRigBuilder.m_RigLayers[0].active = true;
            RigFirstHand.SetActive(true);
            RigSecondHand.SetActive(true);
            
            aimVirtualCamera.gameObject.SetActive(true);
            _thirdPersonController.SetSensitivity(aimSensitivity);
            _thirdPersonController.SetRotateMove(false);
            _animator.SetLayerWeight(1, Mathf.Lerp(_animator.GetLayerWeight(1),1f, Time.deltaTime * 10f));
            
            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        }else{
            //myRigBuilder.m_RigLayers[0].active = false;
            RigFirstHand.SetActive(false);
            RigSecondHand.SetActive(false);
            aimVirtualCamera.gameObject.SetActive(false);
            _thirdPersonController.SetSensitivity(normalSensitivity);
            _thirdPersonController.SetRotateMove(true);
            _animator.SetLayerWeight(1, Mathf.Lerp(_animator.GetLayerWeight(1),0f, Time.deltaTime * 10f));
        }
        
        //
        // For Shooting
        //
        if(_starterAssetsInputs.shoot){
            print("SHOOT");
            RayTracingShot();
            _simpleShoot.gunAnimator.SetTrigger("Fire");
            if(hitTransform != null){
                
                if(hitTransform.GetComponent<Enemy>()){
                    print("hit");
                    hitTransform.GetComponent<Enemy>().TakeDamage(Damage);
                }else{
                    print("miss");
                }
            }
            _starterAssetsInputs.shoot = false;
        }
    }

    void Crough(){
        if(_starterAssetsInputs.crouch){
            _starterAssetsInputs.crouch = !_starterAssetsInputs.crouch;

            
                //Not near cover, so can just move around
                if(_crouch == false){
                    _animator.SetBool("isCrouching", true);
                    _crouch = true;
                    print("CROUCH");

                    if(_nearCover){
                    //move player transform to center of trigger box Collider
                    // disable axis of player
                    print("Eneble position freeze");
                    transform.position = new Vector3(transform.position.x,transform.position.y, 5.2f);

                    // movent is limited to inside of the trigger box collider 
                    // Animation is based on which side you are moving in box collider

                    // trigger animation to cover
                    }

            }else{
                _animator.SetBool("isCrouching", false);
                _crouch = false;
                _nearCover = false;
            } 
            

             
        } 
    }

    void Cover(){
        //trigger
    }

    void RayTracingShot(){
    }
}
