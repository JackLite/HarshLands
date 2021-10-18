using System;
using EcsCore;

namespace Main.Mobs
{
    public class MobsSetup : EcsSetup
    {
        protected override Type Type => GetType();
    }
}