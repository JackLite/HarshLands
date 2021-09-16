using System;
using EcsCore;
using Main;

namespace InteractiveObjects
{
    public class TempInteractiveObjectsSetup : EcsSetup
    {
        protected override Type Type => GetType();
    }
}