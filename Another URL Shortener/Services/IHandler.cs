namespace Another_URL_Shortener.Services
{
    public interface IHandler<T>
    {
        void Handle(T entity);
    }
}