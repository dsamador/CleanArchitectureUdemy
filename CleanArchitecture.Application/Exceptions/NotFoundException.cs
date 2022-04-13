namespace CleanArchitecture.Application.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        //Mensaje personalizado, name es la entidad y key es la pro
        //piedad que puede ser el Id
        public NotFoundException(string name, object key) : 
            base($"Entity \"{name}\" ({key}) no fue encontrado ") { }
    }
}
