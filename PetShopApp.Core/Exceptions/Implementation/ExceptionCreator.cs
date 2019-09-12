using System.IO;

namespace PetShopApp.Core.Exceptions.Implementation
{
    public class ExceptionCreator : IExceptionCreator
    {
        public void Invalid(string message)
        {
            throw new InvalidDataException(message);
        }
    }
}