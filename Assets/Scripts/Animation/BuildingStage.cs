using DG.Tweening;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public abstract class BuildingStage
{
    public float _level;
    public int _duration;
    public Sequence _sequence;

    public abstract void Build(Material readyBuilding, Material templateBuilding);

    public void SetSequence(Sequence someSequence)
    {
        _sequence = someSequence;
    }

    public void Tween(Material readyBuilding, Material templateBuilding)
    {
        _sequence.Append(readyBuilding.DOFloat(_level, "_PercentFinished", _duration));
        _sequence.Join(templateBuilding.DOFloat(_level, "_PercentFinished", _duration));
    }
}

public class TemplateStage : BuildingStage
{
    public override void Build(Material readyBuilding, Material templateBuilding)
    {
        _level = -0.3f;
        _duration = 1;
        Tween(readyBuilding, templateBuilding);
    }
}

public class AssemblingStage : BuildingStage
{
    public override void Build(Material readyBuilding, Material templateBuilding)
    {
        _level = 2f;
        _duration = 10;
        Tween(readyBuilding, templateBuilding);
    }
}

public class CoolingStage : BuildingStage
{
    public override void Build(Material readyBuilding, Material templateBuilding)
    {
        _level = 10f;
        _duration = 5;
        Tween(readyBuilding, templateBuilding);
    }
}
