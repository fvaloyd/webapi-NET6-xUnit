using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Personas.Api .model;
using Microsoft.EntityFrameworkCore;

namespace Personas.Api .context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) {}
        public DbSet<Persona> Personas { get; set; } = null!;
    }
}