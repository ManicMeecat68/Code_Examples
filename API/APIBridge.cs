using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class APIBridge : PersistentLazySingleton<APIBridge>
{  
    public void Login(UnityAction<FacilitatorRequest> callbackOnSuccess, UnityAction<string> callbackOnFail, string data)
    {
        Debug.Log(Constants.login);
        SendPostRequest(Constants.login, data, callbackOnSuccess, callbackOnFail);
    }

    public void RequestGame(UnityAction<AvailibleGames.Game> callbackOnSuccess, UnityAction<string> callbackOnFail, string data)
    {
        Debug.Log(Constants.getCredit);
        SendPostRequest(Constants.getCredit, data, callbackOnSuccess, callbackOnFail);
    }

    public void UsedGame(UnityAction<UsedGames.Game> callbackOnSuccess, UnityAction<string> callbackOnFail, string data)
    {
        Debug.Log(Constants.sendCredit);
        SendPostRequest(Constants.sendCredit, data, callbackOnSuccess, callbackOnFail);
    }

    public void BanUser(UnityAction<FacilitatorRequest> callbackOnSuccess, UnityAction<string> callbackOnFail, string data)
    {
        Debug.Log(Constants.banUser);
        SendPostRequest(Constants.banUser, data, callbackOnSuccess, callbackOnFail);
    }

    //public void POSTEXMPL(UnityAction<FacilitatorLogin> callbackOnSuccess, UnityAction<string> callbackOnFail, string data)
    //{
    //    SendPostRequest("", data,callbackOnSuccess, callbackOnFail);
    //}

    #region [Server Communication]

    private void SendGetRequest<T>(string url, UnityAction<T> callbackOnSuccess, UnityAction<string> callbackOnFail)
    {
        StartCoroutine(RequestCoroutine(url, callbackOnSuccess, callbackOnFail));
    }

    private void SendPostRequest<T>(string url, string data,UnityAction<T> callbackOnSuccess, UnityAction<string> callbackOnFail)
    {
        StartCoroutine(RequestPostCoroutine(url, data,callbackOnSuccess, callbackOnFail));
    }

    private IEnumerator RequestPostCoroutine<T>(string url, string data,UnityAction<T> callbackOnSuccess, UnityAction<string> callbackOnFail)
    {
        var www = UnityWebRequest.Post(url, data);
        UploadHandler jsonHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(data))
        {
            contentType = "application/json"
        };
        www.uploadHandler = jsonHandler;
        yield return www.SendWebRequest();
        if (www.isNetworkError)
        {
            Debug.LogError(www.error);
            callbackOnFail?.Invoke(www.error);            
        }
        else if (www.isHttpError)
        {
            Debug.Log(www.downloadHandler.text +"     :     " + url);
            callbackOnFail?.Invoke(www.error + "|" + www.downloadHandler.text);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            ParseResponse(www.downloadHandler.text, callbackOnSuccess, callbackOnFail);
        }
    }

    private IEnumerator RequestCoroutine<T>(string url, UnityAction<T> callbackOnSuccess, UnityAction<string> callbackOnFail)
    {
        var www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();
        if (www.isNetworkError)
        {
            Debug.LogError(www.error);
            callbackOnFail?.Invoke(www.error);
        }
        else if (www.isHttpError)
        {
            Debug.Log(www.downloadHandler.text);
            callbackOnFail?.Invoke(www.error + "|" + www.downloadHandler.text);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            ParseResponse(www.downloadHandler.text, callbackOnSuccess, callbackOnFail);
        }
    }

    private void ParseResponse<T>(string data, UnityAction<T> callbackOnSuccess, UnityAction<string> callbackOnFail)
    {
        Debug.Log("JSON DATA : " + data);
        Debug.Log("TYPE : " + callbackOnSuccess);

        var parsedData = JsonUtility.FromJson<T>(data);
        callbackOnSuccess?.Invoke(parsedData);       
    }
    #endregion
}
