using Microsoft.EntityFrameworkCore;  
namespace PaymentApi.Models {     
public class PaymentContext : DbContext     
{
  public DbSet<Payment> Payments { get; set; }          
  public PaymentContext(DbContextOptions<PaymentContext> options): base(options)         
  {         
  }       
} 

}