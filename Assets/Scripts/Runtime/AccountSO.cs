using UnityEngine;

namespace Runtime
{
    [CreateAssetMenu(fileName = nameof(AccountSO), menuName = "Network Assets/" + nameof(AccountSO), order = 0)]
    public class AccountSO : ScriptableObject
    {
        [field: SerializeField] public int PlayerID { get; private set; }
        [field: SerializeField] public string Username { get; private set; }
        [field: SerializeField] public string Email { get; private set; }
        [field: SerializeField] public string Password { get; private set; }
        
        public void SetPlayerID(int playerID)
        {
            PlayerID = playerID;
        }
        
        public void SetUsername(string username)
        {
            Username = username;
        }
        
        public void SetEmail(string email)
        {
            Email = email;
        }
        
        public void SetPassword(string password)
        {
            Password = password;
        }
    }
}