using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class TransactionInfo
{
   
    public int TransactionId { get; set; }
    [ForeignKey("users")]
    public int UserId { get; set; }
    [Key]
    public int OperationId { get; set; }
    public string sender_Username { get; set; }
    public string reciever_Username { get; set; }
    public double OperationAmount { get; set; }
    public DateTime operationdatetime { get; set; }
    public double balancebefore { get; set; }
    public double balanceafter { get; set; }
    public bool iscomplete { get; set; }
    public string operation_name { get; set; }

    public users users { get; set; }



}