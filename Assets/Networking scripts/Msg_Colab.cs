using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using static System.Net.WebRequestMethods;
public class Msg_Colab : MonoBehaviour
{
    [SerializeField]
    TypewriterEffect typewriterEffect;
    [SerializeField]
    TextMeshProUGUI textDisplay;
    string APIKey = "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";
    public class ColabResponse
    {
        public string response;
    }
    [SerializeField]
    string baseURL = "https://3a864cc20a17.ngrok-free.app";
    [SerializeField]
    ViewTranscript viewTranscript;

    public IEnumerator PostToColab(string json)
    {

        string url = "https://v19kesucjfuzi1ub.us-east-1.aws.endpoints.huggingface.cloud/v1/chat/completions";
        HFRequest req = new HFRequest
        {
            model = "ASTARFALCONER/cassandra-mistral-carnus-v1",
            messages = new HFMessage[] { new HFMessage { role = "user", content = json }
            }
             
};
        UnityWebRequest www = new UnityWebRequest(url, "POST");
        string bodyJson = JsonUtility.ToJson(req);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(bodyJson);
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("Authorization", $"Bearer {APIKey}");
        www.timeout = 50;
        Debug.Log("Payload: " + json);
        yield return www.SendWebRequest();
       

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + www.error);
            
            ToastManager.Instance.showToast("Network Error: " + www.error);
           
        }
        else
        {
            string responseText = www.downloadHandler.text;
            Debug.Log("Raw HF response: " + responseText);

            HFResponse response = JsonUtility.FromJson<HFResponse>(responseText);

            if (response == null || response.choices == null || response.choices.Length == 0 || response.choices[0].message == null)
            {
                Debug.LogError("HF response parsing failed or no choices/message found.");
                
                ToastManager.Instance.showToast("AI Error: Empty or invalid response.");
                
                yield break;
            }

            string assistantContent = response.choices[0].message.content;
            var cleaned = CleanUI.CleanString(assistantContent);

            typewriterEffect.DisplayText(cleaned);
            History.Instance.AppendTranscriptC4554NDR4(cleaned);
            
            ViewTranscript.Instance.RefreshTranscriptList();

            Debug.Log("Cleaned Response: " + cleaned);
            Debug.Log("Parsed Assistant Content: " + assistantContent);
        }
    }
    [System.Serializable]
    public class HFGenerationParams
    {
        public int max_tokens = 120;
        public float temperature = 0;
        public float top_p = 1;
        public float frequency_penalty = 1.2f;
    }
    [System.Serializable]
    public class HFMessage
    {
        public string role;
        public string content;

    }
    [System.Serializable]
    public class HFRequest
    {
        public string model;
        public HFMessage[] messages;
        public int max_tokens = 120;
        public float temperature = 0.1f;
        public float top_p = 1;
        public float frequency_penalty = 1.2f;
    }
    
    [System.Serializable]
    public class HFChoices
    {
        public HFMessage message;
        
    }
    
    [System.Serializable]
    public class HFResponse
    {
        public HFChoices[] choices;
    }
    
    IEnumerator Upload(string json)
    {
        string url = baseURL + "/inference";
        var req = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        req.uploadHandler = new UploadHandlerRaw(bodyRaw);
        req.downloadHandler = new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");
        req.timeout = 10;
        //Debug.Log("Sending request to: " + url);
        Debug.Log("Payload: " + json);
        yield return req.SendWebRequest();
        //Debug.Log("Response Code: " + req.responseCode);
       // Debug.Log("Body: " + req.downloadHandler.text);

        if (req.result == UnityWebRequest.Result.ConnectionError || req.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + req.error);
        }
        else
        {
            ColabResponse response = JsonUtility.FromJson<ColabResponse>(req.downloadHandler.text);
            Debug.Log("Parsed Response: " + response.response);
        }
    }

}