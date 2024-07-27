using System;
using System.Collections;
using System.Collections.Generic;


public class MatchData {
    public static Action<int> KillsUpdate;
    public static int[] _kills ={0,0,0,0,0,0,0,0};
    public static int Kills {
        get { return _kills[8]; }
        set {
            for (int i = 0; i < 8; i++)
            {
                _kills[i] = value;
                KillsUpdate?.Invoke(Kills);
            }
        }
    }
    public static Action<int> DeathsUpdate;
    public static int[] _deaths={0,0,0,0,0,0,0,0};
    public static int Deaths {
        get { return _deaths[8]; }
        set {
            for (int i = 0; i < 8; i++)
            {
                _deaths[i] = value;
                DeathsUpdate?.Invoke(Deaths);
            }
        }
    }
    public static string[]  _names ={"Carlina", "Gideon", "Uliath", "Remagine", "Sicofla", "Jaygee", "Player 1", "Player 2"};
    public static string Names {
        get { return _names[8]; }       
    }
}
