using Microsoft.EntityFrameworkCore;  
namespace PaymentApi.Models {     
public class BankInfoContext : DbContext     
{ 
  public DbSet<BankInfo> BankInfo { get; set; }      
  public BankInfoContext(DbContextOptions<BankInfoContext> options): base(options)         
  {         
  }       
}
   
}