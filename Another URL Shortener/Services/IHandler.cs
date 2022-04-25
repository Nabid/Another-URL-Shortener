namespace Another_URL_Shortener.Services
{
    public interface IHandler<T> where T : class
    {
        void Handle(T entity);
    }
}