using IdentityServer.DataAccess.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.DataAccess.Context;

public class UsersDbContext(DbContextOptions<UsersDbContext> options) 
    : IdentityDbContext<ApplicationUser, ApplicationRole, string>(options)
{ }
