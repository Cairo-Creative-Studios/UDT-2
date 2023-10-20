using UDT.Scriptables.Utilities;
using NaughtyAttributes;

namespace UDT.Scriptables.Variables
{
    [CreateNodeMenu("Variables/Value Types/Enum Value")]
    public sealed class EnumValue : VariableNode<int>
    {
        [Dropdown("GetEnums")]
        public Enum enumType;

        [Dropdown("GetEnumValues")]
        public new int value;

        public DropdownList<Enum> GetEnums()
        {
            DropdownList<Enum> returnList = new();
            var enums = ((EventGraph)graph).enums;

            returnList.Add("None", null);

            foreach(var e in enums)
            {
                returnList.Add(e.name, e);
            }

            return returnList;
        }

        public DropdownList<int> GetEnumValues()
        {
            DropdownList<int> returnList = new();

            if(enumType == null)
            {
                returnList.Add("None", 0);
                return returnList;
            }

            for(int i = 0; i < enumType.values.Count; i++)
            {
                var item = enumType.values[i];
                returnList.Add(item, i);
            }

            return returnList;
        }
    }
}

