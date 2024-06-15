using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA_WEBHAUI_AMIS_Core.Entities
{
    public class Comment
    {
        public Guid CommentId { get; set; }
        public string CommentContent { get; set; }
        public int Rating { get; set; }
        public Guid UserId { get; set; }
        public DateTime PostDate { get; set; }
        public Guid ProductId { get; set; }
    }
}
