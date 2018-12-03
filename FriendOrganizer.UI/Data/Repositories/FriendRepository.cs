using FriendOrganizer.DataAccess;
using FriendOrganizer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
            //TODO Load data from a real database
            //yield return new Friend { FirstName = "Thomas", LastName = "Huber" };
            //yield return new Friend { FirstName = "Andreas", LastName = "Boehler" };
            //yield return new Friend { FirstName = "Julia", LastName = "Huber" };
            //yield return new Friend { FirstName = "Chrisi", LastName = "Egin" };

            //need to add entityFramework to reference
            //replace using (var ctx = new FriendOrganizerDbContext())

            // the context is created and grabs the friend with the specic
            // friend ID from the Database and the sected friend is displayed in the detailed view

            //using (var ctx = _contextCreator())
            //{
            //    //return ctx.Friends.AsNoTracking().ToList();
            //    //return await ctx.Friends.AsNoTracking().ToListAsync();
            //    return await ctx.Friends.AsNoTracking().SingleAsync(f => f.Id == friendId);
            //}


            //we want the context to track the entities it has loaded so get rid of AsNoTracking
            return await _context.Friends.SingleAsync(f => f.Id == friendId);
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }

        public void Remove(Friend model)
        {
            _context.Friends.Remove(model);
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
