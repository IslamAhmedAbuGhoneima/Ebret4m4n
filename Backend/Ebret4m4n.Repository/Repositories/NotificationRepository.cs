using Ebret4m4n.Contracts;
using Ebret4m4n.Entities.Models;

namespace Ebret4m4n.Repository.Repositories;

class NotificationRepository : BaseRepository<Notification>, INotificationRepository
{
    public NotificationRepository(EbretAmanDbContext context) 
        : base(context)
    { }
}
