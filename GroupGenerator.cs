using System.Collections.Generic;

namespace GroupMaker
{
    public class GroupGenerator
    {
        private Properties properties;
        public List<StudentGroup> studentGroups { get; }
        private int groupSize;

        public GroupGenerator(Properties properties)
        {
            this.properties = properties;
            this.studentGroups = new List<StudentGroup>();
        }

        private void generateEmptyGroups()
        {
            if (this.properties.useGroupSize)
            {
                this.groupSize = this.properties.groupSize;
                int studentCount = this.properties.studentList.Count;
                int numberOfGroups = studentCount / this.properties.groupSize;
                int remainder = studentCount % numberOfGroups;

                for (int i = 0; i < numberOfGroups; i++)
                {
                    if (i != numberOfGroups - 1)
                        this.studentGroups.Add(new StudentGroup(this.properties.groupSize, i));
                    else
                        this.studentGroups.Add(new StudentGroup(this.properties.groupSize + remainder, i));
                }
            }
            else
            {
                int studentCount = this.properties.studentList.Count;
                this.groupSize = studentCount / this.properties.numberGroups;
                int remainder = studentCount % groupSize;

                for (int i = 0; i < this.properties.numberGroups; i++)
                {
                    if (i != this.properties.numberGroups - 1)
                        this.studentGroups.Add(new StudentGroup(groupSize, i));
                    else
                        this.studentGroups.Add(new StudentGroup(groupSize + remainder, i));
                }
            }
        }

        public void makeGroups()
        {
            generateEmptyGroups();
            List<Student> copy = clone(properties.studentList);

            // add one student to each group
            foreach (StudentGroup sg in studentGroups)
            {
                sg.addMember(copy[0]);
                copy.RemoveAt(0);
            }

            // add best match for each group
            foreach (StudentGroup sg in studentGroups)
            {
                int indexBest = getIndexOfBestMatch(sg, copy);
                sg.addMember(copy[indexBest]);
                copy.RemoveAt(indexBest);
            }

            // optimize
            for (int k = 0; k < studentGroups.Count; k++)
            {
                optimize(studentGroups[k]);
            }
        }

        private bool optimize(StudentGroup group1)
        {
            foreach (Student student1 in group1.getMembers())
            {
                // group score of group with student1
                int before1 = getGroupScore(group1);

                foreach (StudentGroup group2 in studentGroups)
                {
                    if (group2.id == group1.id)
                        continue;
                    foreach (Student student2 in group2.getMembers())
                    {
                        int before2 = getGroupScore(group2);

                        int after = getGroupScoreIfReplace(group1, student1, student2) + getGroupScoreIfReplace(group2, student2, student1);

                        if (after < before1 + before2)
                        {
                            swapGroupMembers(group1, student1, group2, student2);
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private void swapGroupMembers(StudentGroup group1, Student member1, StudentGroup group2, Student member2)
        {
            group1.removeMember(member1);
            group1.addMember(member2);
            group2.removeMember(member2);
            group2.addMember(member1);
        }

        private int getGroupScoreIfReplace(StudentGroup group, Student member1, Student member2)
        {
            StudentGroup copy = new StudentGroup(group);
            copy.removeMember(member1);
            copy.addMember(member2);
            return getGroupScore(copy);
        }

        private int getGroupScore(StudentGroup group)
        {
            int score = group.max * group.max;
            foreach (Student s in group.getMembers())
            {
                foreach (Student member in group.getMembers())
                {
                    if (member.getId() != s.getId())
                    {
                        if (relationshipListContains(s, RelationshipTypeEnum.MATCH, member))
                        {
                            score -= 1;
                        }
                        if (relationshipListContains(s, RelationshipTypeEnum.DO_NOT_MATCH, member))
                        {
                            score += 1000;
                        }
                    }
                }
            }
            return score;
        }

        private int getIndexOfBestMatch(StudentGroup studentGroup, List<Student> studentList)
        {
            int[] points = new int[studentList.Count];
            for (int i = 0; i < studentList.Count; i++)
            {
                Student s = studentList[i];
                points[i] = this.groupSize;
                foreach (Student member in studentGroup.getMembers())
                {
                    if (relationshipListContains(s, RelationshipTypeEnum.MATCH, member))
                    {
                        points[i] -= 1;
                    }
                    if (relationshipListContains(s, RelationshipTypeEnum.DO_NOT_MATCH, member))
                    {
                        points[i] += 1000;
                    }
                }
            }

            int minIndex = 0;
            for (int j = 0; j < points.Length; j++)
            {
                if (points[j] < points[minIndex])
                    minIndex = j;
            }

            return minIndex;
        }

        private bool relationshipListContains(Student student1, RelationshipTypeEnum relType, Student student2)
        {
            foreach (StudentRelationship sr in properties.studentRelationships)
            {
                if (sr.student1.Equals(student1) && sr.relType == relType && sr.student2.Equals(student2))
                    return true;
            }
            return false;
        }

        private List<Student> clone(List<Student> arr)
        {
            List<Student> clone = new List<Student>();
            for (int i = 0; i < arr.Count; i++)
            {
                clone.Add(new Student(arr[i]));
            }
            return clone;
        }
    }
}
