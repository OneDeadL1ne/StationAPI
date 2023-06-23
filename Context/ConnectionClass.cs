using System;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;

using StationAPI.Models;

namespace StationAPI.Context;

public class ConnectionClass : DbContext
{
    public ConnectionClass(DbContextOptions<ConnectionClass> options) : base(options) {}

    public DbSet<Station> Station => Set<Station>();
}