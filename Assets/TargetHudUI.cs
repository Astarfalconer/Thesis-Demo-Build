using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetHudUI : MonoBehaviour
{
    [SerializeField]
    Image targetPortrait;
    [SerializeField]
    Image healthBar;
    [SerializeField]
    GameObject targetHudPanel;
    // Start is called before the first frame update
    EnemyStats currentTarget;

    public static TargetHudUI instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

     void Update()
    {
      if (currentTarget == null || currentTarget.isDead)
      {
          targetHudPanel.SetActive(false);
          return;
      }
      else
      {
          targetHudPanel.SetActive(true);
          float fillAmount = (float)currentTarget.currentHealth / currentTarget.maxHealth;
            healthBar.fillAmount = Mathf.Clamp01(fillAmount);
        }
    }

    public void SetTarget(EnemyStats target)
    {
        if(target == null || !target.isHostile) { 
            currentTarget = null;
            targetHudPanel.SetActive(false);
            return;
        }
        currentTarget = target;
        targetPortrait.sprite = target.portrait;
        targetHudPanel.SetActive(true);
    }
}
