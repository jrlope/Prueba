using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ejemplo.Model
{
    public class User {
        public User() {
            CreatedDate = DateTime.Now;
            LastUpdatedDate = DateTime.Now;
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public string FBCode { get; set; }
        public string GoogleCode { get; set; }
        public string TeacherId { get; set; }
        public string StudentId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string ImageIdentifier { get; set; }
        public string StringIdentifier { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
