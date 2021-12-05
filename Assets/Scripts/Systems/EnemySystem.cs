using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Physics;
using Unity.Physics.Systems;

public class EnemySystem : SystemBase
{
    
    protected override void OnUpdate()
    {
        var raycastChecker = new RayCastCheck();
        Entities.ForEach((ref Movable mov, ref Enemy enemy, in Translation trns) =>
        {
            if(math.distance(trns.Value,enemy.previousCell)> 0.9)
            {
                enemy.previousCell = math.round(trns.Value);

                //perform raycast here!
                raycastChecker.CheckRay();

            }
        }).Schedule();
    }

    public struct RayCastCheck
    {
        public bool CheckRay()
        {
            return true;
        }
    }
}
