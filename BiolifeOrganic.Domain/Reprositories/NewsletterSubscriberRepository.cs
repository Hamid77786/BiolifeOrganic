using BiolifeOrganic.Dll.DataContext;
using BiolifeOrganic.Dll.DataContext.Entities;
using BiolifeOrganic.Dll.Reprositories.Contracts;

namespace BiolifeOrganic.Dll.Reprositories;

public class NewsletterSubscriberRepository : EfCoreRepository<NewsletterSubscriber>,INewsletterSubscriberRepository
{
    public NewsletterSubscriberRepository(AppDbContext dbContext) : base(dbContext)
    {
        
    }
}
