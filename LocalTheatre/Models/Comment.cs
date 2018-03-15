using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocalTheatre.Models
{
    ///<summary>
    /// this class creates Comment object with its properties, Comment ID, Post ID, Comment Bodey, Comment Date and UserName
    /// 
    /// </summary>
    public class Comment
    {
        [Key]
        public int CommentID { get; set; }


        public int PostID { get; set; }

        [DataType(DataType.MultilineText)]
        public string commentMain { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CommentDate { get; set; } = DateTime.Now;

        public string UserName { get; set; }
    }
}