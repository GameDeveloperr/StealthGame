using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffect : MonoBehaviour {

    public enum EffectType
    {
        Smoke
    }

    public EffectType m_eType;
    public System.Action<Collider> EnterEffect;
    public System.Action<Collider> ExitEffect;

    private void Start()
    {
        SelectEffect(m_eType);
        Destroy(this.gameObject, 5.0f);
    }

    public void SelectEffect(EffectType type)
    {
        switch(type)
        {
            case EffectType.Smoke:
                SetEnterEffect(SmokeEnterEffect);
                SetExitEffect(SmokeExitEffect);
                break;
            default:
                break;
        }
    }

    public void SetEnterEffect(System.Action<Collider> effect)
    {
        EnterEffect = effect;
    }
    public void SetExitEffect(System.Action<Collider> effect)
    {
        ExitEffect = effect;
    }
    private void SmokeEnterEffect(Collider other)
    {
        if(other.CompareTag("PLAYER"))
        {
            other.gameObject.layer = 0;
        }
    }
    private void SmokeExitEffect(Collider other)
    {
        if(other.CompareTag("PLAYER"))
        {
            other.gameObject.layer = 8;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        EnterEffect(other);
    }
    private void OnTriggerExit(Collider other)
    {
        ExitEffect(other);
    }
}
