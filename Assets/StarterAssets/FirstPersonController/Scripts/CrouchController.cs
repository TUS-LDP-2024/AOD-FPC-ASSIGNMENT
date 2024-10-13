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

    public bool objectOverhead = false;

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
        CheckForOverheadObjects();
        Crouch();
    }

    private void CheckForOverheadObjects()
    {
        if(_input.crouch || objectOverhead)
        {
            var position = _PlayerCapsule.transform.position + new Vector3(0, 1.5f, 0);
            var layerMask = LayerMask.GetMask("Ground");

            var hitColliders = Physics.OverlapSphere(position, 0.5f, layerMask);

            objectOverhead = hitColliders.Length > 0;
        }
    }

    private void Crouch()
    {
        float targetHeight = _input.crouch || objectOverhead ? crouchHeight : 1f;

        _PlayerCapsule.transform.localScale = new Vector3(1, targetHeight, 1);
    }
}
