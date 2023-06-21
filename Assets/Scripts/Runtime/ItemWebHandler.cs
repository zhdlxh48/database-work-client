using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace Runtime
{
    public static class ItemWebHandler
    {
        public static async UniTask<(bool success, List<Item> items)> GetItems()
        {
            using var req = UnityWebRequest.Get("http://localhost:3000/api/item/list");
            
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
            var jsonItems = (JArray)jsonData["message"];
            if (jsonItems == null)
            {
                return (false, null);
            }

            var items = new List<Item>();
            foreach (var jsonItem in jsonItems)
            {
                var item = new Item(jsonItem["item_id"]?.ToObject<int>() ?? -1, jsonItem["item_type"]?.ToString() ?? "",
                    jsonItem["item_name"]?.ToString() ?? "");
                items.Add(item);
            }
            return (true, items);
        }
        
        public static async UniTask<(bool success, Item item)> GetItemByID(int id)
        {
            using var req = UnityWebRequest.Get($"http://localhost:3000/api/item?id={id}");
            
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
            var jsonItem = jsonData["message"];
            if (jsonItem == null)
            {
                return (false, null);
            }
            
            var item = new Item(jsonItem["item_id"]?.ToObject<int>() ?? -1, jsonItem["item_type"]?.ToString() ?? "",
                jsonItem["item_name"]?.ToString() ?? "");
            return (true, item);
        }
        
        public static async UniTask<(bool success, string response)> CreateItem(Item item)
        {
            var jsonData = new JObject
            {
                { "item_id", item.ItemID },
                { "item_type", item.ItemType }, 
                { "item_name", item.ItemName }
            };

            Debug.Log(jsonData.ToString());

            using var req = UnityWebRequest.Post("http://localhost:3000/api/item", jsonData.ToString(),
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