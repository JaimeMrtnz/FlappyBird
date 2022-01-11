using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

/// <summary>
/// Controls the login depending the platform
/// </summary>
public class PlayFabLoginManager
{
    public static void LogIn()
    {
#if UNITY_ANDROID

        var request = new LoginWithAndroidDeviceIDRequest
        {
            AndroidDeviceId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };

        new LoginAndroid().LogIn(request);

#endif

    }
}
