using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace enums
{
    [System.Flags]
    public enum PopType
    {
        None    =   0,  //required
        Normal  =   1 << 0, //1 aka 001
        Camo    =   1 << 1, //2 aka 010
        Lead    =   1 << 2, //4 aka 100
    }

    public enum ProjType
    {
        Dart      = 0,
        Shuriken  = 1,
        Explosive = 2,
    }

    public enum PowerUpType
    {
        // May add more in the future
        Shield = 0,
        Camo   = 1,   
        Lead   = 2,
    }

    public enum ObstacleType
    {
        RoadSpikes = 0,
        HotSpikes  = 1,
        Glue       = 2,
    }

    public enum GameState
    {
        GAME      = 0,
        PAUSED    = 1,
        GAME_OVER = 2,
    }
}
