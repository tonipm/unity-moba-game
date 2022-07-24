using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBase : Skill
{

    private string prefabActionName;
    private CombatSystem cs;
    private PlayerController playerController;
    public float timePassed = 0;

    // Use this for initialization
    void Start()
    {
        this.prefabActionName = "SkillsActions/SkillBase";
        this.manaCost = 0;
        this.coolDownTime = 10;
        this.time = 5;
    }

    public override bool Execute(int damageChamp)
    {
        if (this.canExecute)
        {
            cs = gameObject.GetComponent<CombatSystem>();
            playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            playerController.SetCanMove(false);

            GameObject go = PhotonNetwork.Instantiate(this.prefabActionName, this.firePoint.position, this.firePoint.rotation, 0);
            go.GetComponent<ActionBase>().SetSkill(this);

            StartCoroutine(GoToBase());

            this.time = 0;
            this.canExecute = false;
            return true;
        }
        else
        {
            return false;
        }
    }

    public override void Return(GameObject target)
    {

    }

    IEnumerator GoToBase()
    {
        timePassed = 0;
        while (timePassed < 4 || timePassed == -1)
        {
            timePassed += Time.deltaTime;
            if (timePassed >= 4)
            {
                if (cs != null)
                {
                    this.gameObject.transform.position = cs.GetInitialPos();
                    playerController.SetCanMove(true);
                }
            }

            yield return null;
        }
    }
}