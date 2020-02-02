using System;

namespace GroupMaker
{
    public class Student
    {
        private String firstName;
        private String lastName;
        private int id;

        public Student(String firstName, String lastName, int id)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.id = id;
        }

        public Student(Student other)
        {
            this.firstName = other.firstName;
            this.lastName = other.lastName;
            this.id = other.id;
        }

        public int getId()
        {
            return this.id;
        }

        public String getFirstName()
        {
            return this.firstName;
        }

        public String getLastName()
        {
            return this.lastName;
        }

        public override string ToString()
        {
            return string.Concat(firstName, " ", lastName);
        }

        public bool Equals(Student other)
        {
            return null != other && this.id == other.id;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Student);
        }
        public override int GetHashCode()
        {
            return id;
        }
    }
}
