
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;

public class FBS : MonoBehaviour
{
    private string status = "Ready";
    private string lastResponse = string.Empty;
    public Text freindTextList;

    private void Awake()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(() =>
            {
                if (FB.IsInitialized)
                {
                    FB.ActivateApp();
                }
                else
                {
                    Debug.LogError("couldn't int");
                }
            },
            isgameshown =>
            {
                if (!isgameshown)
                {
                    Time.timeScale = 0;
                }
                else
                {
                    Time.timeScale = 1;
                }
            });
        }
        else
        {
            FB.ActivateApp();
        }
        
    }
    #region login
    public void FBLogIn()
    {
        var permission = new List<string>() { "Profile", "Email", "Friends" };
        var Call = 1;
        FB.LogInWithReadPermissions(permission, this.HandleResult);
    }

    public void FBLogOut()
    {
        FB.LogOut();
    }


    protected void HandleResult(IResult result)
    {
        if (result == null)
        {
            this.LastResponse = "Null Response\n";
            //LogView.AddLog(this.LastResponse);
            Debug.LogWarning("status:" + status + "response: " + lastResponse);
            return;
        }

        //this.LastResponseTexture = null;

        // Some platforms return the empty string instead of null.
        if (!string.IsNullOrEmpty(result.Error))
        {
            this.Status = "Error - Check log for details";
            this.LastResponse = "Error Response:\n" + result.Error;
        }
        else if (result.Cancelled)
        {
            this.Status = "Cancelled - Check log for details";
            this.LastResponse = "Cancelled Response:\n" + result.RawResult;
        }
        else if (!string.IsNullOrEmpty(result.RawResult))
        {
            this.Status = "Success - Check log for details";
            this.LastResponse = "Success Response:\n" + result.RawResult;
        }
        else
        {
            this.LastResponse = "Empty Response\n";
        }

        Debug.LogWarning("status:" + status + "response: " + lastResponse);
       // LogView.AddLog(result.ToString());
    }

    protected string Status
    {
        get
        {
            return this.status;
        }

        set
        {
            this.status = value;
        }
    }

    protected string LastResponse
    {
        get
        {
            return this.lastResponse;
        }

        set
        {
            this.lastResponse = value;
        }
    }
    #endregion

    public void FBShare()
    {
        FB.ShareLink(new System.Uri("http:://resocoder.com"), "cjeck it out!", "holle you went her", new System.Uri("https://www.google.com/"));
    }

    #region Invites
    public void FBGameRequest()
    {
        FB.AppRequest("play this cool game", title: "Forever End!");
    }
    //public void FBInvite()
    //{
    //    FB.Mobile.AppInvite(new System.Uri("https://www.google.com/"));
    //}
    public void FBFreindInvite()
    {
        string person = "/me.friends";
        FB.API(person, HttpMethod.GET, result =>
          {

              var dictionary = (Dictionary<string, object>)Facebook.MiniJSON.Json.Deserialize(result.RawResult);
              List<object> freindList = (List<object>)dictionary["data"];
              freindTextList.text = string.Empty;
              foreach (var dict in freindList)
              {
                  freindTextList.text += ((Dictionary<string, object>)dict)["name"];
              }
          });
       
    }
    #endregion
}
