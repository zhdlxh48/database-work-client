using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace Runtime
{
    public static class PlayerWebHandler
    {
        public static async UniTask<(bool success, List<Player> players)> GetPlayerData()
        {
            using var req = UnityWebRequest.Get("http://localhost:3000/api/player/list");
            
            try
            {
                await req.SendWebRequest();
            }
            catch (UnityWebRequestException ex)
            {
                Debug.LogError(ex.Error);
                return (false, null);
            }

            var res = req.downloadHandler.text;
            var jsonData = JObject.Parse(res);
            Debug.Log(jsonData.ToString());
            var jsonPlayers = (JArray)jsonData["message"];
            if (jsonPlayers == null)
            {
                return (false, null);
            }

            var players = new List<Player>();
            foreach (var jsonPlayer in jsonPlayers)
            {
                var player = new Player(jsonPlayer["player_id"]?.ToObject<int>() ?? -1, jsonPlayer["player_name"]?.ToString() ?? "");
                players.Add(player);
            }
            return (true, players);
        }
        
        public static async UniTask<(bool success, string response)> GiveItem(int playerId, int itemId)
        {
            var jsonData = new JObject
            {
                { "player_id", playerId }, 
                { "item_id", itemId }
            };

            Debug.Log(jsonData.ToString());

            using var req = UnityWebRequest.Post("http://localhost:3000/api/inventory", jsonData.ToString(),
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