//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class answer_rankings
    {
        public int author_id { get; set; }
        public int answer_id { get; set; }
        public byte rank { get; set; }
    
        public virtual answer answer { get; set; }
        public virtual user user { get; set; }
    }
}
