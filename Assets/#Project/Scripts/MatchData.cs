using System;
using System.Collections;
using System.Collections.Generic;


public class MatchData {
    public static Action<int> KillsUpdate;
    public static int[] _kills ={0,0,0,0,0,0,0};
    public static int Kills {
        get { return _kills[7]; }
        set {
            for (int i = 0; i < 7; i++)
            {
                _kills[i] = value;
                KillsUpdate?.Invoke(Kills);
            }
        }
    }
    public static Action<int> DeathsUpdate;
    public static int[] _deaths={0,0,0,0,0,0,0};
    public static int Deaths {
        get { return _deaths[7]; }
        set {
            for (int i = 0; i < 7; i++)
            {
                _deaths[i] = value;
                DeathsUpdate?.Invoke(Deaths);
            }
        }
    }
    public static string[]  _names ={"Carlina", "Gideon", "Uliath", "Remagine", "Sicofla", "Jaygee", "You"};
    public static string Names {
        get { return _names[7]; }       
    }
}
