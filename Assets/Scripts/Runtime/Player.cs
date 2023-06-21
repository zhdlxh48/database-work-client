namespace Runtime
{
    public class Player
    {
        public int PlayerID { get; private set; }
        public string PlayerName { get; private set; }
        public string PlayerEmail { get; private set; }
        public string PlayerPassword { get; private set; }
        
        public Player(int id, string name, string email, string password)
        {
            PlayerID = id;
            PlayerName = name;
            PlayerEmail = email;
            PlayerPassword = password;
        }
        
        public Player(int id, string name)
        {
            PlayerID = id;
            PlayerName = name;
        }
    }
}