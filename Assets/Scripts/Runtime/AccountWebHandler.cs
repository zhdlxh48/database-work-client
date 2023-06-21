using System;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

namespace Runtime
{
    public static class AccountWebHandler
    {
        public static async UniTask<(bool success, string response)> Login(string email, string password)
        {
            var jsonData = new JObject
            {
                { "email", email },
                { "password", password }
            };

            Debug.Log(jsonData);

            using var req = UnityWebRequest.Post("http://localhost:3000/api/player/login", jsonData.ToString(),
                "application/json");
            
            try
            {
                await req.SendWebRequest();
            }
            catch (UnityWebRequestException ex)
            {
                Debug.LogError(ex.Error);
                return (false, ex.Text);
            }

            var res = req.downloadHandler.text;
            return (true, res);
        }
        
        public static async UniTask<(bool success, string response)> Register(string username, string email, string password)
        {
            var jsonData = new JObject
            {
                { "username", username },
                { "email", email },
                { "password", password }
            };

            Debug.Log(jsonData);

            using var req = UnityWebRequest.Post("http://localhost:3000/api/player/register", jsonData.ToString(),
                "application/json");

            try
            {
                await req.SendWebRequest();
            }
            catch (UnityWebRequestException ex)
            {
                Debug.LogError(ex.Error);
                return (false, ex.Text);
            }

            var res = req.downloadHandler.text;
            return (true, res);
        }
    }
}
