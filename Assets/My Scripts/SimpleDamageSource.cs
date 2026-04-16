using UnityEngine;

public class SimpleDamageSource : IDamageSource
{
    public GameObject Owner { get; private set; }
    public Team Team { get; private set; }

    public SimpleDamageSource(GameObject owner, Team team)
    {
        Owner = owner;
        Team = team;
    }
}
