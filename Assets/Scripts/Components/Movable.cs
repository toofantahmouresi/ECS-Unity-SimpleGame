using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent] // just gonna create an Authoring Game Object to get attached by script
public struct Movable : IComponentData
{
    public float speed;
    public float3 direction;
}
