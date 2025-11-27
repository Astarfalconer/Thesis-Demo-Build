using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class History : MonoBehaviour
{
    public static History Instance { get; private set; }
    // Start is called before the first frame update
    public List<string> Transcript = new List<string>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AppendTranscriptZero(string message)
    {
        Transcript.Add("Zero> " + message);
    }

    public void AppendTranscriptC4554NDR4(string message)
    {
        Transcript.Add("C4554NDR4> " + message);
    }

    public List<string> GetTranscript()
    {
        return Transcript;

    }

    public void ClearTranscript()
    {
        Transcript.Clear();
    }
}
