namespace GroupMaker
{
    public class StudentRelationship
    {
        public Student student1 { get; }
        public RelationshipTypeEnum relType { get; }
        public Student student2 { get; }

        public StudentRelationship(Student student1, RelationshipTypeEnum relType, Student student2)
        {
            this.student1 = student1;
            this.relType = relType;
            this.student2 = student2;
        }

        public override string ToString()
        {
            return string.Concat(student1.ToString(), " ", Utils.GetDescription(relType), " ", student2.ToString());
        }

        public bool Equals(StudentRelationship other)
        {
            return null != other && this.student1.Equals(other.student1) && this.student2.Equals(other.student2) && this.relType == other.relType;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as StudentRelationship);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
