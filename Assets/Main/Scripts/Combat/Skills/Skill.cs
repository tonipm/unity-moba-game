using UnityEngine;
using UnityEngine.UI;

public abstract class Skill : MonoBehaviour {
    
    public Transform firePoint;
    public Sprite skillSprite;
    [HideInInspector]
    public Slider coolDownSlider;

    protected PhotonView playerPV;
    protected CombatSystem playerCS;
    protected int manaCost;
    protected float coolDownTime;
    protected float time;
    protected bool canExecute;

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

    public abstract bool Execute();

    // Si l'acció fa mal a un objectiu avisa a l'skill
    public abstract void Return(GameObject target);
}
