using System;
using EcsCore;

namespace Main.Player
{
    public class PlayerSetup : EcsSetup
    {
        protected override Type Type => GetType();
    }
}