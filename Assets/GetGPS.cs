using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class GetGPS : MonoBehaviour
{
    string GetGps = "";
    public Text ShowGPS;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartGPS());
    }

    // Update is called once per frame
    void Update()
    {
        // 更新GPS信息
        UpdateGps();
    }

    void UpdateGps()
    {
        if (Input.location.status == LocationServiceStatus.Running)
        {
            GetGps = "N:" + Input.location.lastData.latitude + " E:" + Input.location.lastData.longitude;
            GetGps = GetGps + " Time:" + Input.location.lastData.timestamp;
            ShowGPS.text = GetGps;
            Debug.Log(GetGps);
        }
    }

    void StopGPS()
    {
        Input.location.Stop();
    }

    IEnumerator StartGPS()
    {
        if (!Input.location.isEnabledByUser)
        {
            ShowGPS.text = "User has not enabled GPS";
            yield break;
        }

        Input.location.Start(10.0f, 10.0f);

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait < 1)
        {
            ShowGPS.text = "Timed out";
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            ShowGPS.text = "Unable to determine device location";
            yield break;
        }
    }
}
