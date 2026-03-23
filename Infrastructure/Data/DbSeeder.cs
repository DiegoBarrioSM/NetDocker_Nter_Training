using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.BankAccounts.AnyAsync())
            return;

        var account = new BankAccount(new Guid("11111111-1111-1111-1111-111111111111"), "Test Account 1", 0);

        context.BankAccounts.Add(account);

        await context.SaveChangesAsync();
    }
}