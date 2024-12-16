using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperScope : MonoBehaviour
{
    public Animator animator;
    public GameObject scopeOverlay;
    public GameObject weaponCamera;
    public Camera mainCamera;

    public float scopedFOV;
    private float normalFOV;



    private bool isScoped = false;

    private void Update()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            isScoped = !isScoped;
            animator.SetBool("Scoped", isScoped);

            if (isScoped)
                StartCoroutine(OnScoped());
            else
                OnUnscoped();
        }
    }

    void OnUnscoped()
    {
        scopeOverlay.SetActive(false);
        weaponCamera.SetActive(true);
        mainCamera.fieldOfView = normalFOV;
    }

    IEnumerator OnScoped()
    {
        yield return new WaitForSeconds(.20f);

        scopeOverlay.SetActive(true);
        weaponCamera.SetActive(false);

        normalFOV = mainCamera.fieldOfView;
        mainCamera.fieldOfView = scopedFOV;
    }
}

