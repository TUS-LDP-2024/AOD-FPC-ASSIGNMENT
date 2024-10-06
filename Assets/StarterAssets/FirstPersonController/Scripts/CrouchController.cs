using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CrouchController : MonoBehaviour
{
    public float crouchHeight = 0.5f;
    public float crouchSpeed = 10f;

    private StarterAssetsInputs _input;
    private GameObject _PlayerCapsule;

    private bool objectOverhead = false;

    public void Awake()
    {
        _PlayerCapsule = GameObject.FindGameObjectWithTag("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        Crouch();
    }

    private void CheckForOverheadObjects()
    {
        if(_input.crouch || objectOverhead)
        {
            var position = _PlayerCapsule.transform.position + new Vector3(0, 1.5f, 0);
            var layerId = 3;
            var layerMask = 1 << layerId;

            var hitColliders = Physics.OverlapSphere(position, 0.5f, layerMask);

            objectOverhead = hitColliders.Length > 0;
        }
    }

    private void Crouch()
    {
        float targetHeight = _input.crouch || objectOverhead ? crouchHeight : 1.375f;

        //float newPosition = Mathf.Lerp(_PlayerCameraTarget.transform.localPosition.y, targetHeight, Time.deltaTime * crouchSpeed);
        _PlayerCapsule.transform.localScale = new Vector3(1, targetHeight, 1);
    }
}
