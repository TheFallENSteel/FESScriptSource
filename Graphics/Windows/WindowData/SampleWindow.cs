using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FESScript.Graphics.Windows.WindowData
{
    public struct SampleWindow
    {
        public SampleWindow(int iD, string name, double popularity)
        {
            this.ID = iD;
            this.Name = name;
            this.Popularity = popularity;
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public double Popularity { get; set; }
        public List<double> SuperList { get; set; } = new List<double>();
        public override readonly string ToString()
        {
            return $"ID: {ID}, Name: {Name}, Popularity: {Popularity}, SuperList: {SuperList.FirstOrDefault()}";
        }
    }
}
