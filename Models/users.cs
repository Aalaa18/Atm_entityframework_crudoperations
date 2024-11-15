using System.ComponentModel.DataAnnotations;

public class users
{
   
    public int id { get; set; }
    public string pass { get; set; }
    public double balance { get; set; }
    public string email { get; set; }
    public DateTime birthdate { get; set; }
    public string category { get; set; }
    public int max_count { get; set; }
    public string name { get; set; }
    //public int user_id { get; set; }

    public ICollection<TransactionInfo> transactionInfos { get; set; }

}
