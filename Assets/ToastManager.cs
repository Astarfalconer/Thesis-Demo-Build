using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class ToastManager : MonoBehaviour
{
    [SerializeField]
    GameObject toastPrefab;
    [SerializeField]
    RectTransform toastAnchor;
    [SerializeField]
    float fadeTime = 1.0f;
    [SerializeField]
    float displayTime = 2f;
   private Queue<string> toastQueue = new Queue<string>();
    private bool isShowing = false;

    public static ToastManager Instance;

    #region singleton
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(Instance);
        }
        Instance = this;
    }
    #endregion

   

    public void showToast(string message)
    {
        toastQueue.Enqueue(message);
        if (!isShowing) { 
        StartCoroutine(DisplayNextToast());
        }
    }

    IEnumerator DisplayNextToast()
    {
        if (toastQueue.Count == 0)
            yield break;

        isShowing = true;
        string message = toastQueue.Dequeue();
        GameObject toastInstance = Instantiate(toastPrefab, toastAnchor);

        // Use GetComponentInChildren for robustness
        TextMeshProUGUI toastText = toastInstance.GetComponentInChildren<TextMeshProUGUI>();
        if (toastText != null)
            toastText.text = message;
        else
            Debug.LogWarning("Toast prefab missing TextMeshProUGUI component.");

        CanvasGroup cg = toastInstance.GetComponent<CanvasGroup>();
        if (cg == null)
        {
            cg = toastInstance.AddComponent<CanvasGroup>();
        }
        StartCoroutine(FadeInToast(cg, toastInstance));

    }

    IEnumerator FadeInToast(CanvasGroup cg, GameObject toastInstance)
    {
        float t = 0f;
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            cg.alpha = Mathf.Lerp(0, 1, t / fadeTime);
            yield return null;
        }
        cg.alpha = 1;
        yield return new WaitForSeconds(displayTime);
        t= 0f;
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            cg.alpha = Mathf.Lerp(1, 0, t / fadeTime);
            yield return null;
        }
        cg.alpha = 0;
        Destroy(toastInstance);
        isShowing = false;
        if (toastQueue.Count > 0)
        {
            StartCoroutine(DisplayNextToast());
        }
        
    }
}
