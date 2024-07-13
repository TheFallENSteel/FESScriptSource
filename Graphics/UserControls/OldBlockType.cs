using System.Collections.Generic;
using FESScript.Graphics.UserControls.SubUserControls;

namespace FESScript.Graphics.UserControls
{
    /// <summary>
    /// Struct used to reconstruct block.
    /// </summary>

    public class OldBlockType
    {
        public static List<OldBlockType> global = new List<OldBlockType>();

        public bool CreateFunction { get; set; }
        public bool IsBodyless { get; set; }
        public string FakeString { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public bool SingleActionOutput { get; set; }
        public int ID { get; set; }
        public Type type { get; set; }
        public List<DotsType> Dots { get; set; }
        public List<ContentsType> Contents { get; set; }

        public OldBlockType(int id, string category, string name, Type type)
        {
            CreateFunction = true;
            this.Category = category;
            this.ID = id;
            this.Name = name;
            this.type = type;
            Dots = new List<DotsType>();
            SingleActionOutput = !HasMoreThanOneActionOutputDot();
            Contents = new List<ContentsType>();
            global.Add(this);
        }

        public bool HasMoreThanOneActionOutputDot()
        {
            int dotCount = 0;
            foreach (DotsType dot in Dots)
            {
                if ((dot.dotType == Type.Action || dot.dotType == Type.SubAction) && dot.io == IO.Output)
                {
                    dotCount++;
                }
                if (dotCount >= 2)
                {
                    return true;
                }
            }
            return false;
        }

        public static OldBlockType Find(int id)
        {
            for (int i = 0; i < global.Count; i++)
            {
                if (global[i].ID == id)
                {
                    return global[i];
                }
            }
            return null;
        }

    }
}
