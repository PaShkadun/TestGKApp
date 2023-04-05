using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using System;
using UnityEngine.Networking;

public class HttpService
{
    private const int TimeOut = 10;

    public async UniTask<PostData[]> GetData()
    {
        using var request = UnityWebRequest.Get("https://jsonplaceholder.typicode.com/posts");
        request.SetRequestHeader("Content-Type", "application/json");

        try
        {
            request.timeout = TimeOut;
            var requestTask = await request.SendWebRequest().ToUniTask();

            if (requestTask.result != UnityWebRequest.Result.Success)
            {
                throw new UnityWebRequestException(requestTask);
            }

            return JsonConvert.DeserializeObject<PostData[]>(requestTask.downloadHandler.text);
        }
        catch (Exception e)
        {
            throw;
        }
    }
}
