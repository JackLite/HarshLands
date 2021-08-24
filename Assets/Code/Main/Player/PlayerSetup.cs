using System;

namespace Main.Player
{
    public class PlayerSetup : EcsSetup
    {
        protected override Type Type => GetType();
    }
}