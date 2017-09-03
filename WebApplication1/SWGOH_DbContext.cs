using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1
{
    public class SWGOH_DbContext : DbContext
    {
        public DbSet<CharacterModel> Characters { get; set; }
        public DbSet<GuildModel> Guilds { get; set; }
        public DbSet<GuildMemberModel> GuildMembers { get; set; }
        public DbSet<MemberCharacterModel> MemberCharacters { get; set; }
    }
}