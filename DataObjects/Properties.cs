using System.Collections.Generic;

namespace GroupMaker
{
    public class Properties
    {
        public string file { get; set; }
        public List<Student> studentList { get; set; }
        public bool useGroupSize { get; set; }
        public int groupSize { get; set; }
        public int numberGroups { get; set; }
        public List<StudentRelationship> studentRelationships { get; set; }

        public Properties()
        {
            this.useGroupSize = true;
            this.groupSize = 0;
            this.studentRelationships = new List<StudentRelationship>();
        }
    }
}
