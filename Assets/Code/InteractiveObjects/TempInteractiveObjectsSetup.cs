using System;
using Main;

namespace InteractiveObjects
{
    public class TempInteractiveObjectsSetup : EcsSetup
    {
        protected override Type Type => GetType();
    }
}