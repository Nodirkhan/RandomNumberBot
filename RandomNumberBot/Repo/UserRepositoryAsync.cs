using Microsoft.EntityFrameworkCore;
using RandomNumberBot.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RandomNumberBot.Repo
{
    public class UserRepositoryAsync
    {
        private ApplicationDbContext _context = new();
        private bool IsRandom = true;

        public async Task<List<int>> RandomUser()
        {
            List<int> selectedNumbers = new List<int>(3);
            var numbers = await _context.Users.Select(u => u.VoterNumber).ToListAsync();
            var Length = numbers.Count;

            int i = numbers.Count > 3 ? 3 : numbers.Count;
            while(i > 0)
            {
                Random random = new Random();
                int randomIndex = random.Next(0, Length);
                if (!selectedNumbers.Contains(numbers[randomIndex]))
                {
                    selectedNumbers.Add(numbers[randomIndex]);
                    i--;
                }
            }

            return selectedNumbers;
        }
        public async Task<bool> CopyAndDelete()
        {
            try
            {
                int Count = await _context.Users.CountAsync();
                var position = await _context.RegionCounters.SingleAsync();
                int size = 1;

                int pages = Count / size + 1;

                for (int page = 0; page < pages; page++)
                {
                    var users = await _context.Users.AsNoTracking()
                        .Skip(page * size)
                        .Take(size).ToListAsync();

                    foreach (var user in users)
                    {
                        var oldUser = (OldUser)user;
                        oldUser.PlaceNumber = position.PositionNumber;
                        await _context.OldUsers.AddAsync(oldUser);
                    }
                    _context.Users.RemoveRange(users);
                }
                position.PositionNumber += 1;
                await _context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }

            return true;


        }
    }
}
