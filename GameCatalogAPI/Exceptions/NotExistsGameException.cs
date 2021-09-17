using System;

namespace GameCatalogAPI.Exceptions
{
    public class NotExistsGameException: Exception
    {
        public NotExistsGameException()
            :base("This game is not registered")
        {}
    }
}
