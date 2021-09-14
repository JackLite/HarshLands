using System;

namespace Main
{
    public class EcsSystemAttribute : Attribute
    {
        public readonly Type Setup;
        public EcsSystemAttribute(Type setup)
        {
            Setup = setup;
        }
    }
}