using System.ComponentModel;

namespace GroupMaker
{
    public enum RelationshipTypeEnum
    {
        [Description("[Match With]")] MATCH,
        [Description("[Don't Match With]")] DO_NOT_MATCH
    }
}
