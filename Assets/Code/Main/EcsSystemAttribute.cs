using System;

namespace Main
{
    public class EcsSystemAttribute : Attribute
    {
        public readonly Type Startup;
        public EcsSystemAttribute(Type startup)
        {
            Startup = startup;
        }
    }
}