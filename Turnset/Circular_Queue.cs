using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessBasedGame
{
    public class Circular_Queue<Type> : Queue<Type>
    {
        Queue<int> q = new Queue<int>();

        public Circular_Queue(Type[] a)
        {
            foreach (Type t in a)
                this.Enqueue(t);
        }
        public Type Dequeue()
        {
            Type elt = base.Dequeue();
            this.Enqueue(elt);
            return elt;
        }
        public Type Requeue()//undoes a Dequeue
        {
            for (int i = 0; i < this.Count - 2; i++)
                this.Dequeue();
            return this.Dequeue();
        }
    }
}
