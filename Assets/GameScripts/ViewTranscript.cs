using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ViewTranscript : MonoBehaviour
{
    [SerializeField]
    TMP_Text transcriptText;
    [SerializeField]
    private Scrollbar scrollbar;

    public static ViewTranscript Instance { get; set; }
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }
    // Start is called before the first frame update
    public void RefreshTranscriptList()
    {
        if (History.Instance.Transcript.Count > 5)
        {
            History.Instance.Transcript.RemoveAt(0);
        }
        transcriptText.text = History.Instance.Transcript.Count > 0 ? string.Join("\n\n", History.Instance.GetTranscript()) : "No transcript available.";
        Canvas.ForceUpdateCanvases();
    }
}