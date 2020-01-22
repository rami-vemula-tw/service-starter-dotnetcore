using Microsoft.EntityFrameworkCore;  
namespace StubBankApi.Models {     
public class BankContext : DbContext     
  {         
    public BankContext(DbContextOptions<BankContext> options)                            : base(options)         
{         
}       
    public DbSet<Account> Accounts { get; set; }     
  
   } 
}