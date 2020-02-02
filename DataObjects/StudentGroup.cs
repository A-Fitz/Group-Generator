using System.Collections.Generic;

namespace GroupMaker
{
    public class StudentGroup
    {
        private List<Student> memberList;
        public int max { get; }
        public int id { get; }

        public StudentGroup(int size, int id)
        {
            this.max = size;
            this.memberList = new List<Student>();
            this.id = id;
        }

        public StudentGroup(StudentGroup other)
        {
            this.id = other.id;
            this.memberList = new List<Student>(other.memberList);
            this.max = other.max;
        }

        public List<Student> getMembers()
        {
            return this.memberList;
        }

        public void addMember(Student student)
        {
            this.memberList.Add(student);
        }

        public void removeMember(Student student)
        {
            this.memberList.Remove(student);
        }

        public override string ToString()
        {
            return string.Concat("Group #", this.id.ToString());
        }

        public bool Equals(StudentGroup other)
        {
            return null != other && this.id == other.id;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as StudentGroup);
        }
        public override int GetHashCode()
        {
            return id;
        }
    }
}
