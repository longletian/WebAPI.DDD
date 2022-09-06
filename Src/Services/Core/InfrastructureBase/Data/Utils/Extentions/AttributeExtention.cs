using DomainBase.Attributes;
using System;
using System.Reflection;

namespace InfrastructureBase.Data
{
    /// <summary>
    /// 属性扩展
    /// </summary>
    public static class AttributeExtention
    {
        /// <summary>
        /// 获取枚举名称
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum value)
        {
            Type type = value.GetType();
            FieldInfo fieldInfo = type.GetField(value.ToString());
            if (fieldInfo.IsDefined(typeof(DescriptionAttribute), true))
            {
                DescriptionAttribute descriptionAttribute = (DescriptionAttribute)fieldInfo.GetCustomAttribute(typeof(DescriptionAttribute), true);
                return descriptionAttribute.Description;
            }
            else
                return value.ToString();
        }
    }
}
