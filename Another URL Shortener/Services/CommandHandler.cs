using Another_URL_Shortener.Models;

namespace Another_URL_Shortener.Services
{
    public class CommandHandler: ICommandHandler<Entity>
    {
        public void Handle(Entity entity)
        {
            // todo : save entry to db
        }
    }
}