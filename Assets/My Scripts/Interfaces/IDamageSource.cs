using UnityEngine;

public interface IDamageSource
{
    GameObject Owner 
    { 
        get; 
    }
    Team Team 
    {
        get; 
    }
}
