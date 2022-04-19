namespace Another_URL_Shortener.Services
{
    public interface ICommandHandler<T> : IHandler<T> where T : class
    {
        
    }
}