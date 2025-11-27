using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PartyManager : MonoBehaviour
{
    #region singleton
    public static PartyManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public List<GameObject> partyMembers = new List<GameObject>();

    GameObject currentControlledMember;

    void Start()
    {
        if (partyMembers.Count > 0)
        {
            currentControlledMember = partyMembers[0];
        }
    }

    public IEnumerator KillPlayer(float delay) { 
        yield return new WaitForSeconds(delay);
        Debug.Log("Zero has died. Game Over.");
        GameEventsManager.instance.FadeInGameOver();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // Implement game over logic here (e.g., show game over screen, restart level)
    }

}
