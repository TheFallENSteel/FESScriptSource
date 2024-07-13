using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FESScript.CodeWorks.BlockCreation.Interfaces
{
    public interface IFindable
    {
        public static List<IFindable> TopFindable = new List<IFindable>();

        public int ID { get; protected set; }

        public IFindable Parent { get; }
        public IFindable FindChild(int ID);

        public void Register() 
        {
            if (this.ID == 0) AssignUniqueID();
            TopFindable.Add(this);
        }

        public void UnRegister()
        {
            if (this.ID != 0) 
            { 
                TopFindable.Remove(this);
                freeID.Enqueue(this.ID);
                ID = 0;
            }
        }

        public IFindable Find(Queue<int> address) 
        {
            if (address.Count == 0) return this;
            IFindable child = FindChild(address.Dequeue());
            if (child == null) return child.Find(address);
            return null;
        }

        public Queue<int> GetAddress()
        {
            Queue<int> queue = new Queue<int>();
            GetAddress(ref queue);
            return queue;
        }

        private void GetAddress(ref Queue<int> queue) 
        { 
            if (Parent != null) Parent.GetAddress(ref queue);
            queue.Enqueue(ID);
        }

        const int DynamicIDMin = int.MinValue;
        const int DynamicIDMax = 0;

        static int LargestID = DynamicIDMin;
        static Queue<int> freeID = new Queue<int>();

        public void AssignUniqueID() 
        {
            if (freeID.Count == 0) 
            {
                LargestID++;
                this.ID = LargestID;
            }
            else 
            {
                this.ID = freeID.Dequeue();
            }
        }
    }
}
