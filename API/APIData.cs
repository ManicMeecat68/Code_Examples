using System;
using System.Collections.Generic;

[Serializable]
public class FacilitatorLogin
{
    public string email;
    public string password;

    //[System.Serializable]
    //public class Games
    //{
    //    public int gameId;
    //    public string assignedEmail;
    //    public string gameKey;
    //}
    //public List<Games> games;
}

[Serializable]
public class FacilitatorRequest
{
    public string email;
    public string api_key;
}

[Serializable]
public class AvailibleGames
{
    [Serializable]
    public class Game
    {
        public string api_key;
        public string license_key;
    }
	public List<Game> games;
}
[Serializable]
public class UsedGames
{
    [Serializable]
    public class Game
    {
        public string api_key;
        public string license_key;
        public string email;
        public string used_date;
        public string city;
        public string country;
        public bool focus_devops;
        public bool focus_itsm;
        public bool focus_digital;
        public bool focus_agile;
        public bool focus_xla;

        //public Focus focus;
        //[Serializable]
        //public class Focus
        //{
        //    public bool devops;
        //    public bool itsm;
        //    public bool digital;
        //    public bool agile;
        //    public bool xlaother;
        //}
    }

    public List<Game> used_games;
}
//4 Joooosh
[Serializable]
public class Focus
{
    public bool devops;
    public bool itsm;
    public bool digital;
    public bool agile;
    public bool xlaother;
}

[Serializable]
public class Error
{
    public string error;
}

