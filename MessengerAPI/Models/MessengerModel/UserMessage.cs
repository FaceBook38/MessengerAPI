namespace MessengerAPI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserMessage")]
    public partial class UserMessage
    {
        [Key]
        public int MessageId { get; set; }

        [StringLength(550)]
        public string MessageContent { get; set; }

        public DateTime? MessageDate { get; set; }

        public int? SenderId { get; set; }

        public int? ReceiverId { get; set; }

        public bool? Read { get; set; }

        public bool? Received { get; set; }

        public virtual User User { get; set; }

        public virtual User User1 { get; set; }
    }
}
