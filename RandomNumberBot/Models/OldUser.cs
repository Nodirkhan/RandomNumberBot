using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace RandomNumberBot.Entity
{
    public class OldUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public long UserId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VoterNumber { get; set; }

        public string Username { get; set; }

        public string FullName { get; set; }

        public Language Language { get; set; }

        public string ContactNumber { get; set; }

        public bool IsSubscriber { get; set; } = false;

        public virtual List<string> Comments { get; set; }

        public int PlaceNumber { get; set; } = 1;

        public OldUser()
        {
            Comments = new List<string>();
        }
        public static explicit operator OldUser(User user)
        {
            return new OldUser
            {
                UserId = user.UserId,
                Username = user.Username,
                FullName = user.FullName,
                Language = user.Language,
                ContactNumber = user.ContactNumber,
                IsSubscriber = user.IsSubscriber,
                Comments = user.Comments
            };
        }
    }
}
