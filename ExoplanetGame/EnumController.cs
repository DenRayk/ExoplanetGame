using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExoplanetGame
{
    public static class EnumController
    {
        public static string GetDescriptionFromEnum<T>(this T enumValue) where T : struct
        {
            Type enumType = enumValue.GetType();

            if (!enumType.IsEnum)
            {
                return string.Empty;
            }

            string enumValueAsString = enumValue.ToString();
            MemberInfo[] memberInfoArray = enumType.GetMember(enumValueAsString);

            bool hasMemberInfo = memberInfoArray != null && memberInfoArray.Length > 0;

            if (hasMemberInfo)
            {
                if (memberInfoArray != null)
                {
                    MemberInfo firstMemberInfo = memberInfoArray[0];
                    object[] customAttributes = firstMemberInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

                    bool hasDescriptionAttribute = customAttributes.Length > 0;

                    if (hasDescriptionAttribute)
                    {
                        DescriptionAttribute descriptionAttribute = (DescriptionAttribute)customAttributes[0];
                        return descriptionAttribute.Description;
                    }
                }
            }

            return enumValueAsString;
        }
    }
}
