using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AAAATHIDEMO.Models;

namespace AAAATHIDEMO.Data
{
    public class AAAATHIDEMOContext : DbContext
    {
        public AAAATHIDEMOContext (DbContextOptions<AAAATHIDEMOContext> options)
            : base(options)
        {
        }

        public DbSet<AAAATHIDEMO.Models.Student> Student { get; set; } = default!;

        public DbSet<AAAATHIDEMO.Models.Dongvat> Dongvat { get; set; } = default!;

        public DbSet<AAAATHIDEMO.Models.Cat> Cat { get; set; } = default!;

        public DbSet<AAAATHIDEMO.Models.Faculty> Faculty { get; set; } = default!;
    }
}
