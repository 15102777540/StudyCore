using System;
using System.Collections.Generic;
using System.Text;

namespace StudyCore.Model
{

    public abstract class Entity<T>
    {
        public T Id { get; set; }
    }
}
