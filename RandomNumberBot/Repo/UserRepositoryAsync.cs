using Microsoft.EntityFrameworkCore;
using RandomNumberBot.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomNumberBot.Repo
{
    public class UserRepositoryAsync
    {
        private ApplicationDbContext _context = new();
        private bool IsRandom = true;
        private static List<long> randomed = new List<long>();

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

        public async Task<User> RandomAndGetUser()
        {
            bool IsRandom = true;
            var numbers = await _context.Users.Where(u => u.Comments.Count > 0)
                .Select(u => u.UserId).ToListAsync();

            var Length = numbers.Count;
            Random random = new Random();
            int randomIndex = random.Next(0, Length);

            if (numbers.Count == 0)
                return null;

            while (IsRandom)
            {
                if (Length < 0)
                    return null;

                if (!randomed.Contains(numbers[randomIndex]))
                {
                    randomed.Add(numbers[randomIndex]);
                    IsRandom = false;
                }
                Length--;
            }

            var user = await _context.Users.AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserId == numbers[randomIndex]);

            return user;

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
                randomed.Clear();
            }
            catch
            {
                return false;
            }

            return true;


        }
    }
}
