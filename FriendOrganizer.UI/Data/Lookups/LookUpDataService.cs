using FriendOrganizer.DataAccess;
using FriendOrganizer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.Data.Lookups
{
    public class LookUpDataService : IFriendLookUpDataService, IProgrammingLanguageLookUpDataService,
        IMeetingLookUpDataService
    {
        private Func<FriendOrganizerDbContext> _contextCreator;

        /// <summary>
        /// A delegate holds reference to a function i.e 
        /// </summary>
        /// <param name="contextCreator"></param>
        public LookUpDataService(Func<FriendOrganizerDbContext> contextCreator)
        {

            //store the value in a field_contextCreator = contextCreator;
            _contextCreator = contextCreator;
        }
        /// <summary>
        /// a Task lets you create threads and run them asynchronously.
        /// A Task is an object that represents some work that should be done.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<LookupItem>> GetFriendLookUpAsync()
        {
            using (var ctx = _contextCreator())
            {
                return await  ctx.Friends.AsNoTracking()
                    .Select(f =>
                    new LookupItem
                    {
                        Id = f.Id,
                        DisplayMember = f.FirstName + " " + f.LastName
                    })
                    .ToListAsync();
            }
        }
        public async Task<IEnumerable<LookupItem>> GetProgrammingLanguageLookUpAsync()
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.ProgrammingLanguages.AsNoTracking()
                    .Select(f =>
                    new LookupItem
                    {
                        Id = f.Id,
                        DisplayMember = f.Name
                    })
                    .ToListAsync();
            }
        }

        public async Task<List<LookupItem>> GetMeetingLookUpAsync()
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.Meetings.AsNoTracking()
                    .Select(m =>
                    new LookupItem
                    {
                        Id = m.Id,
                        DisplayMember = m.Title
                    })
                    .ToListAsync();
            }
        }

      
    }
}
