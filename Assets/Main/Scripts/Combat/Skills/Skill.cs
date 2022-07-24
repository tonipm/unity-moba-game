using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class Skill : MonoBehaviour {
    
    public Transform firePoint;
    public Sprite skillSprite;
    [HideInInspector]
    public Slider coolDownSlider;
    [HideInInspector]
    public bool execSkill;

    protected PhotonView playerPV;
    protected CombatSystem playerCS;
    protected int manaCost;
    protected float coolDownTime;
    protected float time;
    protected bool canExecute;
    protected bool isMele;
    protected Animation animations;
    protected PlayerController playerController;


    public void SetCombatSystem(CombatSystem _playerCS)
    {
        this.playerCS = _playerCS;
        if (this.coolDownSlider != null)
        {
            this.coolDownSlider.value = 0;
        }
    }

    public CombatSystem GetCombatSystem()
    {
        return this.playerCS;
    }

    public void SetPhotonView(PhotonView _pv)
    {
        this.playerPV = _pv;
    }

    public bool GetIsMele()
    {
        return this.isMele;
    }

    public int GetManaCost()
    {
        return manaCost;
    }

    private void Update()
    {
        if (!this.canExecute)
        {
            this.time += Time.deltaTime;

            if (this.coolDownSlider != null)
            {
                this.coolDownSlider.value = this.coolDownSlider.maxValue - ((this.coolDownSlider.maxValue * this.time) / this.coolDownTime);

            }
            if (this.time >= this.coolDownTime)
            {
                this.canExecute = true;
            }
        }
    }

    public IEnumerator PararAnimacio(float time)
    {
        yield return new WaitForSeconds(time);
        execSkill = false;
        animations.Stop();
        if (playerController != null)
        {
            playerController.execAnimationIdle = true;
        }
    }

    public abstract bool Execute(int damageChamp);

    // Si l'acció fa mal a un objectiu avisa a l'skill
    public abstract void Return(GameObject target);
}
