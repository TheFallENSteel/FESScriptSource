using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FESScript.CodeWorks.BlockCreation.Blocks.UserElements.Contents.Args
{
    [JsonDerivedType(typeof(BoolArgs), typeDiscriminator: "boolValue")]
    [JsonDerivedType(typeof(ComboBoxArgs), typeDiscriminator: "comboBoxValue")]
    [JsonDerivedType(typeof(DoubleArgs), typeDiscriminator: "doubleValue")]
    [JsonDerivedType(typeof(StringArgs), typeDiscriminator: "stringValue")]
    public interface IArgs : INotifyPropertyChanged
    {
        public bool IsReadOnly { get; set; }
        public void Set(IArgs args);
    }
}
