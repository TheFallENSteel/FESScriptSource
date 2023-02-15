using System;
using System.Collections.Generic;
using System.Text;

namespace FESScript2.Graphics.UserControls.SubUserControls
{
    public interface IContents : CodeWorks.IName
    {
        public string Text { get; set; }
        public new int Id { get; set; }
        public bool QuotationMarks { get;}
        public bool IsCompiler { get; set; }
    }
}
