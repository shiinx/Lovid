using UnityEngine;

public class MuzzleController : MonoBehaviour {
    private Animator _muzzleAnimator;
    void Start() {
        _muzzleAnimator = GetComponent<Animator>();
    }

    public void OnShootResponse() {
        _muzzleAnimator.SetTrigger("onShoot");
    }
}
