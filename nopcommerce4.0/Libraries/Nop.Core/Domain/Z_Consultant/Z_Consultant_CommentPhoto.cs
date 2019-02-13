namespace Nop.Core.Domain.Z_Consultant
{
    public class Z_Consultant_CommentPhoto : BaseEntity
    {
        public string Url { get; set; }

        public int CommentId { get; set; }
     
        public Z_Consultant_Comment Comment { get; set; }
    }
}
 
 
 