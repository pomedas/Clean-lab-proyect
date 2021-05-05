using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Logger : MonoBehaviour
{
    static string filepath;
    static string fileUserpath;
    static string DateTimeFormat = "yyyy/MM/dd HH:mm:ss.ffff";

    // Start is called before the first frame update
    void Start()
    {
        filepath = Application.persistentDataPath + "/" + Application.productName + "_" + System.DateTime.Now.ToString("yyyy_MM_dd_HH_mm") + ".log";
        fileUserpath = Application.persistentDataPath + "/" + Application.productName + "_" + System.DateTime.Now.ToString("yyyy_MM_dd_HH_mm") + "_USERDATA.log";
        addLogLine("[", true);
        addUserPositionLogLine("[", true);
    }

    void OnApplicationQuit()
    {
#if UNITY_EDITOR

        return;
#endif
        string fullog = File.ReadAllText(filepath);
        if (fullog.Length > 1)
        {
            fullog = fullog.Substring(0, fullog.Length - 1);
            fullog += "]";
            File.WriteAllText(filepath, fullog);
        }
        else
        {
            File.Delete(filepath);
        }

        string fullogUser = File.ReadAllText(fileUserpath);
        if (fullogUser.Length > 1)
        {
            fullogUser = fullogUser.Substring(0, fullogUser.Length - 1);
            fullogUser += "]";
            File.WriteAllText(fileUserpath, fullogUser);
        }
        else {
            File.Delete(fileUserpath);
        }
    }

    private static void addUserPositionLogLine(string content, bool ending = false)
    {
#if UNITY_EDITOR

        return;
#endif
        if (ending)
        {
            File.AppendAllText(fileUserpath, content);
        }
        else
        {
            File.AppendAllText(fileUserpath, content + ",");
        }
    }

    private static void addLogLine(string content, bool ending = false) {
#if UNITY_EDITOR

        return;
#endif
        if (ending)
        {
            File.AppendAllText(filepath, content);
        }
        else
        {
            File.AppendAllText(filepath, content + ",");
        }
    }

    #region WhiteHoleLog
    public static void AddWhiteHoleActivatedEventOnLog(string Playername, ParticleType type, Vector3 position) {
        Dictionary<string, string> val = new Dictionary<string, string>();
        val.Add("Time", System.DateTime.Now.ToString(DateTimeFormat));
        val.Add("Event", "WhiteHole.SinglePlayer.Touch");
        val.Add("Player.Color", Playername);
        val.Add("Particle.Type", type.ToString());
        val.Add("WhiteHole.Position.X", position.x.ToString());
        val.Add("WhiteHole.Position.Y", position.z.ToString());
        addLogLine(JsonFormat(val));
    }
    //public static void AddWhiteHoleCancelEventOnLog(string Playername, ParticleType type, Vector3 position)
    //{
    //    Dictionary<string, string> val = new Dictionary<string, string>();
    //    val.Add("Time", System.DateTime.Now.ToString(DateTimeFormat));
    //    val.Add("Event", "WhiteHole.SinglePlayer.Exit");
    //    val.Add("Player.Color", Playername);
    //    val.Add("Particle.Type", type.ToString());
    //    val.Add("WhiteHole.Position.X", position.x.ToString());
    //    val.Add("WhiteHole.Position.Y", position.z.ToString());
    //    addLogLine(JsonFormat(val));
    //}
    public static void AddWhiteHoleUsedEventOnLog(string Playername, ParticleType type, Vector3 position)
    {
        Dictionary<string, string> val = new Dictionary<string, string>();
        val.Add("Time", System.DateTime.Now.ToString(DateTimeFormat));
        val.Add("Event", "WhiteHole.SinglePlayer.Trigger");
        val.Add("Player.Color", Playername);
        val.Add("Particle.Type", type.ToString());
        val.Add("WhiteHole.Position.X", position.x.ToString());
        val.Add("WhiteHole.Position.Y", position.z.ToString());
        addLogLine(JsonFormat(val));
    }
    public static void AddWhiteHoleUsedCollaborativelyEventOnLog(ParticleType type1, ParticleType type2, Vector3 position1, Vector3 position2)
    {
        Dictionary<string, string> val = new Dictionary<string, string>();
        val.Add("Time", System.DateTime.Now.ToString(DateTimeFormat));
        val.Add("Event", "WhiteHole.MultiPlayer.Trigger");
        val.Add("Particle.1.Type", type1.ToString());
        val.Add("Particle.2.Type", type2.ToString());
        val.Add("WhiteHole.1.Position.X", position1.x.ToString());
        val.Add("WhiteHole.1.Position.Y", position1.z.ToString());
        val.Add("WhiteHole.2.Position.X", position2.x.ToString());
        val.Add("WhiteHole.2.Position.Y", position2.z.ToString());
        addLogLine(JsonFormat(val));
    }
    #endregion

    #region PlanetLog
    public static void AddPlanetCretedEventOnLog(string Playername, ParticleType[] type, string PlanetID, RoamingObjectType core, Vector3 position)
    {
        Dictionary<string, string> val = new Dictionary<string, string>();
        val.Add("Time", System.DateTime.Now.ToString(DateTimeFormat));
        val.Add("Event", "Planet.SinglePlayer.Creation");
        val.Add("Player.Color", Playername);
        string s = "[";
        foreach (ParticleType t in type) {
            s += t.ToString() + ", ";
        }
        if (type.Length > 0) {
            s = s.Substring(0, s.Length - 2);
        }
        s += "]";
        val.Add("Particle.Type", s);
        val.Add("Particle.Count", type.Length.ToString());
        val.Add("Planet.Id", PlanetID);
        val.Add("Planet.Core", core.ToString());
        val.Add("Planet.Position.X", position.x.ToString());
        val.Add("Planet.Position.Y", position.z.ToString());
        addLogLine(JsonFormat(val));
    }
    public static void AddPlanetCloseByPlayerEventOnLog(string Playername, string PlanetID, Vector3 position)
    {
        Dictionary<string, string> val = new Dictionary<string, string>();
        val.Add("Time", System.DateTime.Now.ToString(DateTimeFormat));
        val.Add("Event", "Planet.SinglePlayer.Connection");
        val.Add("Player.Color", Playername);
        val.Add("Planet.Id", PlanetID);
        val.Add("Planet.Position.X", position.x.ToString());
        val.Add("Planet.Position.Y", position.z.ToString());
        addLogLine(JsonFormat(val));
    }
    public static void AddPlanetMoveEventOnLog(string PlanetID, Vector3 position)
    {
        Dictionary<string, string> val = new Dictionary<string, string>();
        val.Add("Time", System.DateTime.Now.ToString(DateTimeFormat));
        val.Add("Event", "Planet.MultiPlayer.Move");
        val.Add("Planet.Id", PlanetID);
        val.Add("Planet.Position.X", position.x.ToString());
        val.Add("Planet.Position.Y", position.z.ToString());
        addLogLine(JsonFormat(val));
    }
    //public static void AddPlanetDroppedEventOnLog(string Playername_disconnect, string PlanetID, Vector3 position)
    //{
    //    Dictionary<string, string> val = new Dictionary<string, string>();
    //    val.Add("Time", System.DateTime.Now.ToString(DateTimeFormat));
    //    val.Add("Event", "Planet.SinglePlayer.Exit");
    //    val.Add("Player.Color", Playername_disconnect);
    //    val.Add("Planet.Id", PlanetID);
    //    val.Add("Planet.Position.X", position.x.ToString());
    //    val.Add("Planet.Position.Y", position.z.ToString());
    //    addLogLine(JsonFormat(val));
    //}
    public static void AddPlanetAddedToOrbitEventOnLog(string PlanetID, string StarID, Vector3 position)
    {
        Dictionary<string, string> val = new Dictionary<string, string>();
        val.Add("Time", System.DateTime.Now.ToString(DateTimeFormat));
        val.Add("Event", "Planet.MultiPlayer.AddedToOrbit");
        val.Add("Planet.Id", PlanetID);
        val.Add("Star.Id", StarID);
        val.Add("Planet.Position.X", position.x.ToString());
        val.Add("Planet.Position.Y", position.z.ToString());
        addLogLine(JsonFormat(val));
    }
    /*public static void AddPlanetRemovedFromOrbitEventOnLog(string PlanetID, string StarID, Vector3 position)
    {
        Dictionary<string, string> val = new Dictionary<string, string>();
        val.Add("EventType", "Planet Removed from orbit");
        val.Add("PlanetID", PlanetID);
        val.Add("StarID", StarID);
        val.Add("Position", position.ToString());
        val.Add("Timestamp", System.DateTime.Now.ToString(DateTimeFormat));
        addLogLine(JsonFormat(val));
    }*/
    #endregion

    #region StarLog
    public static void AddStarCretedEventOnLog(string StarID,int orbit, Vector3 position)
    {
        Dictionary<string, string> val = new Dictionary<string, string>();
        val.Add("Time", System.DateTime.Now.ToString(DateTimeFormat));
        val.Add("Event", "Star.MultiPlayer.Creation");
        val.Add("Star.Id", StarID);
        val.Add("Star.Orbit.Count", orbit.ToString());
        val.Add("Star.Position.X", position.x.ToString());
        val.Add("Star.Position.Y", position.z.ToString());
        addLogLine(JsonFormat(val));
    }
    /*public static void AddStarCloseByPlayerEventOnLog(string Playername, string StarID, Vector3 position)
    {
        Dictionary<string, string> val = new Dictionary<string, string>();
        val.Add("EventType", "Star Close By");
        val.Add("UserID", Playername);
        val.Add("StarID", StarID);
        val.Add("Position", position.ToString());
        val.Add("Timestamp", System.DateTime.Now.ToString(DateTimeFormat));
        addLogLine(JsonFormat(val));
    }
    public static void AddStarMoveEventOnLog(string StarID, Vector3 position)
    {
        Dictionary<string, string> val = new Dictionary<string, string>();
        val.Add("EventType", "Star Move");
        val.Add("StarID", StarID);
        val.Add("Position", position.ToString());
        val.Add("Timestamp", System.DateTime.Now.ToString(DateTimeFormat));
        addLogLine(JsonFormat(val));
    }
    public static void AddStarDroppedEventOnLog(string Playername_disconnect, string StarID, Vector3 position)
    {
        Dictionary<string, string> val = new Dictionary<string, string>();
        val.Add("EventType", "Star Dropped By");
        val.Add("UserID", Playername_disconnect);
        val.Add("StarID", StarID);
        val.Add("Position", position.ToString());
        val.Add("Timestamp", System.DateTime.Now.ToString(DateTimeFormat));
        addLogLine(JsonFormat(val));
    }*/
    #endregion

    #region BlackHoleLog
    public static void AddCompleteSolarSystemEventOnLog(string StarID, string[] PlanetID, string BlackHoleID, Vector3 position)
    {
        Dictionary<string, string> val = new Dictionary<string, string>();
        val.Add("Time", System.DateTime.Now.ToString(DateTimeFormat));
        val.Add("Event", "BlackHole.MultiPlayer.Creation");
        string s = "[";
        foreach (string t in PlanetID)
        {
            s += t + ", ";
        }
        if (PlanetID.Length > 0)
        {
            s = s.Substring(0, s.Length - 2);
        }
        s += "]";
        val.Add("Planet.Id", s);
        val.Add("Planet.Count", PlanetID.Length.ToString());
        val.Add("Star.Id", StarID);
        val.Add("BlackHole.Id", BlackHoleID);
        val.Add("BlackHole.Position.X", position.x.ToString());
        val.Add("BlackHole.Position.Y", position.z.ToString());
        addLogLine(JsonFormat(val));
    }
    public static void AddUsedBlackHoleEventOnLog(string BlackHoleID, Vector3 position)
    {
        Dictionary<string, string> val = new Dictionary<string, string>();
        val.Add("Time", System.DateTime.Now.ToString(DateTimeFormat));
        val.Add("Event", "BlackHole.MultiPlayer.Transition");
        val.Add("BlackHole.Id", BlackHoleID);
        val.Add("BlackHole.Position.X", position.x.ToString());
        val.Add("BlackHole.Position.Y", position.z.ToString());
        addLogLine(JsonFormat(val));
    }


    // TODO: Add BlackHole.SinglePlayer.Touch

    //public static void AddCancelUseBlackHoleEventOnLog(string BlackHoleID, Vector3 position)
    //{
    //    Dictionary<string, string> val = new Dictionary<string, string>();
    //    val.Add("Time", System.DateTime.Now.ToString(DateTimeFormat));
    //    val.Add("Event", "BlackHole.SinglePlayer.Exit");
    //    //Missing PLAYER COLOR
    //    val.Add("BlackHole.Id", BlackHoleID);
    //    val.Add("BlackHole.Position.X", position.x.ToString());
    //    val.Add("BlackHole.Position.Y", position.z.ToString());
    //    addLogLine(JsonFormat(val));
    //}
    public static void AddStartUseBlackHoleEventOnLog(string BlackHoleID, Vector3 position)
    {
        Dictionary<string, string> val = new Dictionary<string, string>();
        val.Add("Time", System.DateTime.Now.ToString(DateTimeFormat));
        val.Add("Event", "BlackHole.MultiPlayer.Touch");
        val.Add("BlackHole.Id", BlackHoleID);
        val.Add("BlackHole.Position.X", position.x.ToString());
        val.Add("BlackHole.Position.Y", position.z.ToString());
        addLogLine(JsonFormat(val));
    }
    public static void AddSendThroughBlackHoleEventOnLog(string BlackHoleID, string objectToSendID, Vector3 position)
    {
        Dictionary<string, string> val = new Dictionary<string, string>();
        val.Add("Time", System.DateTime.Now.ToString(DateTimeFormat));
        val.Add("Event", "BlackHole.MultiPlayer.TransferObject");
        val.Add("BlackHole.Id", BlackHoleID);
        val.Add("Object.Id", objectToSendID);
        val.Add("BlackHole.Position.X", position.x.ToString());
        val.Add("BlackHole.Position.Y", position.z.ToString());
        addLogLine(JsonFormat(val));
    }
    #endregion

    #region Asteroid
    public static void AddAsteroidCloseByPlayerEventOnLog(string Playername, string AsteroidID, Vector3 position)
    {
        // TODO: Fix logging only once per meaningful connection
        Dictionary<string, string> val = new Dictionary<string, string>();
        val.Add("Time", System.DateTime.Now.ToString(DateTimeFormat));
        val.Add("Event", "Asteroid.SinglePlayer.Connection");
        val.Add("Asteroid.Id", AsteroidID);
        val.Add("Player.Color", Playername);
        val.Add("Asteroid.Position.X", position.x.ToString());
        val.Add("Asteroid.Position.Y", position.z.ToString());
        addLogLine(JsonFormat(val));
    }
    public static void AddAsteroidTwoPlayerEventOnLog(string AsteroidID, Vector3 position)
    {
        // TODO: Fix logging only once per meaningful connection
        Dictionary<string, string> val = new Dictionary<string, string>();
        val.Add("Time", System.DateTime.Now.ToString(DateTimeFormat));
        val.Add("Event", "Asteroid.MultiPlayer.Connection");
        val.Add("Asteroid.Id", AsteroidID);
        val.Add("Asteroid.Position.X", position.x.ToString());
        val.Add("Asteroid.Position.Y", position.z.ToString());
        addLogLine(JsonFormat(val));
    }
    public static void AddAsteroidDroppedEventOnLog(string Playername_disconnect, string AsteroidID, Vector3 position)
    {
        // TODO: Fix logging only once per meaningful exit
        Dictionary<string, string> val = new Dictionary<string, string>();
        val.Add("Time", System.DateTime.Now.ToString(DateTimeFormat));
        val.Add("Event", "Asteroid.SinglePlayer.Exit");
        val.Add("Player.Color", Playername_disconnect);
        val.Add("Asteroid.Id", AsteroidID);
        val.Add("Asteroid.Position.X", position.x.ToString());
        val.Add("Asteroid.Position.Y", position.z.ToString());
        addLogLine(JsonFormat(val));
    }
    #endregion

    #region PositionLog
    public static void AddUserPosition(string playerID, Vector3 position)
    {
        Dictionary<string, string> val = new Dictionary<string, string>();
        val.Add("Time", System.DateTime.Now.ToString(DateTimeFormat));
        val.Add("Player.Color", playerID.ToString());
        val.Add("Position.X", position.x.ToString());
        val.Add("Position.Y", position.z.ToString());
        addUserPositionLogLine(JsonFormat(val));
    }
    #endregion

    private static string JsonFormat(Dictionary<string, string> values) {
        string s = "{";
        foreach (string k in values.Keys) {
            s += "\"" + k + "\" : \"" + values[k] + "\",";
        }
        if(values.Count > 0){
            s = s.Substring(0, s.Length - 1);
        }
        s += "}";
        return s;
    }
}
