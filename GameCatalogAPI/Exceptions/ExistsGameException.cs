using System;

namespace GameCatalogAPI.Exceptions
{
    public class ExistsGameException : Exception
    {
        public ExistsGameException()
            : base("This game is already registered")
        { }
    }
}
