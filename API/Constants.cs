using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour
{
    public static string webURL = "https://portal.sxp.solutions/";
    public static string APIUrl = webURL + "api/v1/";
    public static string login = APIUrl + "auth";
    public static string getCredit = APIUrl + "license";
    public static string sendCredit = APIUrl + "updatelicense";
    public static string banUser = APIUrl + "ban";

    public static string userLogin = "User_Login";
    public static string userToken = "User_Access";
    public static string userAvailibleGames = "Availible_Games";
    public static string userUsedGames = "Used_Games";
}
