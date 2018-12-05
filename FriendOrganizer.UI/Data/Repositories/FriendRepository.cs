using FriendOrganizer.DataAccess;
using FriendOrganizer.Model;
using System.Data.Entity;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.Data.Repositories
{
    public class FriendRepository : IFriendRepository
    {
        // private Func<FriendOrganizerDbContext> _contextCreator;
        private FriendOrganizerDbContext _context;

        //we have set up Dependency Injection with Autofac so we should get the FriendOrgainser Dbcontext 
        // injected into our FriendData Service
        // public FriendRepository(Func<FriendOrganizerDbContext> contextCreator )
        public FriendRepository(FriendOrganizerDbContext context)
        {
            // _contextCreator = contextCreator;
            _context = context;
        }

        public void Add(Friend friend)
        {
            _context.Friends.Add(friend);
        }

        //public IEnumerable<Friend> GetAll()
        //Make it Async
        //public async Task<List<Friend>> GetAllAsync()
        //we now just want t return a single friend by ID
        public async Task<Friend> GetByIdAsync(int friendId)
        {
            
            //we want the context to track the entities it has loaded so get rid of AsNoTracking
            return await _context.Friends
                .Include(f=> f.PhoneNumbers)
                .SingleAsync(f => f.Id == friendId);
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }

        public void Remove(Friend model)
        {
            _context.Friends.Remove(model);
        }

        public void RemovePhoneNumber(FriendPhoneNumber model)
        {
            _context.FriendPhoneNumbers.Remove(model);
        }

        /// <summary>
        /// Save the friend using entityFramework
        /// </summary>
        /// <param name="friend"></param>
        /// <returns></returns>
        // public async Task SaveAsync(Friend friend)
        public async Task SaveAsync()
        {
            //context in a field don't need the using block now we don't have to attach anything or set any state
            //using (var ctx = _contextCreator())
            //{
            //    ctx.Friends.Attach(friend);
            //    ctx.Entry(friend).State = EntityState.Modified;
            //     await ctx.SaveChangesAsync();
            //}

            await _context.SaveChangesAsync();
        }
    }
}
